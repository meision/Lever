using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Meision.Database;

namespace Meision.VisualStudio.CustomCommands
{
    internal class EFCoreCodeGenerator : DatabaseCodeGenerator
    {
        public EFCoreCodeGenerator()
        {
        }

        public override void GenerateMain()
        {
            string code = this.GenerateMainCode();
            System.IO.File.WriteAllText(Path.Combine(this.WorkingDictionary, $"{this.Config.Generation.Main.Class.Name}.cs"), code);
        }

        private string GenerateMainCode()
        {
            if (!this.Config.Generation.Enable)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, Parameters.DO_NOT_MODIFY, this.GetType().Name));
            // Imports
            if (this.Config.Generation.Main.Imports != null)
            {
                foreach (string import in this.Config.Generation.Main.Imports)
                {
                    builder.AppendLine($"using {import};");
                }
            }
            builder.AppendLine($"");
            builder.AppendLine($"namespace {this.Config.Generation.Main.Class.Namespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    {this.Config.Generation.Main.Class.AccessModifier.ToString().ToLowerInvariant()} partial class {this.Config.Generation.Main.Class.Name} : {this.Config.Generation.Main.Class.Base}");
            builder.AppendLine($"    {{");
            foreach (DataModel dataModel in this.DataModels)
            {
                builder.AppendLine($"        public virtual DbSet<{dataModel.Name}> {dataModel.Name} {{ get; set; }}");
            }
            builder.AppendLine($"");
            builder.AppendLine($"        public {this.Config.Generation.Main.Class.Name}()");
            builder.AppendLine($"        {{");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        public {this.Config.Generation.Main.Class.Name}(DbContextOptions options) : base(options)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        protected override void OnModelCreating(ModelBuilder modelBuilder)");
            builder.AppendLine($"        {{");
            foreach (DataModel dataModel in this.DataModels)
            {
                TableModel tableModel = dataModel as TableModel;
                RelationshipModel[] dependentModels = null;
                PrimaryKeyConstraintModel primaryKeyModel = null;
                IdentityModel identityModel = null;
                IndexModelCollection indexesModel = null;
                if (tableModel != null)
                {
                    dependentModels = this.RelationshipModels.Where(p => tableModel.Schema.Equals(p.DependentEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.DependentEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
                    primaryKeyModel = tableModel.Constraints.OfType<PrimaryKeyConstraintModel>().FirstOrDefault();
                    identityModel = tableModel.Identity;
                    indexesModel = tableModel.Indexes;
                }

                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>(entity =>");
                builder.AppendLine($"            {{");
                // ToTable
                builder.AppendLine($"                // Table");
                builder.AppendLine($"                entity.ToTable(\"{dataModel.Name}\");");
                // Key
                if (primaryKeyModel != null)
                {
                    builder.AppendLine($"                entity.HasKey(_ => new {{ { string.Join(", ", primaryKeyModel.Columns.Select(p => "_." + p))} }});");
                }
                // Columns
                builder.AppendLine($"                // Columns");
                foreach (ColumnModel columnModel in dataModel.Columns)
                {         
                    string columnName = columnModel.Name;
                    builder.Append($"                entity.Property(_ => _.{columnName})");
                    if ((identityModel != null) && columnModel.Name.Equals(identityModel.ColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        builder.Append($".ValueGeneratedOnAdd()");
                    }
                    if (!columnModel.Nullable)
                    {
                        builder.Append($".IsRequired()");
                    }
                    if (columnModel.DefaultValue != null)
                    {
                        builder.Append($".HasDefaultValue({columnModel.DefaultValue})");
                    }
                    switch (columnModel.Type)
                    {
                        case DbType.AnsiString:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({((columnModel.Precision >= 0) ? columnModel.Precision.ToString() : "max")})\")");
                            break;
                        case DbType.AnsiStringFixedLength:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({((columnModel.Precision >= 0) ? columnModel.Precision.ToString() : "max")})\")");
                            break;
                        case DbType.Binary:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({((columnModel.Precision >= 0) ? columnModel.Precision.ToString() : "max")})\")");
                            break;
                        case DbType.Boolean:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Byte:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Currency:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({columnModel.Precision}, {columnModel.Scale})\")");
                            break;
                        case DbType.Date:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.DateTime:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.DateTime2:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({columnModel.Scale})\")");
                            break;
                        case DbType.DateTimeOffset:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({columnModel.Scale})\")");
                            break;
                        case DbType.Decimal:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({columnModel.Precision}, {columnModel.Scale})\")");
                            break;
                        case DbType.Double:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Guid:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Int16:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Int32:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Int64:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Object:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.SByte:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.Single:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.String:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({((columnModel.Precision >= 0) ? columnModel.Precision.ToString() : "max")})\")");
                            break;
                        case DbType.StringFixedLength:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({((columnModel.Precision >= 0) ? columnModel.Precision.ToString() : "max")})\")");
                            break;
                        case DbType.Time:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({columnModel.Scale})\")");
                            break;
                        case DbType.UInt16:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.UInt32:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.UInt64:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        case DbType.VarNumeric:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}({columnModel.Precision}, {columnModel.Scale})\")");
                            break;
                        case DbType.Xml:
                            builder.Append($".HasColumnType(\"{columnModel.OriginType}\")");
                            break;
                        default:
                            break;
                    }
                    builder.AppendLine(";");
                }
                // Index
                if ((indexesModel != null) && (indexesModel.Count > 0))
                {
                    builder.AppendLine($"                // Indices");
                    foreach (IndexModel indexModel in tableModel.Indexes)
                    {
                        builder.Append($"                ");
                        builder.Append($"entity.HasIndex(_ => new {{ { string.Join(", ", indexModel.ColumnSorts.Select(p => "_." + p.Column))} }})");
                        if (!string.IsNullOrEmpty(indexModel.Name))
                        {
                            builder.Append($".HasName(\"{indexModel.Name}\")");
                        }
                        if (indexModel.IsUnique)
                        {
                            builder.Append($".IsUnique()");
                        }
                        builder.AppendLine(";");
                    }
                }
                // ForeignKey
                if ((dependentModels != null) && (dependentModels.Length > 0))
                {
                    builder.AppendLine($"                // ForeignKeys");
                    foreach (RelationshipModel dependentModel in dependentModels)
                    {
                        builder.Append($"                ");
                        builder.Append($"entity.HasOne(_ => _.{dependentModel.PrincipalEnd.Table})");
                        bool isUnique = dependentModel.DependentEnd.Columns.All(p => primaryKeyModel.Columns.Contains(p));
                        builder.Append($".{(isUnique ? "WithOne" : "WithMany")}(_ => _.{dependentModel.DependentEnd.Table})");
                        builder.Append($".HasForeignKey(_ => new {{ { string.Join(", ", dependentModel.DependentEnd.Columns.Select(p => "_." + p))} }})");
                        if (dependentModel.DeleteCascade)
                        {
                            builder.Append($".OnDelete(DeleteBehavior.Cascade)");
                        }
                        if (!string.IsNullOrEmpty(dependentModel.Name))
                        {
                            builder.Append($".HasConstraintName(\"{dependentModel.Name}\")");
                        }
                        builder.AppendLine(";");
                    }
                }

                builder.AppendLine($"            }});");
                builder.AppendLine($"");
            }
            builder.AppendLine($"            base.OnModelCreating(modelBuilder);");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"    }}");
            builder.AppendLine($"}}");
            builder.AppendLine($"");

            return builder.ToString();
        }

        public override void GenerateEntites()
        {
            foreach (DataModel dataModel in this.DataModels)
            {
                string code = this.GenerateEntityCode(dataModel);
                System.IO.File.WriteAllText(Path.Combine(this.WorkingDictionary, $"{dataModel.Name}.cs"), code);
            }
        }
        public string GenerateEntityCode(DataModel dataModel)
        {
            StringBuilder builder = new StringBuilder();

            TableModel tableModel = dataModel as TableModel;
            RelationshipModel[] principalModels = null;
            RelationshipModel[] dependentModels = null;
            PrimaryKeyConstraintModel primaryKeyModel = null;
            if (tableModel != null)
            {
                principalModels = this.RelationshipModels.Where(p => tableModel.Schema.Equals(p.PrincipalEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.PrincipalEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
                dependentModels = this.RelationshipModels.Where(p => tableModel.Schema.Equals(p.DependentEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.DependentEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
                primaryKeyModel = tableModel.Constraints.OfType<PrimaryKeyConstraintModel>().FirstOrDefault();
            }

            // Base class
            string baseClass = null;
            if (!string.IsNullOrEmpty(this.Config.Generation.Entity.Class.Base))
            {
                if (this.Config.Generation.Entity.Class.Base.EndsWith("<>"))
                {

                    // generic type
                    if (primaryKeyModel != null)
                    {
                        ColumnModel[] idColumnModels = dataModel.Columns.Where(p => primaryKeyModel.Columns.Contains(p.Name)).ToArray();
                        string[] idTypes = idColumnModels.Select(p => DatabaseHelper.GetCLRTypeString(p.Type, p.Nullable)).ToArray();
                        baseClass = $"{this.Config.Generation.Entity.Class.Base.Substring(0, this.Config.Generation.Entity.Class.Base.Length - "<>".Length)}<{string.Join(", ", idTypes)}>";
                    }
                    else
                    {
                        baseClass = this.Config.Generation.Entity.Class.Base.Substring(0, this.Config.Generation.Entity.Class.Base.Length - "<>".Length);
                    }
                }
                else
                {
                    baseClass = this.Config.Generation.Entity.Class.Base;
                }
            }
            // Interface
            List<string> interfaceNames = new List<string>();
            if (this.Config.Generation.Entity.Class.UseConventionalInterfaces)
            {
                if (dataModel.Columns.Any(p => (p.Name == "TenantId") && (p.Type == DbType.Int32) && !p.Nullable))
                {
                    string interfaceName = "IMustHaveTenant";
                    interfaceNames.Add(interfaceName);
                }
                if (dataModel.Columns.Any(p => (p.Name == "TenantId") && (p.Type == DbType.Int32) && p.Nullable))
                {
                    string interfaceName = "IMayHaveTenant";
                    interfaceNames.Add(interfaceName);
                }
                if (dataModel.Columns.Any(p => (p.Name == "CreationTime") && (p.Type == DbType.DateTime) && !p.Nullable))
                {
                    string interfaceName = "IHasCreationTime";
                    if (dataModel.Columns.Any(p => (p.Name == "CreatorUserId") && (p.Type == DbType.Int64) && p.Nullable))
                    {
                        interfaceName = "ICreationAudited";
                    }
                    interfaceNames.Add(interfaceName);
                }
                if (dataModel.Columns.Any(p => (p.Name == "LastModificationTime") && (p.Type == DbType.DateTime) && p.Nullable))
                {
                    string interfaceName = "IHasModificationTime";
                    if (dataModel.Columns.Any(p => (p.Name == "LastModifierUserId") && (p.Type == DbType.Int64) && p.Nullable))
                    {
                        interfaceName = "IModificationAudited";
                    }
                    interfaceNames.Add(interfaceName);
                }
                if (dataModel.Columns.Any(p => (p.Name == "IsDeleted") && (p.Type == DbType.Boolean) && !p.Nullable))
                {
                    string interfaceName = "ISoftDelete";
                    if (dataModel.Columns.Any(p => (p.Name == "DeletionTime") && (p.Type == DbType.DateTime) && p.Nullable))
                    {
                        interfaceName = "IHasDeletionTime";
                        if (dataModel.Columns.Any(p => (p.Name == "DeleterUserId") && (p.Type == DbType.Int64) && p.Nullable))
                        {
                            interfaceName = "IDeletionAudited";
                        }
                    }
                    interfaceNames.Add(interfaceName);
                }
                if (dataModel.Columns.Any(p => (p.Name == "IsActive") && (p.Type == DbType.Boolean) && !p.Nullable))
                {
                    string interfaceName = "IPassivable";
                    interfaceNames.Add(interfaceName);
                }
            }

            builder.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, Parameters.DO_NOT_MODIFY, this.GetType().Name));
            if (this.Config.Generation.Entity.Imports != null)
            {
                foreach (string import in this.Config.Generation.Entity.Imports)
                {
                    builder.AppendLine($"using {import};");
                }
            }
            builder.AppendLine($"");
            builder.AppendLine($"namespace {this.Config.Generation.Entity.Class.Namespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {dataModel.Description ?? string.Empty}");
            builder.AppendLine($"    /// </summary>");
            builder.AppendLine($"    {this.Config.Generation.Entity.Class.AccessModifier.ToString().ToLowerInvariant()} partial class {dataModel.Name}{((!string.IsNullOrEmpty(baseClass) || (interfaceNames.Count > 0)) ? " : " : string.Empty)}{(!string.IsNullOrEmpty(baseClass) ? baseClass : string.Empty)}{((interfaceNames.Count > 0) ? ", " + string.Join(", ", interfaceNames.ToArray()) : string.Empty)}");
            builder.AppendLine($"    {{");
            builder.AppendLine($"        public {dataModel.Name}()");
            builder.AppendLine($"        {{");
            if ((this.Config.Generation.Entity.DefaultValues != null) && (this.Config.Generation.Entity.DefaultValues.Count > 0))
            {
                builder.AppendLine($"            // Set default value");
                foreach (ColumnModel columnModel in dataModel.Columns)
                {
                    string type = DatabaseHelper.GetCLRTypeString(columnModel.Type, columnModel.Nullable);
                    if (this.Config.Generation.Entity.DefaultValues.ContainsKey(type))
                    {
                        builder.AppendLine($"            this.{columnModel.Name} = {this.Config.Generation.Entity.DefaultValues[type]};");
                    }
                }
            }
            builder.AppendLine($"");
            if ((principalModels != null) && (principalModels.Length > 0))
            {
                foreach (RelationshipModel relationshipModel in principalModels)
                {
                    builder.AppendLine($"            this.{relationshipModel.DependentEnd.Table} = new HashSet<{relationshipModel.DependentEnd.Table}>();");
                }
            }
            builder.AppendLine($"        }}");
            builder.AppendLine($"");

            foreach (ColumnModel columnModel in dataModel.Columns)
            {
                builder.AppendLine($"        /// <summary>{columnModel.Description ?? string.Empty}</summary>");
                builder.AppendLine($"        {((!string.IsNullOrEmpty(baseClass) && (primaryKeyModel != null) && (primaryKeyModel.Columns.Contains(columnModel.Name))) ? "//" : string.Empty)}public {DatabaseHelper.GetCLRTypeString(columnModel.Type, columnModel.Nullable)} {(((primaryKeyModel != null) && (primaryKeyModel.Columns.Contains(columnModel.Name))) ? "Id" : columnModel.Name)} {{ get; set; }}");
            }
            builder.AppendLine($"");
            if (dependentModels != null)
            {
                foreach (RelationshipModel relationshipModel in dependentModels)
                {
                    builder.AppendLine($"        public virtual {relationshipModel.PrincipalEnd.Table} {relationshipModel.PrincipalEnd.Table} {{ get; set; }}");
                }
            }
            if (principalModels != null)
            {
                foreach (RelationshipModel relationshipModel in principalModels)
                {
                    builder.AppendLine($"        public virtual ICollection<{relationshipModel.DependentEnd.Table}> {relationshipModel.DependentEnd.Table} {{ get; set; }}");
                }
            }
            builder.AppendLine($"    }}");
            builder.AppendLine($"}}");
            builder.AppendLine($"");

            return builder.ToString();
        }
    }
}
