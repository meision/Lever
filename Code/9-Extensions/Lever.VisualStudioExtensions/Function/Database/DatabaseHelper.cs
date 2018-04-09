using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.Database
{
    public static class DatabaseHelper
    {
        #region Static
        public const string Separator = ",";
        public const string Accessor = ".";
        public const string Blank = " ";
        public const string Relation = "_";
        public const string Connect = "-";

        private static readonly Dictionary<SqlColumnType, DbType> __SqlColumnTypeDbType = new Dictionary<SqlColumnType, DbType>()
        {
            {SqlColumnType.@image, DbType.Binary},
            {SqlColumnType.@text, DbType.AnsiString},
            {SqlColumnType.@uniqueidentifier, DbType.Guid},
            {SqlColumnType.@date, DbType.Date},
            {SqlColumnType.@time, DbType.Time},
            {SqlColumnType.@datetime2, DbType.DateTime2},
            {SqlColumnType.@datetimeoffset, DbType.DateTimeOffset},
            {SqlColumnType.@tinyint, DbType.Byte},
            {SqlColumnType.@smallint, DbType.Int16},
            {SqlColumnType.@int, DbType.Int32},
            {SqlColumnType.@smalldatetime, DbType.DateTime},
            {SqlColumnType.@real, DbType.Single},
            {SqlColumnType.@money, DbType.Currency},
            {SqlColumnType.@datetime, DbType.DateTime},
            {SqlColumnType.@float, DbType.Double},
            {SqlColumnType.@ntext, DbType.String},
            {SqlColumnType.@bit, DbType.Boolean},
            {SqlColumnType.@decimal, DbType.Decimal},
            {SqlColumnType.@numeric, DbType.Decimal},
            {SqlColumnType.@smallmoney, DbType.Currency},
            {SqlColumnType.@bigint, DbType.Int64},
            {SqlColumnType.@varbinary, DbType.Binary},
            {SqlColumnType.@varchar, DbType.AnsiString},
            {SqlColumnType.@binary, DbType.Binary},
            {SqlColumnType.@char, DbType.AnsiStringFixedLength},
            {SqlColumnType.@timestamp, DbType.Binary},
            {SqlColumnType.@nvarchar, DbType.String},
            {SqlColumnType.@nchar, DbType.StringFixedLength},
            {SqlColumnType.@xml, DbType.Xml},
        };

        private static readonly Dictionary<DbType, Tuple<string, string>> __DbTypeString = new Dictionary<DbType, Tuple<string, string>>()
        {
            {DbType.AnsiString, new Tuple<string, string>("string", "string")},
            {DbType.AnsiStringFixedLength, new Tuple<string, string>("string", "string")},
            {DbType.Binary, new Tuple<string, string>("byte[]", "byte[]")},
            {DbType.Boolean, new Tuple<string, string>("bool", "bool?")},
            {DbType.Byte, new Tuple<string, string>("byte", "byte?")},
            {DbType.Currency, new Tuple<string, string>("decimal", "decimal?")},
            {DbType.Date, new Tuple<string, string>("DateTime", "DateTime?")},
            {DbType.DateTime, new Tuple<string, string>("DateTime", "DateTime?")},
            {DbType.DateTime2, new Tuple<string, string>("DateTime", "DateTime?")},
            {DbType.DateTimeOffset, new Tuple<string, string>("DateTimeOffset", "DateTimeOffset?")},
            {DbType.Decimal, new Tuple<string, string>("decimal", "decimal?")},
            {DbType.Double, new Tuple<string, string>("double", "double?")},
            {DbType.Guid, new Tuple<string, string>("Guid", "Guid?")},
            {DbType.Int16, new Tuple<string, string>("short", "short?")},
            {DbType.Int32, new Tuple<string, string>("int", "int?")},
            {DbType.Int64, new Tuple<string, string>("long", "long?")},
            {DbType.Object, new Tuple<string, string>("object", "object")},
            {DbType.SByte, new Tuple<string, string>("sbyte", "sbyte?")},
            {DbType.Single, new Tuple<string, string>("float", "float?")},
            {DbType.String, new Tuple<string, string>("string", "string")},
            {DbType.StringFixedLength, new Tuple<string, string>("string", "string")},
            {DbType.Time, new Tuple<string, string>("TimeSpan", "TimeSpan?")},
            {DbType.UInt16, new Tuple<string, string>("ushort", "ushort?")},
            {DbType.UInt32, new Tuple<string, string>("uint", "uint?")},
            {DbType.UInt64, new Tuple<string, string>("ulong", "ulong?")},
            {DbType.Xml, new Tuple<string, string>("string", "string")},
        };
        #endregion Static

        public static DbType GetDbType(SqlColumnType type)
        {
            if (!DatabaseHelper.__SqlColumnTypeDbType.ContainsKey(type))
            {
                throw new NotSupportedException();
            }

            return DatabaseHelper.__SqlColumnTypeDbType[type];
        }

        public static string GetCLRTypeString(DbType type, bool nullable)
        {
            if (!DatabaseHelper.__DbTypeString.ContainsKey(type))
            {
                throw new NotSupportedException();
            }

            Tuple<string, string> pair = DatabaseHelper.__DbTypeString[type];
            return !nullable ? pair.Item1 : pair.Item2;
        }

        private static string GenerateLoadTablesOrViewsScript(string xtype)
        {
            string text = $@"
SELECT
    [Schema] = s.name,
    [Name] = o.name,
    [Description] = p.value
FROM
    sys.schemas s
    INNER JOIN sys.sysobjects o ON s.schema_id = o.uid
    LEFT JOIN sys.extended_properties p ON o.id = p.major_id AND p.minor_id = 0 AND p.name = 'MS_Description'
WHERE
    o.xtype = '{xtype}'
ORDER BY
    [Schema] ASC,
    [Name] ASC
";
            return text;
        }
        public static string GenerateLoadTablesScript()
        {
            return DatabaseHelper.GenerateLoadTablesOrViewsScript("U");
        }
        public static string GenerateLoadViewsScript()
        {
            return DatabaseHelper.GenerateLoadTablesOrViewsScript("V");
        }

        private static string GenerateLoadDataColumnsScript(string xtype)
        {
            string text = $@"
SELECT
    [Schema] = s.name,
    [TableName] = o.name,
    [ColumnName] = c.name,
    [Sequence] = c.colorder,
    [Type] = t.name,
    [Length] = c.length,
    [Precision] = c.prec,
    [Scale] = c.scale,
    [Nullable] = c.isnullable,
    [Collation] = c.collation,
    [DefaultValue] = m.text,
    [Description] = p.value
FROM 
    sys.schemas s
    INNER JOIN sys.sysobjects o ON s.schema_id = o.uid
    INNER JOIN sys.syscolumns c ON o.id = c.id     
    LEFT JOIN sys.systypes t ON c.xtype = t.xusertype
    LEFT JOIN sys.syscomments m ON c.cdefault = m.id
    LEFT JOIN sys.extended_properties p ON c.id = p.major_id AND c.colid = p.minor_id AND p.name = 'MS_Description'
WHERE
    o.xtype = '{xtype}'
ORDER BY
    [Schema] ASC,
    [TableName] ASC, 
    [Sequence] ASC
";
            return text;
        }
        public static string GenerateLoadTableColumnsScript()
        {
            return DatabaseHelper.GenerateLoadDataColumnsScript("U");
        }
        public static string GenerateLoadViewColumnsScript()
        {
            return DatabaseHelper.GenerateLoadDataColumnsScript("V");
        }

        public static string GenerateLoadIdentitiesScript()
        {
            string text = @"
SELECT
    [Schema] = s.name,
    [TableName] = o.name,
    [ColumnName] = i.name,
    [Seed] = i.seed_value,
    [Increment] = i.increment_value
FROM
    sys.schemas s
    INNER JOIN sys.sysobjects o ON s.schema_id = o.uid
    INNER JOIN sys.identity_columns i ON o.id = i.object_id
WHERE
    o.xtype = 'U'
";
            return text;
        }

        public static string GenerateLoadIndicesScript()
        {
            string text = @"
SELECT
    [Schema] = s.name,
    [TableName] = o.name,
    [IndexName] = i.name,
    [ColumnName] = c.name,
    [Sequence] = k.keyno,
    [IsDescending] = INDEXKEY_PROPERTY(o.id, k.indid, k.keyno, 'IsDescending'),
    [IsPrimaryKey] = CASE WHEN EXISTS(SELECT 1 FROM sys.sysobjects WHERE name = i.name AND xtype = 'PK') THEN 1 ELSE 0 END,
    [IsUniqueConstraint] = CASE WHEN EXISTS(SELECT 1 FROM sys.sysobjects WHERE name = i.name AND xtype = 'UQ') THEN 1 ELSE 0 END,
    [IsClustered] = INDEXPROPERTY(o.id, i.name, 'IsClustered'),
    [IsUnique] = INDEXPROPERTY(o.id, i.name, 'IsUnique')
FROM
    sys.schemas s
    INNER JOIN sys.sysobjects o ON s.schema_id = o.uid
    INNER JOIN sys.sysindexes i ON i.id = o.id
    INNER JOIN sys.sysindexkeys k ON i.id = k.id AND i.indid = k.indid
    INNER JOIN sys.syscolumns c ON k.id = c.id AND k.colid = c.colid
WHERE
    o.xtype = 'U'
ORDER BY
    [Schema] ASC,
    [TableName] ASC,
    [IndexName] ASC,
    [Sequence] ASC
";
            return text;
        }

        public static string GenerateLoadForeignKeysScript()
        {
            string text = @"
SELECT
    [ForeignKeyName] = o.name,
    [PrincipalTableSchema] = OBJECT_SCHEMA_NAME(f.rkeyid),
    [PrincipalTableName] = OBJECT_NAME(f.rkeyid),
    [PrincipalColumnName] = (SELECT name FROM sys.syscolumns WHERE colid=f.rkey AND id=f.rkeyid),
    [DependentTableSchema] = OBJECT_SCHEMA_NAME(f.fkeyid),
    [DependentTableName] = OBJECT_NAME(f.fkeyid),
    [DependentColumnName] = (SELECT name FROM sys.syscolumns WHERE colid = f.fkey AND id = f.fkeyid),
    [Sequence] = f.keyno,
    [UpdateCascade] = OBJECTPROPERTY(o.id,'CnstIsUpdateCascade'),
    [DeleteCascade] = OBJECTPROPERTY(o.id,'CnstIsDeleteCascade') 
FROM
    sys.sysobjects o
    INNER JOIN sys.sysforeignkeys f ON f.constid = o.id
WHERE
    o.xtype = 'F'
ORDER BY   
    [ForeignKeyName] ASC,
    [Sequence] ASC
";
            return text;
        }
    }
}
