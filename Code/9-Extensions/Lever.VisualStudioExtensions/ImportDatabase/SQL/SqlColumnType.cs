using System;

namespace Meision.Database.SQL
{
    public enum SqlColumnType
    {
        @image = 34,
        @text = 35,
        @uniqueidentifier = 36,
        @date = 40,
        @time = 41,
        @datetime2 = 42,
        @datetimeoffset = 43,
        @tinyint = 48,
        @smallint = 52,
        @int = 56,
        @smalldatetime = 58,
        @real = 59,
        @money = 60,
        @datetime = 61,
        @float = 62,
        @sql_variant = 98,
        @ntext = 99,
        @bit = 104,
        @decimal = 106,
        @numeric = 108,
        @smallmoney = 122,
        @bigint = 127,
        @hierarchyid = 128,
        @geometry = 129,
        @geography = 130,
        @varbinary = 165,
        @varchar = 167,
        @binary = 173,
        @char = 175,
        @timestamp = 189,
        @nvarchar = 231,
        @nchar = 239,
        @xml = 241,
        @sysname = 256,
    }
}
