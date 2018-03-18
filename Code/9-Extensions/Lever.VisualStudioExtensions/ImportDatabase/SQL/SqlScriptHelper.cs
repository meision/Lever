using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meision.Text;

namespace Meision.Database.SQL
{
    public class SqlScriptHelper
    {
        public static string GenerateLoadTablePrimaryKeyScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"
SELECT
    c.name AS [ColumnName]
FROM
    sysindexes i
    JOIN sysindexkeys k ON i.id = k.id AND i.indid = k.indid
    JOIN sysobjects o ON i.id = o.id
    JOIN syscolumns c ON i.id = c.id AND k.colid = c.colid
WHERE
    o.xtype = 'U'
    AND o.name = '{0}'
    AND EXISTS (SELECT 1 FROM sysobjects o2 WHERE xtype = 'PK' AND o2.parent_obj = i.id AND o2.name = i.name)
ORDER BY
    c.colorder ASC
".FormatWith(tableName));
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
    id = (SELECT Id FROM sysobjects o WHERE o.xtype = 'U' AND o.name = '{0}')
ORDER BY
    c.[colorder] ASC
".FormatWith(tableName));
            return builder.ToString();
        }

        public static string GenerateLoadTempTableColumnsScript(string tempTableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"
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
    id =  object_id('tempdb..{0}')
ORDER BY
    c.[colorder] ASC
".FormatWith(tempTableName));
            return builder.ToString();
        }

        public static string GenerateCreateTableScript(string tableName, IList<SqlColumnInfo> columnInfos, bool createPrimaryKey, bool createIfNotExists = false)
        {
            StringBuilder builder = new StringBuilder();
            if (createIfNotExists)
            {
                builder.Append("IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE [name] = '{0}' AND [xtype] = 'U') ".FormatWith(tableName));
            }
            builder.AppendLine("CREATE TABLE [{0}](".FormatWith(tableName));
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];

                builder.Append("    ");
                builder.Append("[{0}]".FormatWith(columnInfo.Name));
                builder.Append(" ");
                builder.Append("[{0}]".FormatWith(columnInfo.Type));
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
                    builder.Append("CONSTRAINT [PK_{0}] PRIMARY KEY ({1})".FormatWith(
                        tableName,
                        string.Join(",", primaryKeyColumnInfos.Select(p => "[{0}]".FormatWith(p.Name)).ToArray())));
                }
            }

            builder.Append(")");
            return builder.ToString();
        }

        public static string GenerateGetTempTableColumnsScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT [name] FROM tempdb.sys.columns WHERE [object_id] = OBJECT_ID('[tempdb]..[{0}]', 'U')".FormatWith(tableName));
            return builder.ToString();
        }

        public static string GenerateDropTempTableScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("IF OBJECT_ID('[tempdb]..[{0}]', 'U') IS NOT NULL DROP TABLE [{0}]".FormatWith(tableName));
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
            builder.AppendLine("ALTER TABLE [{0}] DROP COLUMN {1}".FormatWith(
                tableName,
                string.Join(", ", columnInfos.Select(p => "[{0}]".FormatWith(p.Name)))));
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
            builder.AppendLine("MERGE [{0}] AS [Destination]".FormatWith(destinationTableName));
            // Using
            if (sourceTableName != null)
            {
                builder.AppendLine("USING [{0}] AS [Source]".FormatWith(sourceTableName));
            }
            else
            {
                builder.AppendLine("USING (SELECT {0}) AS [Source]({1})".FormatWith(
                    string.Join(", ", columnInfos.Select(p => "@{0}".FormatWith(p.Name))),
                    string.Join(", ", columnInfos.Select(p => "[{0}]".FormatWith(p.Name)))));
            }
            // On
            builder.Append("ON (");
            for (int i = 0; i < primaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo primaryKeyColumnInfo = primaryKeyColumnInfos[i];
                builder.Append("[Source].[{0}] = [Destination].[{0}]".FormatWith(primaryKeyColumnInfo.Name));

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
                builder.Append("        [{0}] = [Source].[{0}]".FormatWith(nonPrimaryKeyColumnInfo.Name));
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
                builder.Append("        [{0}]".FormatWith(columnInfo.Name));
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
                builder.Append("        [Source].[{0}]".FormatWith(columnInfo.Name));
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
            builder.AppendLine("    [{0}]".FormatWith(destinationTableName));
            builder.AppendLine("SET");
            // [Destination].N1 = [Source].N1, 
            // [Destination].N2 = [Source].N2, 
            // .... 
            // [Destination].N* = [Source].N*
            for (int i = 0; i < nonPrimaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo nonPrimaryKeyColumnInfo = nonPrimaryKeyColumnInfos[i];
#if HealthCareCDR // HealthCare special threatment: remove FirstInsertDateTime which for first insert only
                if (nonPrimaryKeyColumnInfo.Name.Equals("FirstInsertDateTime", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
#endif
                builder.Append("    [{0}].[{2}] = [{1}].[{2}]".FormatWith(destinationTableName, sourceTableName, nonPrimaryKeyColumnInfo.Name));

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
            builder.AppendLine("    [{0}]".FormatWith(sourceTableName));
            builder.AppendLine("WHERE");
            // [Destination].K1 = [Source].K1 AND [Destination].K2 = [Source].K2 AND .... [Destination].K* = [Source].K*
            for (int i = 0; i < primaryKeyColumnInfos.Count; i++)
            {
                SqlColumnInfo primaryKeyColumnInfo = primaryKeyColumnInfos[i];
                builder.Append("    [{0}].[{2}] = [{1}].[{2}]".FormatWith(destinationTableName, sourceTableName, primaryKeyColumnInfo.Name));

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

        public static string GenerateInsertScript(string sourceTableName, IList<SqlColumnInfo> sourceColumnInfos, string destinationTableName)
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
            builder.AppendLine("INSERT INTO {0}(".FormatWith(destinationTableName));
            for (int i = 0; i < columnInfos.Count; i++)
            {
                SqlColumnInfo columnInfo = columnInfos[i];
                builder.Append("    [{0}]".FormatWith(columnInfo.Name));
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
                builder.Append("    [{0}]".FormatWith(columnInfo.Name));
                if (i < columnInfos.Count - 1)
                {
                    builder.AppendLine(",");
                }
                else
                {
                    builder.AppendLine("");
                }
            }
            builder.AppendLine("FROM [{0}]".FormatWith(sourceTableName));

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
            builder.Append("INSERT INTO [{0}]({1})".FormatWith(destinationTableName, string.Join(", ", columnInfos.Select(p => "[{0}]".FormatWith(p.Name)))));
            builder.Append(" ");
            builder.Append("SELECT {0}".FormatWith(string.Join(", ", columnInfos.Select(p => "@{0}".FormatWith(p.Name)))));
            builder.Append(" WHERE NOT EXISTS (SELECT 1 FROM [{0}] WHERE {1})".FormatWith(destinationTableName, string.Join(" AND ", primaryKeyColumnInfos.Select(p => "[{0}] = @{0}".FormatWith(p.Name)))));
            return builder.ToString();
        }

        public static string GenerateTruncateTableScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("TRUNCATE TABLE [{0}]".FormatWith(tableName));
            return builder.ToString();
        }

        public static string GenerateCountTableScript(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT COUNT(*) FROM [{0}]".FormatWith(tableName));
            return builder.ToString();
        }
    }
}
