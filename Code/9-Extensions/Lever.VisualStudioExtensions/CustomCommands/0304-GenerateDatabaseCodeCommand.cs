using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Meision.Database;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class GenerateDatabaseCodeCommand : CustomCommand
    {
        public GenerateDatabaseCodeCommand()
        {
            this.CommandId = 0x0304;
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
            if ((string)this.DTE.SelectedItems.Item(1).ProjectItem.Properties.Item("CustomTool").Value != "DatabaseCodeGenerator")
            {
                return;
            }

            menuItem.Visible = true;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string xmlPath = (string)projectItem.Properties.Item("FullPath").Value;

            // Delete all cs files
            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(xmlPath), "*.cs", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }

            DatabaseCodeGenerator generator = new DatabaseCodeGenerator(xmlPath);
            generator.GenerateMain();
            generator.GenerateEntites();

            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(xmlPath), "*.cs", SearchOption.TopDirectoryOnly))
            {
                projectItem.Collection.AddFromFile(file);
            }
        }
    }

    internal class DatabaseCodeGenerator
    {
        private string _dictionary;
        private DatabaseConfig _config;
        private DatabaseModel _databaseModel;
        private List<DataModel> _dataModels;
        private List<RelationshipModel> _relationshipModels;

        public DatabaseCodeGenerator(string xmlPath)
        {
            this._dictionary = Path.GetDirectoryName(xmlPath);

            this._config = new DatabaseConfig();
            XDocument document = XDocument.Parse(File.ReadAllText(xmlPath));
            this._config.Load(document);

            this.Initialize();

        }

        private void Initialize()
        {
            SQLServerGenerator generator = new SQLServerGenerator(this._config.Defination.Connection.ConnectionString);
            this._databaseModel = generator.CreateDatabaseModel();
            this._dataModels = new List<DataModel>();
            this._relationshipModels = new List<RelationshipModel>();

            Func<string, string, bool> IsSchemaMatch = (schemaName, expression) =>
            {
                if (expression == "*")
                {
                    return true;
                }
                else
                {
                    return schemaName.Equals(expression, StringComparison.OrdinalIgnoreCase);
                }
            };
            Func<string, string, bool> IsNameMatch = (dataName, expression) =>
            {
                if (expression.StartsWith("^") && expression.EndsWith("$"))
                {
                    return Regex.IsMatch(dataName, expression);
                }
                else
                {
                    return dataName.Equals(expression, StringComparison.OrdinalIgnoreCase);
                }
            };
            if ((this._databaseModel.Tables != null) && (this._config.Defination.Storages.Tables != null))
            {
                foreach (DataModel item in this._databaseModel.Tables)
                {
                    foreach (var storageConfig in this._config.Defination.Storages.Tables)
                    {
                        if (IsSchemaMatch(item.Schema, storageConfig.Schema)
                         && IsNameMatch(item.Name, storageConfig.Expression))
                        {
                            this._dataModels.Add(item);
                            break;
                        }
                    }
                }
            }
            if ((this._databaseModel.Views != null) && (this._config.Defination.Storages.Views != null))
            {
                foreach (DataModel item in this._databaseModel.Views)
                {
                    foreach (var storageConfig in this._config.Defination.Storages.Views)
                    {
                        if (IsSchemaMatch(item.Schema, storageConfig.Schema)
                         && IsNameMatch(item.Name, storageConfig.Expression))
                        {
                            this._dataModels.Add(item);
                            break;
                        }
                    }
                }
            }

            if (this._config.Defination.Models != null)
            {
                foreach (var modelConfig in this._config.Defination.Models)
                {
                    switch (modelConfig.Action)
                    {
                        case DatabaseConfig.OperationAction.Add:
                        case DatabaseConfig.OperationAction.Update:
                            {
                                List<DataModel> targets = new List<DataModel>();
                                if (modelConfig.Action == DatabaseConfig.OperationAction.Add)
                                {
                                    ViewModel target = new ViewModel();
                                    target.Schema = modelConfig.Schema;
                                    target.Name = modelConfig.Expression;
                                    target.Description = modelConfig.Description;
                                    targets.Add(target);
                                    this._dataModels.AddRange(targets);
                                }
                                else
                                {
                                    for (int i = 0; i < this._dataModels.Count; i++)
                                    {
                                        if (IsSchemaMatch(this._dataModels[i].Schema, modelConfig.Schema)
                                         && IsNameMatch(this._dataModels[i].Name, modelConfig.Expression))
                                        {
                                            if (modelConfig.Description != null) this._dataModels[i].Description = modelConfig.Description;

                                            targets.Add(this._dataModels[i]);
                                        }
                                    }
                                }

                                if (modelConfig.Columns != null)
                                {
                                    foreach (var columnConfig in modelConfig.Columns)
                                    {
                                        switch (columnConfig.Action)
                                        {
                                            case DatabaseConfig.OperationAction.Add:
                                            case DatabaseConfig.OperationAction.Update:
                                                foreach (var target in targets)
                                                {
                                                    ColumnModel column = null;
                                                    if (columnConfig.Action == DatabaseConfig.OperationAction.Add)
                                                    {
                                                        if (!target.Columns.Any(p => p.Name.Equals(columnConfig.Name, StringComparison.OrdinalIgnoreCase)))
                                                        {
                                                            column = new ColumnModel(columnConfig.Name);
                                                            target.Columns.Add(column);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        column = target.Columns.FirstOrDefault(p => p.Name.Equals(columnConfig.Name, StringComparison.OrdinalIgnoreCase));
                                                    }

                                                    if (column != null)
                                                    {
                                                        if (columnConfig.Name != null) column.Name = columnConfig.Name;
                                                        if (columnConfig.Type != null) column.Type = columnConfig.Type.Value;
                                                        if (columnConfig.Length != null) column.Length = columnConfig.Length.Value;
                                                        if (columnConfig.Precision != null) column.Precision = columnConfig.Precision.Value;
                                                        if (columnConfig.Scale != null) column.Scale = columnConfig.Scale.Value;
                                                        if (columnConfig.Nullable != null) column.Nullable = columnConfig.Nullable.Value;
                                                        if (columnConfig.Collation != null) column.Collation = columnConfig.Collation;
                                                        if (columnConfig.DefaultValue != null) column.DefaultValue = columnConfig.DefaultValue;
                                                        if (columnConfig.Description != null) column.Description = columnConfig.Description;
                                                    }
                                                }
                                                break;
                                            case DatabaseConfig.OperationAction.Remove:
                                                foreach (var target in targets)
                                                {
                                                    for (int i = target.Columns.Count - 1; i >= 0; i--)
                                                    {
                                                        if (target.Columns[i].Name.Equals(columnConfig.Name, StringComparison.OrdinalIgnoreCase))
                                                        {
                                                            target.Columns.RemoveAt(i);
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case DatabaseConfig.OperationAction.Remove:
                            for (int i = this._dataModels.Count - 1; i >= 0; i--)
                            {
                                if (IsSchemaMatch(this._dataModels[i].Schema, modelConfig.Schema)
                                 && IsNameMatch(this._dataModels[i].Name, modelConfig.Expression))
                                {
                                    this._dataModels.RemoveAt(i);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (var relationshipMode in this._databaseModel.Relationships)
            {
                if (this._dataModels.Any(p => IsSchemaMatch(p.Schema, relationshipMode.PrincipalEnd.Schema) && IsNameMatch(p.Name, relationshipMode.PrincipalEnd.Table))
                 && this._dataModels.Any(p => IsSchemaMatch(p.Schema, relationshipMode.DependentEnd.Schema) && IsNameMatch(p.Name, relationshipMode.DependentEnd.Table)))
                {
                    this._relationshipModels.Add(relationshipMode);
                }
            }
        }

        public void GenerateMain()
        {
            string code = this.GenerateMainCode();
            System.IO.File.WriteAllText(Path.Combine(this._dictionary, $"{this._config.Generation.Main.Class.Name}.cs"), code);
        }

        private string GenerateMainCode()
        {
            if (!this._config.Generation.Enable)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();
            // Imports
            if (this._config.Generation.Main.Imports != null)
            {
                foreach (string import in this._config.Generation.Main.Imports)
                {
                    builder.AppendLine($"using {import};");
                }
            }
            builder.AppendLine($"");
            builder.AppendLine($"namespace {this._config.Generation.Main.Class.Namespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    {this._config.Generation.Main.Class.AccessModifier.ToString().ToLowerInvariant()} partial class {this._config.Generation.Main.Class.Name} : {this._config.Generation.Main.Class.Base}");
            builder.AppendLine($"    {{");
            builder.AppendLine($"        public {this._config.Generation.Main.Class.Name}()");
            builder.AppendLine($"            : base(\"Default\")");
            builder.AppendLine($"        {{");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        public {this._config.Generation.Main.Class.Name}(string nameOrConnectionString)");
            builder.AppendLine($"            : base(nameOrConnectionString)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"        }}");
            builder.AppendLine($"        ");
            foreach (DataModel dataModel in this._dataModels)
            {
                builder.AppendLine($"public virtual IDbSet<{dataModel.Name}> {dataModel.Name} {{ get; set; }}");
            }
            builder.AppendLine($"");
            builder.AppendLine($"        protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            builder.AppendLine($"        {{");
            foreach (DataModel dataModel in this._dataModels)
            {
                builder.AppendLine($"            this.On{dataModel.Name}Creating(modelBuilder);");
            }
            builder.AppendLine($"");
            builder.AppendLine($"            base.OnModelCreating(modelBuilder);");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            foreach (DataModel dataModel in this._dataModels)
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
                    principalModels = this._relationshipModels.Where(p => tableModel.Schema.Equals(p.PrincipalEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.PrincipalEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
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
                            builder.AppendLine($"            modelBuilder.Entity<{dataModel.Name}>().Property(_ => _.{columnName}).HasColumnType(\"moeny\");");
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

        public void GenerateEntites()
        {
            foreach (DataModel dataModel in this._dataModels)
            {
                string code = this.GenerateEntityCode(dataModel);
                System.IO.File.WriteAllText(Path.Combine(this._dictionary, $"{dataModel.Name}.cs"), code);
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
                principalModels = this._relationshipModels.Where(p => tableModel.Schema.Equals(p.PrincipalEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.PrincipalEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
                dependentModels = this._relationshipModels.Where(p => tableModel.Schema.Equals(p.DependentEnd.Schema, StringComparison.OrdinalIgnoreCase) && tableModel.Name.Equals(p.DependentEnd.Table, StringComparison.OrdinalIgnoreCase)).ToArray();
                primaryKeyModel = tableModel.Constraints.OfType<PrimaryKeyConstraintModel>().FirstOrDefault();
            }

            // Base class
            string baseClass = null;
            if (primaryKeyModel != null)
            {
                ColumnModel[] idColumnModels = dataModel.Columns.Where(p => primaryKeyModel.Columns.Contains(p.Name)).ToArray();
                string[] idTypes = idColumnModels.Select(p => DatabaseHelper.GetCLRTypeString(p.Type, p.Nullable)).ToArray();
                baseClass = "Entity<" + string.Join(", ", idTypes) + ">";
            }
            else
            {
                baseClass = "Entity";
            }
            // Interface
            List<string> interfaceNames = new List<string>();
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

            builder.AppendLine($"/*** This file is auto generated by T4 template, please do not modify manually. ***/");
            if (this._config.Generation.Entity.Imports != null)
            {
                foreach (string import in this._config.Generation.Entity.Imports)
                {
                    builder.AppendLine($"using {import};");
                }
            }
            builder.AppendLine($"");
            builder.AppendLine($"namespace {this._config.Generation.Entity.Class.Namespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {dataModel.Description ?? string.Empty}");
            builder.AppendLine($"    /// </summary>");
            builder.AppendLine($"    {this._config.Generation.Entity.Class.AccessModifier.ToString().ToLowerInvariant()} partial class {dataModel.Name}{(!string.IsNullOrEmpty(baseClass) ? " : " + baseClass : string.Empty)}{((interfaceNames.Count > 0) ? ", " + string.Join(", ", interfaceNames.ToArray()) : string.Empty)}");
            builder.AppendLine($"    {{");
            builder.AppendLine($"        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Microsoft.Usage\", \"CA2214: DoNotCallOverridableMethodsInConstructors\")]");
            builder.AppendLine($"        public {dataModel.Name}()");
            builder.AppendLine($"        {{");
            if ((this._config.Generation.Entity.DefaultValues != null) && (this._config.Generation.Entity.DefaultValues.Count > 0))
            {
                builder.AppendLine($"            // Set default value");
                foreach (ColumnModel columnModel in dataModel.Columns)
                {
                    string type = DatabaseHelper.GetCLRTypeString(columnModel.Type, columnModel.Nullable);
                    if (this._config.Generation.Entity.DefaultValues.ContainsKey(type))
                    {
                        builder.AppendLine($"            this.{columnModel.Name} = {this._config.Generation.Entity.DefaultValues[type]};");
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
                builder.AppendLine($"        /// <summary>");
                builder.AppendLine($"        /// {columnModel.Description ?? string.Empty}");
                builder.AppendLine($"        /// </summary>");
                builder.AppendLine($"        {(((primaryKeyModel != null) && (primaryKeyModel.Columns.Contains(columnModel.Name))) ? "//" : string.Empty)}public {DatabaseHelper.GetCLRTypeString(columnModel.Type, columnModel.Nullable)} {(((primaryKeyModel != null) && (primaryKeyModel.Columns.Contains(columnModel.Name))) ? "Id" : columnModel.Name)} {{ get; set; }}");
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
                    builder.AppendLine($"        [System.Diagnostics.CodeAnalysis.SuppressMessage(\"Microsoft.Usage\", \"CA2227: CollectionPropertiesShouldBeReadOnly\")]");
                    builder.AppendLine($"        public virtual ICollection<{relationshipModel.DependentEnd.Table}> {relationshipModel.DependentEnd.Table} {{ get; set; }}");
                }
            }
            builder.AppendLine($"    }}");
            builder.AppendLine($"}}");
            builder.AppendLine($"");

            return builder.ToString();
        }
    }

    internal class DatabaseConfig
    {
        public enum OperationAction
        {
            None = 0,
            Add = 1,
            Update = 2,
            Remove = 3,
        }

        public enum AccessModifier
        {
            Public = 0,
            Internal = 1,
            Private = 2,
            Protected = 3,
        }

        public class ConnectionConfig
        {
            public string ProviderName { get; set; }
            public string ConnectionString { get; set; }
        }

        public class ClassConfig
        {
            public string Namespace { get; set; }
            public AccessModifier AccessModifier { get; set; }
            public string Name { get; set; }
            public string Base { get; set; }
        }

        public class ColumnConfig
        {
            public OperationAction Action { get; set; }
            public string Name { get; set; }
            public DbType? Type { get; set; }
            public int? Length { get; set; }
            public int? Precision { get; set; }
            public int? Scale { get; set; }
            public bool? Nullable { get; set; }
            public string Collation { get; set; }
            public string DefaultValue { get; set; }
            public string Description { get; set; }
        }

        public class StorageConfig
        {
            public string Schema { get; set; }
            public string Expression { get; set; }
        }

        public class StoragesConfig
        {
            public List<StorageConfig> Tables { get; set; }
            public List<StorageConfig> Views { get; set; }
        }

        public class ModelConfig
        {
            public OperationAction Action { get; set; }
            public string Schema { get; set; }
            public string Expression { get; set; }
            public string Description { get; set; }
            public List<ColumnConfig> Columns { get; set; }
        }

        public class DefinationConfig
        {
            public ConnectionConfig Connection { get; set; }
            public StoragesConfig Storages { get; set; }
            public List<ModelConfig> Models { get; set; }
        }

        public class GenerationConfig
        {
            public bool Enable { get; set; }
            public MainGenerationConfig Main { get; set; }
            public EntityGenerationConfig Entity { get; set; }
        }

        public class MainGenerationConfig
        {
            public List<string> Imports { get; set; }
            public ClassConfig Class { get; set; }
        }

        public class EntityGenerationConfig
        {
            public List<string> Imports { get; set; }
            public Dictionary<string, string> DefaultValues { get; set; }
            public ClassConfig Class { get; set; }
        }

        public DefinationConfig Defination { get; set; }
        public GenerationConfig Generation { get; set; }

        public void Load(XDocument document)
        {
            XElement eConfiguration = document.Element("configuration");
            if (eConfiguration == null)
            {
                throw new ArgumentException("Invalid configuration element or not found.");
            }

            #region defination
            XElement eDefination = eConfiguration.Element("defination");
            if (eDefination == null)
            {
                throw new ArgumentException("Invalid defination element or not found.");
            }
            else
            {
                this.Defination = new DefinationConfig();

                #region connection
                XElement eConnection = eDefination.Element("connection");
                if (eConnection == null)
                {
                    throw new ArgumentException("Invalid connection element or not found.");
                }
                else
                {
                    this.Defination.Connection = new ConnectionConfig();
                    //// providerName
                    XAttribute aProviderName = eConnection.Attribute("providerName");
                    if (aProviderName == null)
                    {
                        throw new ArgumentException("Invalid providerName attribute or not found.");
                    }
                    else
                    {
                        this.Defination.Connection.ProviderName = aProviderName.Value;
                        if (this.Defination.Connection.ProviderName != "System.Data.SqlClient")
                        {
                            throw new ArgumentException("Only support System.Data.SqlClient sql provider");
                        }
                    }
                    //// connectionString
                    XAttribute aConnectionString = eConnection.Attribute("connectionString");
                    if (aConnectionString == null)
                    {
                        throw new ArgumentException("Invalid connectionString attribute or not found.");
                    }
                    else
                    {
                        this.Defination.Connection.ConnectionString = aConnectionString.Value;
                    }
                }
                #endregion connection

                #region storages
                XElement eStorages = eDefination.Element("storages");
                if (eStorages == null)
                {
                    throw new ArgumentException("Invalid storages element or not found.");
                }
                else
                {
                    this.Defination.Storages = new StoragesConfig();
                    foreach (var name in new string[] { "tables", "views" })
                    {
                        foreach (XElement element in eStorages.Elements(name))
                        {
                            if (element == null)
                            {
                            }
                            else
                            {
                                List<StorageConfig> storages = new List<StorageConfig>();
                                foreach (XElement eItem in element.Elements())
                                {
                                    if (!eItem.Name.LocalName.Equals(OperationAction.Add.ToString(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        throw new ArgumentException("Invalid add element or not found.");
                                    }
                                    else
                                    {
                                        StorageConfig storage = new StorageConfig();
                                        // schema
                                        foreach (XAttribute aItem in eItem.Attributes())
                                        {
                                            switch (aItem.Name.LocalName)
                                            {
                                                case "schema":
                                                    storage.Schema = aItem.Value;
                                                    break;
                                                case "expression":
                                                    storage.Expression = aItem.Value;
                                                    break;
                                                default:
                                                    throw new ArgumentException("Invalid attribute.");
                                            }
                                        }
                                        storages.Add(storage);
                                    }
                                }

                                switch (name)
                                {
                                    case "tables":
                                        this.Defination.Storages.Tables = storages;
                                        break;
                                    case "views":
                                        this.Defination.Storages.Views = storages;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                #endregion storages

                #region models
                XElement eModels = eDefination.Element("models");
                if (eModels == null)
                {
                }
                else
                {
                    this.Defination.Models = new List<ModelConfig>();
                    foreach (XElement eModel in eModels.Elements("model"))
                    {
                        ModelConfig model = new ModelConfig();
                        foreach (XAttribute aItem in eModel.Attributes())
                        {
                            switch (aItem.Name.LocalName)
                            {
                                case "action":
                                    model.Action = (OperationAction)Enum.Parse(typeof(OperationAction), aItem.Value, true);
                                    break;
                                case "schema":
                                    model.Schema = aItem.Value;
                                    break;
                                case "expression":
                                    model.Expression = aItem.Value;
                                    break;
                                case "description":
                                    model.Description = aItem.Value;
                                    break;
                                default:
                                    throw new ArgumentException("Invalid attribute.");
                            }
                        }

                        XElement eColumns = eModel.Element("columns");
                        if (eColumns == null)
                        {
                        }
                        else
                        {
                            model.Columns = new List<ColumnConfig>();
                            foreach (XElement eColumn in eColumns.Elements("column"))
                            {
                                ColumnConfig column = new ColumnConfig();
                                column.Type = DbType.String;
                                foreach (XAttribute aItem in eColumn.Attributes())
                                {
                                    switch (aItem.Name.LocalName)
                                    {
                                        case "action":
                                            column.Action = (OperationAction)Enum.Parse(typeof(OperationAction), aItem.Value, true);
                                            break;
                                        case "name":
                                            column.Name = aItem.Value;
                                            break;
                                        case "type":
                                            column.Type = (DbType)Enum.Parse(typeof(DbType), aItem.Value, true);
                                            break;
                                        case "length":
                                            column.Length = Convert.ToInt32(aItem.Value);
                                            break;
                                        case "precision":
                                            column.Precision = Convert.ToInt32(aItem.Value);
                                            break;
                                        case "scale":
                                            column.Scale = Convert.ToInt32(aItem.Value);
                                            break;
                                        case "nullable":
                                            column.Nullable = Convert.ToBoolean(aItem.Value);
                                            break;
                                        case "collation":
                                            column.Collation = Convert.ToString(aItem.Value);
                                            break;
                                        case "defaultValue":
                                            column.DefaultValue = Convert.ToString(aItem.Value);
                                            break;
                                        case "description":
                                            column.Description = Convert.ToString(aItem.Value);
                                            break;
                                        default:
                                            throw new ArgumentException("Invalid attribute.");
                                    }
                                }
                                switch (column.Action)
                                {
                                    case OperationAction.Add:
                                    case OperationAction.Update:
                                    case OperationAction.Remove:
                                        break;
                                    default:
                                        throw new ArgumentException("Invalid action attribute.");
                                }
                                if (string.IsNullOrEmpty(column.Name))
                                {
                                    throw new ArgumentException("Invalid name attribute.");
                                }

                                model.Columns.Add(column);
                            }
                        }

                        this.Defination.Models.Add(model);
                    }
                }
                #endregion models
            }
            #endregion defination

            #region generation
            XElement eGeneration = eConfiguration.Element("generation");
            if (eGeneration == null)
            {
                throw new ArgumentException("Invalid generation element or not found.");
            }
            else
            {
                Func<XElement, List<string>> GetImports = (element) =>
                {
                    List<string> imports = new List<string>();
                    foreach (XElement eItem in element.Elements())
                    {
                        XAttribute aImport = eItem.Attribute("import");
                        if (aImport == null)
                        {
                            throw new ArgumentException("Invalid import attribute or not found.");
                        }
                        else
                        {
                            switch (eItem.Name.LocalName)
                            {
                                case "add":
                                    {
                                        imports.Add(aImport.Value);
                                    }
                                    break;
                                case "remove":
                                    {
                                        imports.Remove(aImport.Value);
                                    }
                                    break;
                            }
                        }
                    }
                    return imports;
                };
                Func<XElement, ClassConfig> GetClassConfig = (element) =>
                {
                    ClassConfig @class = new ClassConfig();
                    foreach (XAttribute aItem in element.Attributes())
                    {
                        switch (aItem.Name.LocalName)
                        {
                            case "namespace":
                                @class.Namespace = aItem.Value;
                                break;
                            case "accessModifier":
                                @class.AccessModifier = (AccessModifier)Enum.Parse(typeof(AccessModifier), aItem.Value, true);
                                break;
                            case "name":
                                @class.Name = aItem.Value;
                                break;
                            case "base":
                                @class.Base = aItem.Value;
                                break;
                        }
                    }
                    return @class;
                };


                this.Generation = new GenerationConfig();
                XAttribute aEnable = eGeneration.Attribute("enable");
                if (aEnable == null)
                {
                    throw new ArgumentException("Invalid enable attribute or not found.");
                }
                else
                {
                    this.Generation.Enable = Convert.ToBoolean(aEnable.Value);
                }

                XElement eMain = eGeneration.Element("main");
                if (eMain == null)
                {
                    throw new ArgumentException("Invalid main element or not found.");
                }
                else
                {
                    this.Generation.Main = new MainGenerationConfig();
                    // Imports
                    XElement eImports = eMain.Element("imports");
                    if (eImports == null)
                    {
                    }
                    else
                    {
                        this.Generation.Main.Imports = new List<string>();
                        this.Generation.Main.Imports.AddRange(GetImports(eImports));
                    }
                    // class
                    XElement eClass = eMain.Element("class");
                    if (eClass == null)
                    {
                        throw new ArgumentException("Invalid class element or not found.");
                    }
                    else
                    {

                        this.Generation.Main.Class = GetClassConfig(eClass);
                    }
                }

                XElement eEntity = eGeneration.Element("entity");
                if (eEntity == null)
                {
                    throw new ArgumentException("Invalid entity element or not found.");
                }
                else
                {
                    this.Generation.Entity = new EntityGenerationConfig();
                    // Imports
                    XElement eImports = eEntity.Element("imports");
                    if (eImports == null)
                    {
                    }
                    else
                    {
                        this.Generation.Entity.Imports = new List<string>();
                        this.Generation.Entity.Imports.AddRange(GetImports(eImports));
                    }
                    // class
                    XElement eClass = eEntity.Element("class");
                    if (eClass == null)
                    {
                        throw new ArgumentException("Invalid class element or not found.");
                    }
                    else
                    {
                        this.Generation.Entity.Class = GetClassConfig(eClass);
                    }
                    // DefaultValues
                    XElement eDefaultValues = eEntity.Element("defaultValues");
                    if (eDefaultValues == null)
                    {
                    }
                    else
                    {
                        this.Generation.Entity.DefaultValues = new Dictionary<string, string>();
                        foreach (XElement eItem in eDefaultValues.Elements())
                        {
                            switch (eItem.Name.LocalName)
                            {
                                case "add":
                                    {
                                        string type;
                                        XAttribute aType = eItem.Attribute("type");
                                        if (aType == null)
                                        {
                                            throw new ArgumentException("Invalid type attribute or not found.");
                                        }
                                        else
                                        {
                                            type = aType.Value;
                                        }
                                        string value;
                                        XAttribute aValue = eItem.Attribute("value");
                                        if (aValue == null)
                                        {
                                            throw new ArgumentException("Invalid value attribute or not found.");
                                        }
                                        else
                                        {
                                            value = aValue.Value;
                                        }

                                        this.Generation.Entity.DefaultValues.Add(type, value);
                                    }
                                    break;
                                case "remove":
                                    {
                                        string type;
                                        XAttribute aType = eItem.Attribute("type");
                                        if (aType == null)
                                        {
                                            throw new ArgumentException("Invalid type attribute or not found.");
                                        }
                                        else
                                        {
                                            type = aType.Value;
                                        }

                                        this.Generation.Entity.DefaultValues.Remove(type);
                                    }
                                    break;
                                default:
                                    throw new ArgumentException("Invalid element under defaultValues.");
                            }
                        }
                    }
                }
            }
            #endregion generation
        }
    }
}
