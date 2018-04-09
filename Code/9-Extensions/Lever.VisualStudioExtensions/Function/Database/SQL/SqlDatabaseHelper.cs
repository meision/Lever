using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.Database.SQL
{
    public class SqlDatabaseHelper
    {
        public static SqlDbType GetDbType(SqlColumnType type)
        {
            switch (type)
            {
                case SqlColumnType.@image:
                    return SqlDbType.Image;
                case SqlColumnType.@text:
                    return SqlDbType.Text;
                case SqlColumnType.@uniqueidentifier:
                    return SqlDbType.UniqueIdentifier;
                case SqlColumnType.@date:
                    return SqlDbType.Date;
                case SqlColumnType.@time:
                    return SqlDbType.Time;
                case SqlColumnType.@datetime2:
                    return SqlDbType.DateTime2;
                case SqlColumnType.@datetimeoffset:
                    return SqlDbType.DateTimeOffset;
                case SqlColumnType.@tinyint:
                    return SqlDbType.TinyInt;
                case SqlColumnType.@smallint:
                    return SqlDbType.SmallInt;
                case SqlColumnType.@int:
                    return SqlDbType.Int;
                case SqlColumnType.@smalldatetime:
                    return SqlDbType.SmallDateTime;
                case SqlColumnType.@real:
                    return SqlDbType.Real;
                case SqlColumnType.@money:
                    return SqlDbType.Money;
                case SqlColumnType.@datetime:
                    return SqlDbType.DateTime;
                case SqlColumnType.@float:
                    return SqlDbType.Float;
                case SqlColumnType.@sql_variant:
                    return SqlDbType.Variant;
                case SqlColumnType.@ntext:
                    return SqlDbType.NText;
                case SqlColumnType.@bit:
                    return SqlDbType.Bit;
                case SqlColumnType.@decimal:
                    return SqlDbType.Decimal;
                case SqlColumnType.@numeric:
                    return SqlDbType.Decimal;
                case SqlColumnType.@smallmoney:
                    return SqlDbType.SmallMoney;
                case SqlColumnType.@bigint:
                    return SqlDbType.BigInt;
                case SqlColumnType.@hierarchyid:
                    throw new ArgumentException();
                case SqlColumnType.@geometry:
                    throw new ArgumentException();
                case SqlColumnType.@geography:
                    throw new ArgumentException();
                case SqlColumnType.@varbinary:
                    return SqlDbType.VarBinary;
                case SqlColumnType.@varchar:
                    return SqlDbType.VarChar;
                case SqlColumnType.@binary:
                    return SqlDbType.Binary;
                case SqlColumnType.@char:
                    return SqlDbType.Char;
                case SqlColumnType.@timestamp:
                    return SqlDbType.Timestamp;
                case SqlColumnType.@nvarchar:
                    return SqlDbType.NVarChar;
                case SqlColumnType.@nchar:
                    return SqlDbType.NChar;
                case SqlColumnType.@xml:
                    return SqlDbType.Xml;
                case SqlColumnType.@sysname:
                    throw new ArgumentException();
                default:
                    throw new ArgumentException();
            }
        }

        public static SqlColumnInfo[] GetColumnInfos(SqlConnection connection, string tableName)
        {
            bool isTempTable = tableName.StartsWith("#");

            // Get Primary key
            List<string> primaryKeys = new List<string>();
            if (!isTempTable)
            {
                using (SqlCommand loadPrimaryKeysCommand = connection.CreateCommand())
                {
                    loadPrimaryKeysCommand.CommandText = SqlScriptHelper.GenerateLoadTablePrimaryKeyScript(tableName);
                    using (SqlDataReader reader = loadPrimaryKeysCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            primaryKeys.Add(Convert.ToString(reader[0]));
                        }
                    }
                }
            }

            // Get Column Info
            List<SqlColumnInfo> columnInfos = new List<SqlColumnInfo>();
            using (SqlCommand loadColumnInfosCommand = connection.CreateCommand())
            {
                if (!isTempTable)
                {
                    loadColumnInfosCommand.CommandText = SqlScriptHelper.GenerateLoadTableColumnsScript(tableName);
                }
                else
                {
                    loadColumnInfosCommand.CommandText = SqlScriptHelper.GenerateLoadTempTableColumnsScript(tableName);
                }
                using (SqlDataReader reader = loadColumnInfosCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SqlColumnInfo columnInfo = new SqlColumnInfo();
                        columnInfo.Name = Convert.ToString(reader["Name"]);
                        columnInfo.Type = (SqlColumnType)Enum.Parse(typeof(SqlColumnType), Convert.ToString(reader["Type"]));
                        switch (columnInfo.Type)
                        {
                            // None
                            case SqlColumnType.@image:
                            case SqlColumnType.@text:
                            case SqlColumnType.@uniqueidentifier:
                            case SqlColumnType.@date:
                            case SqlColumnType.@tinyint:
                            case SqlColumnType.@smallint:
                            case SqlColumnType.@int:
                            case SqlColumnType.@smalldatetime:
                            case SqlColumnType.@real:
                            case SqlColumnType.@money:
                            case SqlColumnType.@datetime:
                            case SqlColumnType.@float:
                            case SqlColumnType.@sql_variant:
                            case SqlColumnType.@ntext:
                            case SqlColumnType.@bit:
                            case SqlColumnType.@smallmoney:
                            case SqlColumnType.@bigint:
                            case SqlColumnType.@hierarchyid:
                            case SqlColumnType.@geometry:
                            case SqlColumnType.@geography:
                            case SqlColumnType.@timestamp:
                            case SqlColumnType.@xml:
                                break;

                            // percious & scale                                    
                            case SqlColumnType.@decimal:
                            case SqlColumnType.@numeric:
                                {
                                    columnInfo.Precision = Convert.ToInt32(reader["Precision"]);
                                    columnInfo.Scale = Convert.ToInt32(reader["Scale"]);
                                }
                                break;

                            // Scale
                            case SqlColumnType.@time:
                            case SqlColumnType.@datetime2:
                            case SqlColumnType.@datetimeoffset:
                                {
                                    columnInfo.Scale = Convert.ToInt32(reader["Scale"]);
                                }
                                break;


                            // Unicode char
                            case SqlColumnType.@nvarchar:
                            case SqlColumnType.@nchar:
                                {
                                    columnInfo.Length = Convert.ToInt32(reader["Length"]);
                                    if (columnInfo.Length > 0)
                                    {
                                        columnInfo.Length /= 2;
                                    }
                                }
                                break;

                            // Ansi char
                            case SqlColumnType.@varchar:
                            case SqlColumnType.@char:
                                {
                                    columnInfo.Length = Convert.ToInt32(reader["Length"]);
                                }
                                break;

                            // Binary
                            case SqlColumnType.@varbinary:
                            case SqlColumnType.@binary:
                                {
                                    columnInfo.Length = Convert.ToInt32(reader["Length"]);
                                }
                                break;

                            // Other
                            case SqlColumnType.@sysname:
                                break;
                            default:
                                break;
                        }

                        columnInfo.IsPrimaryKey = primaryKeys.Any(p => p.Equals(Convert.ToString(reader["Name"]), StringComparison.OrdinalIgnoreCase));
                        columnInfo.Nullable = Convert.ToBoolean(reader["Nullable"]);
                        columnInfo.Collation = !Convert.IsDBNull(reader["Collation"]) ? Convert.ToString(reader["Collation"]) : null;
                        columnInfos.Add(columnInfo);
                    }
                }
            }

            return columnInfos.ToArray();
        }

        public static SqlCommand GetMergeCommand(SqlConnection connection, IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = SqlScriptHelper.GenerateMergeScript(null, sourceColumnInfos, destinationTableName);
            foreach (SqlColumnInfo sourceColumnInfo in sourceColumnInfos)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = @"{sourceColumnInfo.Name}";
                parameter.SqlDbType = SqlDatabaseHelper.GetDbType(sourceColumnInfo.Type);
                command.Parameters.Add(parameter);
            }
            return command;
        }

        public static SqlCommand GetInsertIfNotExistCommand(SqlConnection connection, IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = SqlScriptHelper.GenerateInsertIfNotExistsScript(sourceColumnInfos, destinationTableName);
            foreach (SqlColumnInfo sourceColumnInfo in sourceColumnInfos)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = $"@{sourceColumnInfo.Name}";
                parameter.SqlDbType = SqlDatabaseHelper.GetDbType(sourceColumnInfo.Type);
                command.Parameters.Add(parameter);
            }
            return command;
        }
    }
}
