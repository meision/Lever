using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.Database.SQL
{
    public sealed class SqlColumnInfo
    {
        public string Name { get; set; }
        public SqlColumnType Type { get; set; }
        public int? Length { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool Nullable { get; set; }
        public string Collation { get; set; }
    }
}
