using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using EnvDTE;
using System.Linq;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Meision.Database.SQL;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class SyncDatabaseCommand : CustomCommand
    {

        public SyncDatabaseCommand()
        {
            this.CommandId = 0x0303;
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = false;

            if (this.DTE.SelectedItems.Count != 1)
            {
                return;
            }
            if (this.DTE.SelectedItems.Item(1).ProjectItem.Kind != (string)EnvDTE.Constants.vsProjectItemKindPhysicalFile)
            {
                return;
            }
            if ((string)this.DTE.SelectedItems.Item(1).ProjectItem.Properties.Item("CustomTool").Value != "SyncDatabase")
            {
                return;
            }

            menuItem.Visible = true;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            DataSet dataSet;
            using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                dataSet = EPPlusHelper.ReadExcelToDataSet(stream);
            }
            SyncDatabaseConfig config = SyncDatabaseConfig.CreateFromExcel(fullPath, dataSet);
            if (config == null)
            {
                this.ShowMessage("Error", "Could not load config.");
                return;
            }
            if (config.ConnectionProvider != "System.Data.SqlClient")
            {
                this.ShowMessage("Error", "Only support System.Data.SqlClient for ConnectionProvider currently.");
                return;
            }

            using (SyncDatabaseForm dialog = new SyncDatabaseForm())
            {
                dialog.Initialize(config, dataSet);
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string connectionString = dialog.GetConnectionString();
                string clearSQL = dialog.GetClearSQL();

                try
                {
                    StringBuilder builder = new StringBuilder();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        // Clear script
                        if (clearSQL != null)
                        {
                            builder.AppendLine(clearSQL);
                        }

                        foreach (DataTable table in dataSet.Tables)
                        {
                            string tableName = table.TableName;
                            if (SyncDatabaseConfig.DefaultSheetName.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            SqlColumnInfo[] allColumnInfos = SqlDatabaseHelper.GetColumnInfos(connection, tableName);
                            // Filter available columns
                            List<SqlColumnInfo> usedColumnInfos = new List<SqlColumnInfo>();

                            List<int> usedColumnIndices = new List<int>();
                            for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                            {
                                string columnName = table.Columns[columnIndex].ColumnName;
                                SqlColumnInfo columnInfo = allColumnInfos.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                                if (columnInfo != null)
                                {
                                    usedColumnInfos.Add(columnInfo);
                                    usedColumnIndices.Add(columnIndex);
                                }
                            }

                            string template = null;
                            switch (dialog.GetSyncModel())
                            {
                                case SyncDatabaseModel.Insert:
                                    template = SqlScriptHelper.GenerateInsertScript(usedColumnInfos, tableName);
                                    break;
                                case SyncDatabaseModel.InsertNotExists:
                                    template = SqlScriptHelper.GenerateInsertIfNotExistsScript(usedColumnInfos, tableName);
                                    break;
                                case SyncDatabaseModel.Merge:
                                    template = SqlScriptHelper.GenerateMergeScript(null, usedColumnInfos, tableName);
                                    break;
                            }

                            builder.AppendLine($"-- {tableName}");
                            builder.AppendLine($"IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = object_id('{tableName}')) SET IDENTITY_INSERT [{tableName}] ON");
                            foreach (DataRow row in table.Rows)
                            {
                                string command = template;
                                for (int i = 0; i < usedColumnIndices.Count; i++)
                                {
                                    int columnIndex = usedColumnIndices[i];
                                    string value = (string)row[columnIndex];
                                    string text = string.Empty;
                                    SqlColumnInfo columnInfo = usedColumnInfos[i];
                                    if (value == "NULL" || string.IsNullOrEmpty(value))
                                    {
                                        switch (columnInfo.Type)
                                        {
                                            case SqlColumnType.@text:
                                            case SqlColumnType.@ntext:
                                            case SqlColumnType.@varchar:
                                            case SqlColumnType.@char:
                                            case SqlColumnType.@nvarchar:
                                            case SqlColumnType.@nchar:
                                            case SqlColumnType.@xml:
                                                if (columnInfo.Nullable)
                                                {
                                                    text = "null";
                                                }
                                                else
                                                {
                                                    text = "''";
                                                }
                                                break;
                                            case SqlColumnType.@tinyint:
                                            case SqlColumnType.@smallint:
                                            case SqlColumnType.@int:
                                            case SqlColumnType.@real:
                                            case SqlColumnType.@money:
                                            case SqlColumnType.@float:
                                            case SqlColumnType.@bit:
                                            case SqlColumnType.@decimal:
                                            case SqlColumnType.@numeric:
                                            case SqlColumnType.@smallmoney:
                                            case SqlColumnType.@bigint:
                                                if (columnInfo.Nullable)
                                                {
                                                    text = "null";
                                                }
                                                else
                                                {
                                                    text = "0";
                                                }
                                                break;
                                            case SqlColumnType.@date:
                                            case SqlColumnType.@datetime2:
                                            case SqlColumnType.@time:
                                            case SqlColumnType.@datetimeoffset:
                                            case SqlColumnType.@smalldatetime:
                                            case SqlColumnType.@datetime:
                                                text = "null";
                                                break;
                                            case SqlColumnType.@image:
                                            case SqlColumnType.@varbinary:
                                            case SqlColumnType.@binary:
                                                if (columnInfo.Nullable)
                                                {
                                                    text = "null";
                                                }
                                                else
                                                {
                                                    text = "0x";
                                                }
                                                break;
                                            case SqlColumnType.@uniqueidentifier:
                                                if (columnInfo.Nullable)
                                                {
                                                    text = "null";
                                                }
                                                else
                                                {
                                                    text = "'00000000-0000-0000-0000-000000000000'";
                                                }
                                                break;
                                            default:
                                                throw new NotSupportedException();
                                        }
                                    }
                                    else
                                    {
                                        switch (columnInfo.Type)
                                        {
                                            case SqlColumnType.@text:
                                            case SqlColumnType.@ntext:
                                            case SqlColumnType.@varchar:
                                            case SqlColumnType.@char:
                                            case SqlColumnType.@nvarchar:
                                            case SqlColumnType.@nchar:
                                            case SqlColumnType.@xml:
                                                text = $"'{value}'";
                                                break;
                                            case SqlColumnType.@bit:
                                                text = $"{Convert.ToInt32(Convert.ToBoolean(value))}";
                                                break;
                                            case SqlColumnType.@tinyint:
                                            case SqlColumnType.@smallint:
                                            case SqlColumnType.@int:
                                            case SqlColumnType.@real:
                                            case SqlColumnType.@money:
                                            case SqlColumnType.@float:
                                            case SqlColumnType.@decimal:
                                            case SqlColumnType.@numeric:
                                            case SqlColumnType.@smallmoney:
                                            case SqlColumnType.@bigint:
                                                text = $"{value}";
                                                break;
                                            case SqlColumnType.@date:
                                            case SqlColumnType.@datetime2:
                                            case SqlColumnType.@time:
                                            case SqlColumnType.@datetimeoffset:
                                            case SqlColumnType.@smalldatetime:
                                            case SqlColumnType.@datetime:
                                                text = $"'{value}'";
                                                break;
                                            case SqlColumnType.@image:
                                            case SqlColumnType.@varbinary:
                                            case SqlColumnType.@binary:
                                                text = $"{value}";
                                                break;
                                            case SqlColumnType.@uniqueidentifier:
                                                text = $"'{value}'";
                                                break;
                                            default:
                                                throw new NotSupportedException();
                                        }
                                    }

                                    command = Regex.Replace(command, $@"@{columnInfo.Name}\b", (m) =>
                                    {
                                        return text;
                                    });
                                }
                                builder.AppendLine(command);
                            }
                            builder.AppendLine($"IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = object_id('{tableName}')) SET IDENTITY_INSERT [{tableName}] OFF");
                        }

                        switch (dialog.GetSyncAction())
                        {
                            case SyncDatabaseAction.GenerateScript:
                                {
                                    string commandText = builder.ToString();
                                }
                                break;
                            case SyncDatabaseAction.ImportDatabase:
                                {
                                    using (SqlTransaction transcation = connection.BeginTransaction())
                                    using (SqlCommand command = connection.CreateCommand())
                                    {
                                        command.CommandText = builder.ToString();
                                        command.Transaction = transcation;
                                        command.ExecuteNonQuery();
                                        transcation.Commit();
                                    }

                                    this.ShowMessage("Success", "Operation Successfully.");
                                }
                                break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.ShowMessage("Failure", exception.Message);
                }
            }
        }
    }
}
