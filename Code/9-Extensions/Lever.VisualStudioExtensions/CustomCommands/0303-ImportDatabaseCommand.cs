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
    internal sealed class ImportDatabaseCommand : CustomCommand
    {
        public ImportDatabaseCommand()
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
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            if (!fullPath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (!EPPlusHelper.ContainsSheet(fullPath, ImportDatabaseConfig.DefaultSheetName))
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
            ImportDatabaseConfig config = ImportDatabaseConfig.CreateFromExcel(fullPath, dataSet);
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

            using (ImportDatabaseForm dialog = new ImportDatabaseForm())
            {
                dialog.Initialize(config, dataSet);
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.Retry)
                {
                    return;
                }

                string connectionString = dialog.GetConnectionString();
                string clearSQL = dialog.GetClearSQL();

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        // Clear script
                        if (clearSQL != null)
                        {
                            using (SqlCommand clearCommand = connection.CreateCommand())
                            {
                                clearCommand.CommandText = clearSQL;
                                clearCommand.ExecuteNonQuery();
                            }
                        }

                        foreach (DataTable table in dataSet.Tables)
                        {
                            string tableName = table.TableName;
                            if (ImportDatabaseConfig.DefaultSheetName.Equals(tableName, StringComparison.OrdinalIgnoreCase))
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

                            using (SqlTransaction transcation = connection.BeginTransaction())
                            {
                                SqlCommand command = null;
                                switch (dialog.GetImportModel())
                                {
                                    case ImportDatabaseModel.IgnoreExists:
                                        command = SqlDatabaseHelper.GetInsertIfNotExistCommand(connection, usedColumnInfos, tableName);
                                        break;
                                    case ImportDatabaseModel.Merge:
                                        command = SqlDatabaseHelper.GetMergeCommand(connection, usedColumnInfos, tableName);
                                        break;
                                }
                                command.Transaction = transcation;

                                foreach(DataRow row in table.Rows)
                                {
                                    for (int i = 0; i < usedColumnIndices.Count; i++)
                                    {
                                        int columnIndex = usedColumnIndices[i];
                                        string value = (string)row[columnIndex];
                                        if (value == "NULL")
                                        {
                                            command.Parameters[i].Value = DBNull.Value;
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(value))
                                            {
                                                switch (command.Parameters[i].DbType)
                                                {
                                                    case System.Data.DbType.Boolean:
                                                    case System.Data.DbType.Byte:
                                                    case System.Data.DbType.Double:
                                                    case System.Data.DbType.Int16:
                                                    case System.Data.DbType.Int32:
                                                    case System.Data.DbType.Int64:
                                                    case System.Data.DbType.SByte:
                                                    case System.Data.DbType.Single:
                                                    case System.Data.DbType.UInt16:
                                                    case System.Data.DbType.UInt32:
                                                    case System.Data.DbType.UInt64:
                                                        if (command.Parameters[i].IsNullable)
                                                        {
                                                            command.Parameters[i].Value = DBNull.Value;
                                                        }
                                                        else
                                                        {
                                                            command.Parameters[i].Value = 0;
                                                        }
                                                        break;
                                                    case System.Data.DbType.AnsiString:
                                                    case System.Data.DbType.AnsiStringFixedLength:
                                                    case System.Data.DbType.StringFixedLength:
                                                    case System.Data.DbType.String:
                                                        if (command.Parameters[i].IsNullable)
                                                        {
                                                            command.Parameters[i].Value = DBNull.Value;
                                                        }
                                                        else
                                                        {
                                                            command.Parameters[i].Value = string.Empty;
                                                        }
                                                        break;
                                                    default:
                                                        command.Parameters[i].Value = (object)value ?? DBNull.Value;
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                Convert.ChangeType()
                                                command.Parameters[i].Value = value ?? DBNull.Value;
                                            }
                                        }
                                    }

                                    command.ExecuteNonQuery();
                                }

                                transcation.Commit();
                            }
                        }
                    }

                    this.ShowMessage("Success", "Import Successfully");
                }
                catch (Exception exception)
                {
                    this.ShowMessage("Failure", exception.Message);
                }
            }


        }
    }
}
