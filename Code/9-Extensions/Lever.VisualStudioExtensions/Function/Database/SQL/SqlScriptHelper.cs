using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.Database.SQL
{
    public class SqlScriptHelper
    {
        public static string GenerateLoadTablePrimaryKeyScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($@"
SELECT
    c.name AS [ColumnName]
FROM
    sysindexes i
    JOIN sysindexkeys k ON i.id = k.id AND i.indid = k.indid
    JOIN sysobjects o ON i.id = o.id
    JOIN syscolumns c ON i.id = c.id AND k.colid = c.colid
WHERE
    o.xtype = 'U'
    AND o.name = '{tableName}'
    AND EXISTS (SELECT 1 FROM sysobjects o2 WHERE xtype = 'PK' AND o2.parent_obj = i.id AND o2.name = i.name)
ORDER BY
    c.colorder ASC");
            return builder.ToString();
        }

        public static string GenerateLoadTableColumnsScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($@"
SELECT
    c.[name] AS [Name],
    t.[name] AS [Type],
    c.[length] AS [Length],
    c.[xprec] AS [Precision],
    c.[xscale] AS [Scale],
    c.[isnullable] AS [Nullable],
    c.[collation] AS [Collation]
FROM
    syscolumns [c]
    LEFT JOIN systypes t ON c.[xtype] = t.[xusertype]
WHERE
    id = (SELECT Id FROM sysobjects o WHERE o.xtype = 'U' AND o.name = '{tableName}')
ORDER BY
    c.[colorder] ASC");
            return builder.ToString();
        }

        public static string GenerateLoadTempTableColumnsScript(string tempTableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($@"
 SELECT
    c.[name] AS [Name],
    t.[name] AS [Type],
    c.[length] AS [Length],
    c.[xprec] AS [Precision],
    c.[xscale] AS [Scale],
    c.[isnullable] AS [Nullable],
    c.[collation] AS [Collation]
FROM
    tempdb.dbo.syscolumns [c]
    LEFT JOIN tempdb.dbo.systypes t ON c.[xtype] = t.[xusertype]
WHERE
    id =  object_id('tempdb..{tempTableName}')
ORDER BY
    c.[colorder] ASC");
            return builder.ToString();
        }

        public static string GenerateCreateTableScript(string tableName, IList<SqlColumnInfo> columnInfos, bool createPrimaryKey, bool createIfNotExists = false)
        {
            StringBuilder builder = new StringBuilder();
            if (createIfNotExists)
            {
                builder.Append($"IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE [name] = '{tableName}' AND [xtype] = 'U') ");
            }
            builder.AppendLine($"CREATE TABLE [{tableName}](");
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];

                builder.Append("    ");
                builder.Append($"[{columnInfo.Name}]");
                builder.Append(" ");
                builder.Append($"[{columnInfo.Type}]");
                // Length & Precision & Scale 
                if (columnInfo.Length.HasValue || columnInfo.Precision.HasValue || columnInfo.Scale.HasValue)
                {
                    builder.Append("(");
                    List<string> segments = new List<string>();
                    if (columnInfo.Length.HasValue)
                    {
                        segments.Add((columnInfo.Length > 0) ? columnInfo.Length.ToString() : "MAX");
                    }
                    if (columnInfo.Precision.HasValue)
                    {
                        segments.Add(columnInfo.Precision.ToString());
                    }
                    if (columnInfo.Scale.HasValue)
                    {
                        segments.Add(columnInfo.Scale.ToString());
                    }
                    builder.Append(string.Join(", ", segments.ToArray()));
                    builder.Append(")");
                }
                // Collation
                if (columnInfo.Collation != null)
                {
                    builder.Append(" COLLATE ");
                    builder.Append(columnInfo.Collation);
                }
                // Nullable
                builder.Append(" ");
                if (createPrimaryKey)
                {
                    builder.Append(columnInfo.Nullable ? "NULL" : "NOT NULL");
                }
                else
                {
                    builder.Append("NULL");
                }

                if (i < columnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
            }
            // Primarykey
            if (createPrimaryKey)
            {
                SqlColumnInfo[] primaryKeyColumnInfos = columnInfos.Where(p => p.IsPrimaryKey).ToArray();
                if (primaryKeyColumnInfos.Length > 0)
                {
                    builder.AppendLine(",");
                    builder.Append("    ");
                    string keylist = string.Join(",", primaryKeyColumnInfos.Select(p => $"[{p.Name}]"));
                    builder.Append($"CONSTRAINT [PK_{tableName}] PRIMARY KEY ({keylist})");
                }
            }

            builder.Append(")");
            return builder.ToString();
        }

        public static string GenerateGetTempTableColumnsScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"SELECT [name] FROM tempdb.sys.columns WHERE [object_id] = OBJECT_ID('[tempdb]..[{tableName}]', 'U')");
            return builder.ToString();
        }

        public static string GenerateDropTempTableScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"IF OBJECT_ID('[tempdb]..[{tableName}]', 'U') IS NOT NULL DROP TABLE [{tableName}]");
            return builder.ToString();
        }

        public static string GenerateDropColumnsScript(string tableName, IList<SqlColumnInfo> columnInfos)
        {
            if (tableName == null)
            {
                throw new ArgumentNullException("tableName");
            }
            if (columnInfos == null)
            {
                throw new ArgumentNullException("columnInfos");
            }
            if (columnInfos.Count == 0)
            {
                throw new ArgumentException("columnInfos could not be empty.", "columnInfos");
            }

            StringBuilder builder = new StringBuilder();
            string columnList = string.Join(", ", columnInfos.Select(p => $"[{p.Name}]"));
            builder.AppendLine($"ALTER TABLE [{tableName}] DROP COLUMN {columnList}");
            return builder.ToString();
        }

        public static string GenerateMergeScript(string sourceTableName, IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            if (sourceColumnInfos == null)
            {
                throw new ArgumentNullException("sourceColumnInfos");
            }
            if (destinationTableName == null)
            {
                throw new ArgumentNullException("destinationTableName");
            }
            if (sourceColumnInfos.Count == 0)
            {
                return null;
            }

            // Exclude timestamp column
            List<SqlColumnInfo> columnInfos = sourceColumnInfos.Where(p => p.Type != SqlColumnType.@timestamp).ToList();
            List<SqlColumnInfo> primaryKeyColumnInfos = columnInfos.Where(p => p.IsPrimaryKey).ToList();
            List<SqlColumnInfo> nonPrimaryKeyColumnInfos = columnInfos.Where(p => !p.IsPrimaryKey).ToList();

            StringBuilder builder = new StringBuilder();
            // Merge
            builder.AppendLine($"MERGE [{destinationTableName}] AS [Destination]");
            // Using
            if (sourceTableName != null)
            {
                builder.AppendLine($"USING [{sourceTableName}] AS [Source]");
            }
            else
            {
                string parameterList = string.Join(", ", columnInfos.Select(p => "@{p.Name}"));
                string columnList = string.Join(", ", columnInfos.Select(p => "[{p.Name}]"));
                builder.AppendLine($"USING (SELECT {parameterList}) AS [Source]({columnList})");
            }
            // On
            builder.Append("ON (");
            for (int i = 0; i < primaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo primaryKeyColumnInfo = primaryKeyColumnInfos[i];
                builder.Append($"[Source].[{primaryKeyColumnInfo.Name}] = [Destination].[{primaryKeyColumnInfo.Name}]");

                if (i < primaryKeyColumnInfos.Count - 1)
                {
                    builder.Append(" AND ");
                }
                else
                {
                    builder.Append(")");
                }
            }
            builder.AppendLine();
            // match
            builder.AppendLine("WHEN matched THEN");
            builder.AppendLine("    UPDATE SET");
            for (int i = 0; i < nonPrimaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo nonPrimaryKeyColumnInfo = nonPrimaryKeyColumnInfos[i];
#if HealthCareCDR // HealthCare special threatment: remove FirstInsertDateTime which for first insert only
                if (nonPrimaryKeyColumnInfo.Name.Equals("FirstInsertDateTime", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
#endif
                builder.Append($"        [{nonPrimaryKeyColumnInfo.Name}] = [Source].[{nonPrimaryKeyColumnInfo.Name}]");
                if (i < nonPrimaryKeyColumnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine();
                }
            }
            // not match
            builder.AppendLine("WHEN not matched THEN");
            builder.AppendLine("    INSERT(");
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];
                builder.Append($"        [{columnInfo.Name}]");
                if (i < columnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine(")");
                }
            }
            builder.AppendLine("    VALUES(");
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];
                builder.Append($"        [Source].[{columnInfo.Name}]");
                if (i < columnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine(");");
                }
            }

            return builder.ToString();
        }

        public static string GenerateUpdateFormScript(string sourceTableName, IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            if (sourceTableName == null)
            {
                throw new ArgumentNullException("sourceTableName");
            }
            if (sourceColumnInfos == null)
            {
                throw new ArgumentNullException("sourceColumnInfos");
            }
            if (destinationTableName == null)
            {
                throw new ArgumentNullException("destinationTableName");
            }
            if (sourceColumnInfos.Count == 0)
            {
                return null;
            }

            // Exclude timestamp column
            List<SqlColumnInfo> columnInfos = sourceColumnInfos.Where(p => p.Type != SqlColumnType.@timestamp).ToList();
            List<SqlColumnInfo> primaryKeyColumnInfos = columnInfos.Where(p => p.IsPrimaryKey).ToList();
            List<SqlColumnInfo> nonPrimaryKeyColumnInfos = columnInfos.Where(p => !p.IsPrimaryKey).ToList();

            StringBuilder builder = new StringBuilder();
            // UPDATE 
            //     [Destination] 
            // SET
            builder.AppendLine("UPDATE");
            builder.AppendLine($"    [{destinationTableName}]");
            builder.AppendLine("SET");
            // [Destination].N1 = [Source].N1, 
            // [Destination].N2 = [Source].N2, 
            // .... 
            // [Destination].N* = [Source].N*
            for (int i = 0; i < nonPrimaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo nonPrimaryKeyColumnInfo = nonPrimaryKeyColumnInfos[i];
                builder.Append($"    [{destinationTableName}].[{nonPrimaryKeyColumnInfo.Name}] = [{sourceTableName}].[{nonPrimaryKeyColumnInfo.Name}]");

                if (i < nonPrimaryKeyColumnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine();
                }
            }
            // FROM
            //     [Source]
            // WHERE
            builder.AppendLine("FROM");
            builder.AppendLine($"    [{sourceTableName}]");
            builder.AppendLine("WHERE");
            // [Destination].K1 = [Source].K1 AND [Destination].K2 = [Source].K2 AND .... [Destination].K* = [Source].K*
            for (int i = 0; i < primaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo primaryKeyColumnInfo = primaryKeyColumnInfos[i];
                builder.Append($"    [{destinationTableName}].[{primaryKeyColumnInfo.Name}] = [{sourceTableName}].[{primaryKeyColumnInfo.Name}]");

                if (i < primaryKeyColumnInfos.Count - 1)
                {
                    builder.Append(" AND ");
                }
                else
                {
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
        
        public static string GenerateInsertIntoScript(string sourceTableName, IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            if (sourceColumnInfos == null)
            {
                throw new ArgumentNullException("sourceColumnInfos");
            }
            if (destinationTableName == null)
            {
                throw new ArgumentNullException("destinationTableName");
            }
            if (sourceColumnInfos.Count == 0)
            {
                return null;
            }

            // Exclude timestamp column
            List<SqlColumnInfo> columnInfos = sourceColumnInfos.Where(p => p.Type != SqlColumnType.@timestamp).ToList();
            List<SqlColumnInfo> primaryKeyColumnInfos = columnInfos.Where(p => p.IsPrimaryKey).ToList();
            List<SqlColumnInfo> nonPrimaryKeyColumnInfos = columnInfos.Where(p => !p.IsPrimaryKey).ToList();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"INSERT INTO [{destinationTableName}](");
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];
                builder.Append($"    [{columnInfo.Name}]");
                if (i < columnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine(")");
                }
            }
            builder.AppendLine("SELECT");
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];
                builder.Append($"    [{columnInfo.Name}]");
                if (i < columnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine("");
                }
            }
            builder.AppendLine($"FROM [{sourceTableName}]");

            return builder.ToString();
        }

        public static string GenerateInsertScript(IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            if (sourceColumnInfos == null)
            {
                throw new ArgumentNullException("sourceColumnInfos");
            }
            if (destinationTableName == null)
            {
                throw new ArgumentNullException("destinationTableName");
            }
            if (sourceColumnInfos.Count == 0)
            {
                return null;
            }

            // Exclude timestamp column
            List<SqlColumnInfo> columnInfos = sourceColumnInfos.Where(p => p.Type != SqlColumnType.@timestamp).ToList();
            
            StringBuilder builder = new StringBuilder();
            string columnList = string.Join(", ", columnInfos.Select(p => $"[{p.Name}]"));
            builder.Append($"INSERT INTO [{destinationTableName}]({columnList})");
            builder.Append(" ");
            string parameterList = string.Join(", ", columnInfos.Select(p => $"@{p.Name}"));
            builder.Append($"VALUES({parameterList})");
            return builder.ToString();
        }

        public static string GenerateInsertIfNotExistsScript(IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
        {
            if (sourceColumnInfos == null)
            {
                throw new ArgumentNullException("sourceColumnInfos");
            }
            if (destinationTableName == null)
            {
                throw new ArgumentNullException("destinationTableName");
            }
            if (sourceColumnInfos.Count == 0)
            {
                return null;
            }

            // Exclude timestamp column
            List<SqlColumnInfo> columnInfos = sourceColumnInfos.Where(p => p.Type != SqlColumnType.@timestamp).ToList();
            List<SqlColumnInfo> primaryKeyColumnInfos = columnInfos.Where(p => p.IsPrimaryKey).ToList();

            StringBuilder builder = new StringBuilder();
            string columnList = string.Join(", ", columnInfos.Select(p => $"[{p.Name}]"));
            builder.Append($"INSERT INTO [{destinationTableName}]({columnList})");
            builder.Append(" ");
            string parameterList = string.Join(", ", columnInfos.Select(p => $"@{p.Name}"));
            builder.Append($"SELECT {parameterList}");
            string whereList = string.Join(" AND ", primaryKeyColumnInfos.Select(p => $"[{p.Name}] = @{p.Name}"));
            builder.Append($" WHERE NOT EXISTS (SELECT 1 FROM [{destinationTableName}] WHERE {whereList})");
            return builder.ToString();
        }

        public static string GenerateTruncateTableScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"TRUNCATE TABLE [{tableName}]");
            return builder.ToString();
        }

        public static string GenerateCountTableScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"SELECT COUNT(*) FROM [{tableName}]");
            return builder.ToString();
        }
    }
}
