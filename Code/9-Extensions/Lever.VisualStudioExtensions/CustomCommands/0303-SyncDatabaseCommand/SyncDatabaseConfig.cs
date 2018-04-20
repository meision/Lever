using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meision.VisualStudio.CustomCommands
{
    internal enum SyncDatabaseAction
    {
        None = 0,
        GenerateScript = 1,
        ImportDatabase = 2,
    }

    internal enum SyncDatabaseConnectionStringSource
    {
        Static,
        FileRegex,
    }

    internal enum SyncDatabaseModel
    {
        None,
        Insert,
        InsertNotExists,
        Merge,
    }

    internal class SyncDatabaseConfig
    {
        public const string DefaultSheetName = "__Config";

        public static SyncDatabaseConfig CreateFromExcel(string fullname)
        {
            DataSet dataSet = EPPlusHelper.ReadExcelToDataSet(fullname, (sheetName) => DefaultSheetName.Equals(sheetName, StringComparison.OrdinalIgnoreCase));
            return CreateFromExcel(fullname, dataSet);
        }
        public static SyncDatabaseConfig CreateFromExcel(string excelFilePath, DataSet dataSet)
        {
            if (excelFilePath == null)
            {
                throw new ArgumentNullException(nameof(excelFilePath));
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException(nameof(dataSet));
            }
            DataTable table = dataSet.Tables[DefaultSheetName];
            if (table == null)
            {
                return null;
            }

            SyncDatabaseConfig config = new SyncDatabaseConfig();
            // Action
            config.Action = (SyncDatabaseAction)Enum.Parse(typeof(SyncDatabaseAction), Convert.ToString(table.Rows[0]["Action"]));
            // ConnectionProvider
            config.ConnectionProvider = Convert.ToString(table.Rows[0]["ConnectionProvider"]);
            // Source
            SyncDatabaseConnectionStringSource connectionStringSource = (SyncDatabaseConnectionStringSource)Enum.Parse(typeof(SyncDatabaseConnectionStringSource), Convert.ToString(table.Rows[0]["ConnectionStringSource"]));
            switch (connectionStringSource)
            {
                case SyncDatabaseConnectionStringSource.Static:
                    {
                        config.ConnectionStrings = Convert.ToString(table.Rows[0]["ConnectionStringExpression"])
                            .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                            .ToArray();
                    }
                    break;
                case SyncDatabaseConnectionStringSource.FileRegex:
                    {
                        config.ConnectionStrings = Convert.ToString(table.Rows[0]["ConnectionStringExpression"])
                            .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(p =>
                            {
                                string[] segments = p.Split('|');
                                string connectionStringFile = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(excelFilePath), segments[0]));
                                Match match = Regex.Match(System.IO.File.ReadAllText(connectionStringFile), segments[1]);
                                if (!match.Success)
                                {
                                    return null;
                                }
                                Group group = match.Groups["ConnectionString"];
                                if (!group.Success)
                                {
                                    return null;
                                }
                                return group.Value;
                            })
                            .ToArray();
                    }
                    break;
                default:
                    break;
            }

            config.ClearTableBeforeImport = Convert.ToBoolean(table.Rows[0]["ClearTableBeforeImport"]);
            config.Mode = (SyncDatabaseModel)Enum.Parse(typeof(SyncDatabaseModel), Convert.ToString(table.Rows[0]["ImportMode"]));
            return config;
        }

        public SyncDatabaseAction Action { get; set; }
        public string ConnectionProvider { get; set; }
        public IList<string> ConnectionStrings { get; set; }
        public bool ClearTableBeforeImport { get; set; }
        public SyncDatabaseModel Mode { get; set; }
    }
}
