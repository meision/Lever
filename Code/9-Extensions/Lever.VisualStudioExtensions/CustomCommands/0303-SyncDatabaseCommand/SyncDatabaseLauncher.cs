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
    public class SyncDatabaseLauncher : Launcher
    {
        private class SyncDatabaseExecuteConfig
        {
            public string ConnectionString { get; set; }
            public string ClearSQL { get; set; }
            public SyncDatabaseAction Action { get; set; }
            public SyncDatabaseMode Mode { get; set; }
        }

        private SyncDatabaseConfig _config;
        private DataSet _dataSet;



        public SyncDatabaseLauncher(ProjectItem projectItem) : base(projectItem)
        {
        }

        public override bool Launch()
        {
            if (!this.InputFilePath.EndsWith(".xlsx"))
            {
                throw new InvalidDataException("Input file should be excel file.");
            }

            using (FileStream stream = new FileStream(this.InputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                this._dataSet = EPPlusHelper.ReadExcelToDataSet(stream);
            }
            this._config = SyncDatabaseConfig.CreateFromExcel(this.InputFilePath, this._dataSet);
            if (this._config == null)
            {
                throw new InvalidDataException("Could not load config.");
            }
            if (this._config.ConnectionProvider != "System.Data.SqlClient")
            {
                throw new InvalidDataException("Only support System.Data.SqlClient for ConnectionProvider currently.");
            }

            SyncDatabaseExecuteConfig executeConfig = this.LoadExecuteConfig();
            if (executeConfig == null)
            {
                return false;
            }

            string script = this.GenerateScript(executeConfig);
            if (script == null)
            {
                return false;
            }

            switch (executeConfig.Action)
            {
                case SyncDatabaseAction.GenerateScript:
                    {
                        string outputFilePath = this.GetOutputFilePathByExtension(".sql");

                        if (this.ProjectItem.Kind.Equals(Parameters.guidSQLServerDatabaseProject, StringComparison.OrdinalIgnoreCase))
                        {
                            System.IO.File.WriteAllText(outputFilePath, script);
                        }
                        else
                        {
                            this.ProjectItem.DeleteDependentFiles();
                            System.IO.File.WriteAllText(outputFilePath, script);
                            this.ProjectItem.Collection.AddFromFile(outputFilePath);
                            this.ProjectItem.AddDependentFromFiles(outputFilePath);
                        }
                    }
                    break;
                case SyncDatabaseAction.ImportDatabase:
                    {
                        using (SqlConnection connection = new SqlConnection(executeConfig.ConnectionString))
                        using (SqlTransaction transcation = connection.BeginTransaction())
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            try
                            {
                                command.CommandText = script;
                                command.Transaction = transcation;
                                command.ExecuteNonQuery();
                                transcation.Commit();
                                System.Windows.Forms.MessageBox.Show("Operation Successfully.");
                            }
                            finally
                            {
                                transcation.Rollback();
                            }
                        }

                    }
                    break;
            }

            return true;
        }

        private SyncDatabaseExecuteConfig LoadExecuteConfig()
        {
            using (SyncDatabaseForm dialog = new SyncDatabaseForm())
            {
                dialog.Initialize(this._config, this._dataSet);
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return null;
                }

                SyncDatabaseExecuteConfig executeConfig = new SyncDatabaseExecuteConfig();
                executeConfig.ConnectionString = dialog.GetConnectionString();
                executeConfig.ClearSQL = dialog.GetClearSQL();
                executeConfig.Action = dialog.GetSyncAction();
                executeConfig.Mode = dialog.GetSyncMode();
                return executeConfig;
            }

        }

        private string GenerateScript(SyncDatabaseExecuteConfig executeConfig)
        {
            StringBuilder builder = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(executeConfig.ConnectionString))
            {
                connection.Open();
                // Clear script
                if (executeConfig.ClearSQL != null)
                {
                    builder.AppendLine(executeConfig.ClearSQL);
                }

                foreach (DataTable table in this._dataSet.Tables)
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
                    switch (executeConfig.Mode)
                    {
                        case SyncDatabaseMode.Insert:
                            template = SqlScriptHelper.GenerateInsertScript(usedColumnInfos, tableName);
                            break;
                        case SyncDatabaseMode.InsertNotExists:
                            template = SqlScriptHelper.GenerateInsertIfNotExistsScript(usedColumnInfos, tableName);
                            break;
                        case SyncDatabaseMode.Merge:
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

            }

            return builder.ToString();
        }

    }
}
