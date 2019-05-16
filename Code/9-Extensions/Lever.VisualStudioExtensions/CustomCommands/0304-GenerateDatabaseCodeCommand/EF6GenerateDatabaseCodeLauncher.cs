using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;
using Meision.Database;

namespace Meision.VisualStudio.CustomCommands
{
    internal class EF6GenerateDatabaseCodeLauncher : GenerateDatabaseCodeLauncher
    {
        public EF6GenerateDatabaseCodeLauncher(ProjectItem projectItem) : base(projectItem)
        {
        }

        protected override string GenerateMainCode()
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
            builder.AppendLine($"        public {this.Config.Generation.Main.Class.Name}()");
            builder.AppendLine($"            : base(\"Default\")");
            builder.AppendLine($"        {{");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        public {this.Config.Generation.Main.Class.Name}(string nameOrConnectionString)");
            builder.AppendLine($"            : base(nameOrConnectionString)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"        }}");
            builder.AppendLine($"        ");
            foreach (DataModel dataModel in this.DataModels)
            {
                builder.AppendLine($"        public virtual IDbSet<{dataModel.Name}> {dataModel.Name} {{ get; set; }}");
            }
            builder.AppendLine($"");
            builder.AppendLine($"        protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            builder.AppendLine($"        {{");
            foreach (DataModel dataModel in this.DataModels)
            {
                builder.AppendLine($"            this.On{dataModel.Name}Creating(modelBuilder);");
            }
            builder.AppendLine($"");
            builder.AppendLine($"            base.OnModelCreating(modelBuilder);");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            foreach (DataModel dataModel in this.DataModels)
            {
                builder.AppendLine($"        private void On{dataModel.Name}Creating(DbModelBuilder modelBuilder)");
                builder.AppendLine($"        {{");
                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().ToTable(\"{dataModel.Name}\");");
                TableModel tableModel = dataModel as TableModel;
                RelationshipModel[] principalModels = null;
                PrimaryKeyConstraintModel primaryKeyModel = null;
                IdentityModel identityModel = null;
                IndexModelCollection indexesModel = null;
                if (tableModel != null)
                {
                    principalModels = this.RelationshipModels.Where(p => tableModel.Schema.Equals(p.PrincipalEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.PrincipalEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
                    primaryKeyModel = tableModel.Constraints.OfType<PrimaryKeyConstraintModel>().FirstOrDefault();
                    if (primaryKeyModel != null)
                    {
                        if (primaryKeyModel.Columns.Length != 1)
                        {
                            throw new Exception("Only support PK for single column.");
                        }
                    }
                    identityModel = tableModel.Identity;
                    indexesModel = tableModel.Indexes;
                }

                foreach (ColumnModel columnModel in dataModel.Columns)
                {
                    string columnName = columnModel.Name;
                    if ((primaryKeyModel != null) && (columnName == primaryKeyModel.Columns[0]))
                    {
                        columnName = "Id";
                        builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().HasKey(_ => _.{columnName});");
                        builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasColumnName(\"{columnModel.Name}\");");
                        if ((identityModel == null) || !columnModel.Name.Equals(identityModel.ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);");
                        }
                    }
                    if ((identityModel != null) && columnModel.Name.Equals(identityModel.ColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);");
                    }
                    if (!columnModel.Nullable)
                    {
                        builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsRequired();");
                    }
                    switch (columnModel.Type)
                    {
                        case DbType.AnsiString:
                            if (columnModel.Precision >= 0)
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasMaxLength({columnModel.Precision});");
                            }
                            else
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsMaxLength();");
                            }
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsUnicode(false);");
                            break;
                        case DbType.AnsiStringFixedLength:
                            if (columnModel.Precision >= 0)
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasMaxLength({columnModel.Precision});");
                            }
                            else
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsMaxLength();");
                            }
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsFixedLength();");
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsUnicode(false);");
                            break;
                        case DbType.Binary:
                            if (columnModel.Length >= 0)
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasMaxLength({columnModel.Length});");
                            }
                            else
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsMaxLength();");
                            }
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsFixedLength();");
                            break;
                        case DbType.Boolean:
                            break;
                        case DbType.Byte:
                            break;
                        case DbType.Currency:
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasColumnType(\"money\");");
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasPrecision({columnModel.Precision}, {columnModel.Scale});");
                            break;
                        case DbType.Date:
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasColumnType(\"date\");");
                            break;
                        case DbType.DateTime:
                            break;
                        case DbType.DateTime2:
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasColumnType(\"datetime2\");");
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasPrecision({columnModel.Scale});");
                            break;
                        case DbType.DateTimeOffset:
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasPrecision({columnModel.Scale});");
                            break;
                        case DbType.Decimal:
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasPrecision({columnModel.Precision}, {columnModel.Scale});");
                            break;
                        case DbType.Double:
                            break;
                        case DbType.Guid:
                            break;
                        case DbType.Object:
                            break;
                        case DbType.SByte:
                            break;
                        case DbType.Single:
                            break;
                        case DbType.String:
                            if (columnModel.Precision >= 0)
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasMaxLength({columnModel.Precision});");
                            }
                            else
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsMaxLength();");
                            }
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsUnicode(true);");
                            break;
                        case DbType.StringFixedLength:
                            if (columnModel.Length >= 0)
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasMaxLength({columnModel.Precision});");
                            }
                            else
                            {
                                builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsMaxLength();");
                            }
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsUnicode(true);");
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).IsFixedLength();");
                            break;
                        case DbType.Time:
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasPrecision({columnModel.Scale});");
                            break;
                        default:
                            break;
                    }
                }

                if (indexesModel != null)
                {
                    // Index
                    foreach (IndexModel indexModel in tableModel.Indexes)
                    {
                        for (int i = 0; i < indexModel.ColumnSorts.Count; i++)
                        {
                            ColumnSortModel sortModel = indexModel.ColumnSorts[i];
                            builder.AppendLine($"            modelBuilder.Entity<{tableModel.Name}>().Property(_ => _.{sortModel.Column}).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute(\"{indexModel.Name}\", {i}) {{ IsClustered = {indexModel.IsClustered.ToString().ToLowerInvariant()}, IsUnique = {indexModel.IsUnique.ToString().ToLowerInvariant()} }}));");
                        }
                    }
                }

                if (principalModels != null)
                {
                    foreach (RelationshipModel principalModel in principalModels)
                    {
                        string[] columns = principalModel.DependentEnd.Columns.Select(p => "_." + p).ToArray();
                        builder.AppendLine($"            modelBuilder.Entity<{principalModel.PrincipalEnd.Table}>().HasMany(_ => _.{principalModel.DependentEnd.Table}).WithRequired(_ => _.{principalModel.PrincipalEnd.Table}).HasForeignKey(_ => new {{ {string.Join(", ", columns)} }}).WillCascadeOnDelete({principalModel.DeleteCascade.ToString().ToLowerInvariant()});");
                    }
                }
                builder.AppendLine($"        }}");
                builder.AppendLine($"");
            }
            builder.AppendLine($"    }}");
            builder.AppendLine($"}}");
            builder.AppendLine($"");

            return builder.ToString();
        }
         
        protected override string GenerateEntityCode(DataModel dataModel)
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
                    DatabaseConfig.DefaultValueConfig config = this.Config.Generation.Entity.DefaultValues.FirstOrDefault(p => p.Type == type);
                    if (config != null)
                    {
                        builder.AppendLine($"            this.{columnModel.Name} = {config.Value};");
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
