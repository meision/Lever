using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Meision.Database;

namespace Meision.VisualStudio.CustomCommands
{
    internal abstract class DatabaseCodeGenerator
    {
        public static DatabaseCodeGenerator Create(string xmlPath)
        {
            DatabaseConfig config = new DatabaseConfig();
            XDocument document = XDocument.Parse(File.ReadAllText(xmlPath));
            config.Load(document);

            DatabaseCodeGenerator generator = null;
            switch (config.Generation.Mode)
            {
                case DatabaseConfig.GenerateMode.EF6:
                    generator = new EF6CodeGenerator();
                    break;
                case DatabaseConfig.GenerateMode.EFCore:
                    generator = new EFCoreCodeGenerator();
                    break;
                default:
                    return null;
            }

            generator.Config = config;
            generator.WorkingDictionary = Path.GetDirectoryName(xmlPath);
            generator.Initialize();
            return generator;
        }

        protected string WorkingDictionary { get; set; }
        protected DatabaseConfig Config { get; set; }
        protected DatabaseModel DatabaseModel { get; set; }
        protected List<DataModel> DataModels { get; set; }
        protected List<RelationshipModel> RelationshipModels { get; set; }

        private void Initialize()
        {
            SQLServerGenerator generator = new SQLServerGenerator(this.Config.Defination.Connection.ConnectionString);
            this.DatabaseModel = generator.CreateDatabaseModel();
            this.DataModels = new List<DataModel>();
            this.RelationshipModels = new List<RelationshipModel>();

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
            if ((this.DatabaseModel.Tables != null) && (this.Config.Defination.Storages.Tables != null))
            {
                foreach (DataModel item in this.DatabaseModel.Tables)
                {
                    foreach (var storageConfig in this.Config.Defination.Storages.Tables)
                    {
                        if (IsSchemaMatch(item.Schema, storageConfig.Schema)
                         && IsNameMatch(item.Name, storageConfig.Expression))
                        {
                            this.DataModels.Add(item);
                            break;
                        }
                    }
                }
            }
            if ((this.DatabaseModel.Views != null) && (this.Config.Defination.Storages.Views != null))
            {
                foreach (DataModel item in this.DatabaseModel.Views)
                {
                    foreach (var storageConfig in this.Config.Defination.Storages.Views)
                    {
                        if (IsSchemaMatch(item.Schema, storageConfig.Schema)
                         && IsNameMatch(item.Name, storageConfig.Expression))
                        {
                            this.DataModels.Add(item);
                            break;
                        }
                    }
                }
            }

            if (this.Config.Defination.Models != null)
            {
                foreach (var modelConfig in this.Config.Defination.Models)
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
                                    this.DataModels.AddRange(targets);
                                }
                                else
                                {
                                    for (int i = 0; i < this.DataModels.Count; i++)
                                    {
                                        if (IsSchemaMatch(this.DataModels[i].Schema, modelConfig.Schema)
                                         && IsNameMatch(this.DataModels[i].Name, modelConfig.Expression))
                                        {
                                            if (modelConfig.Description != null) this.DataModels[i].Description = modelConfig.Description;

                                            targets.Add(this.DataModels[i]);
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
                            for (int i = this.DataModels.Count - 1; i >= 0; i--)
                            {
                                if (IsSchemaMatch(this.DataModels[i].Schema, modelConfig.Schema)
                                 && IsNameMatch(this.DataModels[i].Name, modelConfig.Expression))
                                {
                                    this.DataModels.RemoveAt(i);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (var relationshipMode in this.DatabaseModel.Relationships)
            {
                if (this.DataModels.Any(p => IsSchemaMatch(p.Schema, relationshipMode.PrincipalEnd.Schema) && IsNameMatch(p.Name, relationshipMode.PrincipalEnd.Table))
                 && this.DataModels.Any(p => IsSchemaMatch(p.Schema, relationshipMode.DependentEnd.Schema) && IsNameMatch(p.Name, relationshipMode.DependentEnd.Table)))
                {
                    this.RelationshipModels.Add(relationshipMode);
                }
            }
        }

        public abstract void GenerateMain();
        public abstract void GenerateEntites();
    }
}
