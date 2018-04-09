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
            DatabaseConfig config = new DatabaseConfig();
            XDocument document = XDocument.Parse(File.ReadAllText(xmlPath));
            config.Load(document);

            SQLServerGenerator generator = new SQLServerGenerator(config.Defination.Connection.ConnectionString);
            DatabaseModel databaseModel = generator.CreateDatabaseModel();
            List<DataModel> dataModels = new List<DataModel>();
            List<RelationshipModel> relationshipModels = new List<RelationshipModel>();
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
            if ((databaseModel.Tables != null) && (config.Defination.Storages.Tables != null))
            {
                foreach (DataModel item in databaseModel.Tables)
                {
                    foreach (var storageConfig in config.Defination.Storages.Tables)
                    {
                        if (IsSchemaMatch(item.Schema, storageConfig.Schema)
                         && IsNameMatch(item.Name, storageConfig.Expression))
                        {
                            dataModels.Add(item);
                            break;
                        }
                    }
                }
            }
            if ((databaseModel.Views != null) && (config.Defination.Storages.Views != null))
            {
                foreach (DataModel item in databaseModel.Views)
                {
                    foreach (var storageConfig in config.Defination.Storages.Views)
                    {
                        if (IsSchemaMatch(item.Schema, storageConfig.Schema)
                         && IsNameMatch(item.Name, storageConfig.Expression))
                        {
                            dataModels.Add(item);
                            break;
                        }
                    }
                }
            }

            if (config.Defination.Models != null)
            {
                foreach (var modelConfig in config.Defination.Models)
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
                                    dataModels.AddRange(targets);
                                }
                                else
                                {
                                    for (int i = 0; i < dataModels.Count; i++)
                                    {
                                        if (IsSchemaMatch(dataModels[i].Schema, modelConfig.Schema)
                                         && IsNameMatch(dataModels[i].Name, modelConfig.Expression))
                                        {
                                            if (modelConfig.Description != null) dataModels[i].Description = modelConfig.Description;

                                            targets.Add(dataModels[i]);
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
                            for (int i = dataModels.Count - 1; i >= 0; i--)
                            {
                                if (IsSchemaMatch(dataModels[i].Schema, modelConfig.Schema)
                                 && IsNameMatch(dataModels[i].Name, modelConfig.Expression))
                                {
                                    dataModels.RemoveAt(i);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (var relationshipMode in databaseModel.Relationships)
            {
                if (dataModels.Any(p => IsSchemaMatch(p.Schema, relationshipMode.PrincipalEnd.Schema) && IsNameMatch(p.Name, relationshipMode.PrincipalEnd.Table))
                 && dataModels.Any(p => IsSchemaMatch(p.Schema, relationshipMode.DependentEnd.Schema) && IsNameMatch(p.Name, relationshipMode.DependentEnd.Table)))
                {
                    relationshipModels.Add(relationshipMode);
                }
            }

            if (!config.Generation.Enable)
            {
                return;
            }




        }

        private string GenerateDbContext(DatabaseConfig config)
        {
            if (!config.Generation.Enable)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();
            // Imports
            if (config.Generation.Main.Imports != null)
            {
                foreach (string import in config.Generation.Main.Imports)
                {
                    builder.AppendLine($"using {import};");
                }
            }
            builder.AppendLine($"");
            builder.AppendLine($"namespace {config.Generation.Main.Class.Namespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    {config.Generation.Main.Class.AccessModifier.ToString().ToLowerInvariant()} partial class {config.Generation.Main.Class.Name} : {config.Generation.Main.Class.Base}");
            builder.AppendLine($"    {{");
            return null;
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
