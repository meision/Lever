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
        public static byte GetHexValue(char high, char low)
        {
            byte b;
            switch (high)
            {
                case '0':
                    switch (low)
                    {
                        case '0': b = 0x00; break;
                        case '1': b = 0x01; break;
                        case '2': b = 0x02; break;
                        case '3': b = 0x03; break;
                        case '4': b = 0x04; break;
                        case '5': b = 0x05; break;
                        case '6': b = 0x06; break;
                        case '7': b = 0x07; break;
                        case '8': b = 0x08; break;
                        case '9': b = 0x09; break;
                        case 'a': case 'A': b = 0x0A; break;
                        case 'b': case 'B': b = 0x0B; break;
                        case 'c': case 'C': b = 0x0C; break;
                        case 'd': case 'D': b = 0x0D; break;
                        case 'e': case 'E': b = 0x0E; break;
                        case 'f': case 'F': b = 0x0F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '1':
                    switch (low)
                    {
                        case '0': b = 0x10; break;
                        case '1': b = 0x11; break;
                        case '2': b = 0x12; break;
                        case '3': b = 0x13; break;
                        case '4': b = 0x14; break;
                        case '5': b = 0x15; break;
                        case '6': b = 0x16; break;
                        case '7': b = 0x17; break;
                        case '8': b = 0x18; break;
                        case '9': b = 0x19; break;
                        case 'a': case 'A': b = 0x1A; break;
                        case 'b': case 'B': b = 0x1B; break;
                        case 'c': case 'C': b = 0x1C; break;
                        case 'd': case 'D': b = 0x1D; break;
                        case 'e': case 'E': b = 0x1E; break;
                        case 'f': case 'F': b = 0x1F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '2':
                    switch (low)
                    {
                        case '0': b = 0x20; break;
                        case '1': b = 0x21; break;
                        case '2': b = 0x22; break;
                        case '3': b = 0x23; break;
                        case '4': b = 0x24; break;
                        case '5': b = 0x25; break;
                        case '6': b = 0x26; break;
                        case '7': b = 0x27; break;
                        case '8': b = 0x28; break;
                        case '9': b = 0x29; break;
                        case 'a': case 'A': b = 0x2A; break;
                        case 'b': case 'B': b = 0x2B; break;
                        case 'c': case 'C': b = 0x2C; break;
                        case 'd': case 'D': b = 0x2D; break;
                        case 'e': case 'E': b = 0x2E; break;
                        case 'f': case 'F': b = 0x2F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '3':
                    switch (low)
                    {
                        case '0': b = 0x30; break;
                        case '1': b = 0x31; break;
                        case '2': b = 0x32; break;
                        case '3': b = 0x33; break;
                        case '4': b = 0x34; break;
                        case '5': b = 0x35; break;
                        case '6': b = 0x36; break;
                        case '7': b = 0x37; break;
                        case '8': b = 0x38; break;
                        case '9': b = 0x39; break;
                        case 'a': case 'A': b = 0x3A; break;
                        case 'b': case 'B': b = 0x3B; break;
                        case 'c': case 'C': b = 0x3C; break;
                        case 'd': case 'D': b = 0x3D; break;
                        case 'e': case 'E': b = 0x3E; break;
                        case 'f': case 'F': b = 0x3F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '4':
                    switch (low)
                    {
                        case '0': b = 0x40; break;
                        case '1': b = 0x41; break;
                        case '2': b = 0x42; break;
                        case '3': b = 0x43; break;
                        case '4': b = 0x44; break;
                        case '5': b = 0x45; break;
                        case '6': b = 0x46; break;
                        case '7': b = 0x47; break;
                        case '8': b = 0x48; break;
                        case '9': b = 0x49; break;
                        case 'a': case 'A': b = 0x4A; break;
                        case 'b': case 'B': b = 0x4B; break;
                        case 'c': case 'C': b = 0x4C; break;
                        case 'd': case 'D': b = 0x4D; break;
                        case 'e': case 'E': b = 0x4E; break;
                        case 'f': case 'F': b = 0x4F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '5':
                    switch (low)
                    {
                        case '0': b = 0x50; break;
                        case '1': b = 0x51; break;
                        case '2': b = 0x52; break;
                        case '3': b = 0x53; break;
                        case '4': b = 0x54; break;
                        case '5': b = 0x55; break;
                        case '6': b = 0x56; break;
                        case '7': b = 0x57; break;
                        case '8': b = 0x58; break;
                        case '9': b = 0x59; break;
                        case 'a': case 'A': b = 0x5A; break;
                        case 'b': case 'B': b = 0x5B; break;
                        case 'c': case 'C': b = 0x5C; break;
                        case 'd': case 'D': b = 0x5D; break;
                        case 'e': case 'E': b = 0x5E; break;
                        case 'f': case 'F': b = 0x5F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '6':
                    switch (low)
                    {
                        case '0': b = 0x60; break;
                        case '1': b = 0x61; break;
                        case '2': b = 0x62; break;
                        case '3': b = 0x63; break;
                        case '4': b = 0x64; break;
                        case '5': b = 0x65; break;
                        case '6': b = 0x66; break;
                        case '7': b = 0x67; break;
                        case '8': b = 0x68; break;
                        case '9': b = 0x69; break;
                        case 'a': case 'A': b = 0x6A; break;
                        case 'b': case 'B': b = 0x6B; break;
                        case 'c': case 'C': b = 0x6C; break;
                        case 'd': case 'D': b = 0x6D; break;
                        case 'e': case 'E': b = 0x6E; break;
                        case 'f': case 'F': b = 0x6F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '7':
                    switch (low)
                    {
                        case '0': b = 0x70; break;
                        case '1': b = 0x71; break;
                        case '2': b = 0x72; break;
                        case '3': b = 0x73; break;
                        case '4': b = 0x74; break;
                        case '5': b = 0x75; break;
                        case '6': b = 0x76; break;
                        case '7': b = 0x77; break;
                        case '8': b = 0x78; break;
                        case '9': b = 0x79; break;
                        case 'a': case 'A': b = 0x7A; break;
                        case 'b': case 'B': b = 0x7B; break;
                        case 'c': case 'C': b = 0x7C; break;
                        case 'd': case 'D': b = 0x7D; break;
                        case 'e': case 'E': b = 0x7E; break;
                        case 'f': case 'F': b = 0x7F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '8':
                    switch (low)
                    {
                        case '0': b = 0x80; break;
                        case '1': b = 0x81; break;
                        case '2': b = 0x82; break;
                        case '3': b = 0x83; break;
                        case '4': b = 0x84; break;
                        case '5': b = 0x85; break;
                        case '6': b = 0x86; break;
                        case '7': b = 0x87; break;
                        case '8': b = 0x88; break;
                        case '9': b = 0x89; break;
                        case 'a': case 'A': b = 0x8A; break;
                        case 'b': case 'B': b = 0x8B; break;
                        case 'c': case 'C': b = 0x8C; break;
                        case 'd': case 'D': b = 0x8D; break;
                        case 'e': case 'E': b = 0x8E; break;
                        case 'f': case 'F': b = 0x8F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case '9':
                    switch (low)
                    {
                        case '0': b = 0x90; break;
                        case '1': b = 0x91; break;
                        case '2': b = 0x92; break;
                        case '3': b = 0x93; break;
                        case '4': b = 0x94; break;
                        case '5': b = 0x95; break;
                        case '6': b = 0x96; break;
                        case '7': b = 0x97; break;
                        case '8': b = 0x98; break;
                        case '9': b = 0x99; break;
                        case 'a': case 'A': b = 0x9A; break;
                        case 'b': case 'B': b = 0x9B; break;
                        case 'c': case 'C': b = 0x9C; break;
                        case 'd': case 'D': b = 0x9D; break;
                        case 'e': case 'E': b = 0x9E; break;
                        case 'f': case 'F': b = 0x9F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case 'a':
                case 'A':
                    switch (low)
                    {
                        case '0': b = 0xA0; break;
                        case '1': b = 0xA1; break;
                        case '2': b = 0xA2; break;
                        case '3': b = 0xA3; break;
                        case '4': b = 0xA4; break;
                        case '5': b = 0xA5; break;
                        case '6': b = 0xA6; break;
                        case '7': b = 0xA7; break;
                        case '8': b = 0xA8; break;
                        case '9': b = 0xA9; break;
                        case 'a': case 'A': b = 0xAA; break;
                        case 'b': case 'B': b = 0xAB; break;
                        case 'c': case 'C': b = 0xAC; break;
                        case 'd': case 'D': b = 0xAD; break;
                        case 'e': case 'E': b = 0xAE; break;
                        case 'f': case 'F': b = 0xAF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case 'b':
                case 'B':
                    switch (low)
                    {
                        case '0': b = 0xB0; break;
                        case '1': b = 0xB1; break;
                        case '2': b = 0xB2; break;
                        case '3': b = 0xB3; break;
                        case '4': b = 0xB4; break;
                        case '5': b = 0xB5; break;
                        case '6': b = 0xB6; break;
                        case '7': b = 0xB7; break;
                        case '8': b = 0xB8; break;
                        case '9': b = 0xB9; break;
                        case 'a': case 'A': b = 0xBA; break;
                        case 'b': case 'B': b = 0xBB; break;
                        case 'c': case 'C': b = 0xBC; break;
                        case 'd': case 'D': b = 0xBD; break;
                        case 'e': case 'E': b = 0xBE; break;
                        case 'f': case 'F': b = 0xBF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case 'c':
                case 'C':
                    switch (low)
                    {
                        case '0': b = 0xC0; break;
                        case '1': b = 0xC1; break;
                        case '2': b = 0xC2; break;
                        case '3': b = 0xC3; break;
                        case '4': b = 0xC4; break;
                        case '5': b = 0xC5; break;
                        case '6': b = 0xC6; break;
                        case '7': b = 0xC7; break;
                        case '8': b = 0xC8; break;
                        case '9': b = 0xC9; break;
                        case 'a': case 'A': b = 0xCA; break;
                        case 'b': case 'B': b = 0xCB; break;
                        case 'c': case 'C': b = 0xCC; break;
                        case 'd': case 'D': b = 0xCD; break;
                        case 'e': case 'E': b = 0xCE; break;
                        case 'f': case 'F': b = 0xCF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case 'd':
                case 'D':
                    switch (low)
                    {
                        case '0': b = 0xD0; break;
                        case '1': b = 0xD1; break;
                        case '2': b = 0xD2; break;
                        case '3': b = 0xD3; break;
                        case '4': b = 0xD4; break;
                        case '5': b = 0xD5; break;
                        case '6': b = 0xD6; break;
                        case '7': b = 0xD7; break;
                        case '8': b = 0xD8; break;
                        case '9': b = 0xD9; break;
                        case 'a': case 'A': b = 0xDA; break;
                        case 'b': case 'B': b = 0xDB; break;
                        case 'c': case 'C': b = 0xDC; break;
                        case 'd': case 'D': b = 0xDD; break;
                        case 'e': case 'E': b = 0xDE; break;
                        case 'f': case 'F': b = 0xDF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case 'e':
                case 'E':
                    switch (low)
                    {
                        case '0': b = 0xE0; break;
                        case '1': b = 0xE1; break;
                        case '2': b = 0xE2; break;
                        case '3': b = 0xE3; break;
                        case '4': b = 0xE4; break;
                        case '5': b = 0xE5; break;
                        case '6': b = 0xE6; break;
                        case '7': b = 0xE7; break;
                        case '8': b = 0xE8; break;
                        case '9': b = 0xE9; break;
                        case 'a': case 'A': b = 0xEA; break;
                        case 'b': case 'B': b = 0xEB; break;
                        case 'c': case 'C': b = 0xEC; break;
                        case 'd': case 'D': b = 0xED; break;
                        case 'e': case 'E': b = 0xEE; break;
                        case 'f': case 'F': b = 0xEF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                case 'f':
                case 'F':
                    switch (low)
                    {
                        case '0': b = 0xF0; break;
                        case '1': b = 0xF1; break;
                        case '2': b = 0xF2; break;
                        case '3': b = 0xF3; break;
                        case '4': b = 0xF4; break;
                        case '5': b = 0xF5; break;
                        case '6': b = 0xF6; break;
                        case '7': b = 0xF7; break;
                        case '8': b = 0xF8; break;
                        case '9': b = 0xF9; break;
                        case 'a': case 'A': b = 0xFA; break;
                        case 'b': case 'B': b = 0xFB; break;
                        case 'c': case 'C': b = 0xFC; break;
                        case 'd': case 'D': b = 0xFD; break;
                        case 'e': case 'E': b = 0xFE; break;
                        case 'f': case 'F': b = 0xFF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low));
                    }
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(high));
            }
            return b;
        }
        private static byte[] ToBytes(string text)
        {
            const string Prefix = "0x";

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            if (!text.StartsWith(Prefix)
             || (((text.Length - Prefix.Length) % 2) != 0))
            {
                throw new ArgumentException("Invalid hex string.", nameof(text));
            }

            byte[] buffer = new byte[(text.Length - Prefix.Length) / 2];
            int index = 0;
            for (int i = Prefix.Length; i < text.Length; i += 2)
            {
                buffer[index++] = GetHexValue(text[i], text[i + 1]);
            }
            return buffer;
        }

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

                            using (SqlTransaction transcation = connection.BeginTransaction())
                            {
                                SqlCommand command = null;
                                switch (dialog.GetSyncModel())
                                {
                                    case SyncDatabaseModel.InsertNotExists:
                                        command = SqlDatabaseHelper.GetInsertIfNotExistCommand(connection, usedColumnInfos, tableName);
                                        break;
                                    case SyncDatabaseModel.Merge:
                                        command = SqlDatabaseHelper.GetMergeCommand(connection, usedColumnInfos, tableName);
                                        break;
                                }
                                command.Transaction = transcation;

                                foreach (DataRow row in table.Rows)
                                {
                                    for (int i = 0; i < usedColumnIndices.Count; i++)
                                    {
                                        int columnIndex = usedColumnIndices[i];
                                        string value = (string)row[columnIndex];
                                        if (value == "NULL" || string.IsNullOrEmpty(value))
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
                                                    command.Parameters[i].Value = DBNull.Value;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (command.Parameters[i].DbType)
                                            {
                                                case DbType.Binary:
                                                    command.Parameters[i].Value = ToBytes(value);
                                                    break;
                                                case DbType.AnsiString:
                                                    command.Parameters[i].Value = value;
                                                    break;
                                                case DbType.Byte:
                                                    command.Parameters[i].Value = Convert.ToByte(value);
                                                    break;
                                                case DbType.Boolean:
                                                    command.Parameters[i].Value = Convert.ToBoolean(value);
                                                    break;
                                                case DbType.Currency:
                                                    command.Parameters[i].Value = Convert.ToDecimal(value);
                                                    break;
                                                case DbType.Date:
                                                    command.Parameters[i].Value = Convert.ToDateTime(value);
                                                    break;
                                                case DbType.DateTime:
                                                    command.Parameters[i].Value = Convert.ToDateTime(value);
                                                    break;
                                                case DbType.Decimal:
                                                    command.Parameters[i].Value = Convert.ToDecimal(value);
                                                    break;
                                                case DbType.Double:
                                                    command.Parameters[i].Value = Convert.ToDouble(value);
                                                    break;
                                                case DbType.Guid:
                                                    command.Parameters[i].Value = new Guid(value);
                                                    break;
                                                case DbType.Int16:
                                                    command.Parameters[i].Value = Convert.ToInt16(value);
                                                    break;
                                                case DbType.Int32:
                                                    command.Parameters[i].Value = Convert.ToInt32(value);
                                                    break;
                                                case DbType.Int64:
                                                    command.Parameters[i].Value = Convert.ToInt64(value);
                                                    break;
                                                case DbType.Object:
                                                    command.Parameters[i].Value = value;
                                                    break;
                                                case DbType.SByte:
                                                    command.Parameters[i].Value = Convert.ToSByte(value);
                                                    break;
                                                case DbType.Single:
                                                    command.Parameters[i].Value = Convert.ToDouble(value);
                                                    break;
                                                case DbType.String:
                                                    command.Parameters[i].Value = value;
                                                    break;
                                                case DbType.Time:
                                                    command.Parameters[i].Value = TimeSpan.Parse(value);
                                                    break;
                                                case DbType.UInt16:
                                                    command.Parameters[i].Value = Convert.ToUInt16(value);
                                                    break;
                                                case DbType.UInt32:
                                                    command.Parameters[i].Value = Convert.ToUInt32(value);
                                                    break;
                                                case DbType.UInt64:
                                                    command.Parameters[i].Value = Convert.ToUInt64(value);
                                                    break;
                                                case DbType.VarNumeric:
                                                    command.Parameters[i].Value = Convert.ToDecimal(value);
                                                    break;
                                                case DbType.AnsiStringFixedLength:
                                                    command.Parameters[i].Value = value;
                                                    break;
                                                case DbType.StringFixedLength:
                                                    command.Parameters[i].Value = value;
                                                    break;
                                                case DbType.Xml:
                                                    command.Parameters[i].Value = value;
                                                    break;
                                                case DbType.DateTime2:
                                                    command.Parameters[i].Value = Convert.ToDateTime(value);
                                                    break;
                                                case DbType.DateTimeOffset:
                                                    command.Parameters[i].Value = DateTimeOffset.Parse(value);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }

                                    command.ExecuteNonQuery();
                                }

                                transcation.Commit();
                            }
                        }
                    }

                    this.ShowMessage("Success", "Operation Successfully.");
                }
                catch (Exception exception)
                {
                    this.ShowMessage("Failure", exception.Message);
                }
            }


        }
    }
}
