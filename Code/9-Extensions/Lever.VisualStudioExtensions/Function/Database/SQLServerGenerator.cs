using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.Database
{
    public class SQLServerGenerator
    {
        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
        }

        private DataTable _tTables;
        private DataTable _tTableColumns;
        private DataTable _tViews;
        private DataTable _tViewColumns;
        private DataTable _tIdentities;
        private DataTable _tIndices;
        private DataTable _tForeignKeys;

        public SQLServerGenerator(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public DatabaseModel CreateDatabaseModel()
        {
            DataSet dataSet = new DataSet();
            string[] scripts = new string[]
            {
                DatabaseHelper.GenerateLoadTablesScript(),
                DatabaseHelper.GenerateLoadTableColumnsScript(),
                DatabaseHelper.GenerateLoadViewsScript(),
                DatabaseHelper.GenerateLoadViewColumnsScript(),
                DatabaseHelper.GenerateLoadIdentitiesScript(),
                DatabaseHelper.GenerateLoadIndicesScript(),
                DatabaseHelper.GenerateLoadForeignKeysScript()
            };

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(string.Join(";", scripts), connection);
                adapter.Fill(dataSet);
                this._tTables = dataSet.Tables[0];
                this._tTableColumns = dataSet.Tables[1];
                this._tViews = dataSet.Tables[2];
                this._tViewColumns = dataSet.Tables[3];
                this._tIdentities = dataSet.Tables[4];
                this._tIndices = dataSet.Tables[5];
                this._tForeignKeys = dataSet.Tables[6];

                DatabaseModel databaseModel = new DatabaseModel(connection.Database);
                // Tables
                foreach (DataRow row in this._tTables.Rows)
                {
                    TableModel tableModel = this.CreateTableModel(row);
                    databaseModel.Tables.Add(tableModel);
                }
                // Relationships
                foreach (IGrouping<string, DataRow> row in this._tForeignKeys.Select().GroupBy(p => Convert.ToString(p["ForeignKeyName"])))
                {
                    RelationshipModel relationshipModel = this.CreateRelationshipModel(row.ToArray());
                    databaseModel.Relationships.Add(relationshipModel);
                }
                // Views
                foreach (DataRow row in this._tViews.Rows)
                {
                    ViewModel viewModel = this.CreateViewModel(row);
                    databaseModel.Views.Add(viewModel);
                }

                return databaseModel;
            }
        }

        private TableModel CreateTableModel(DataRow tableRow)
        {
            TableModel tableModel = new TableModel();
            tableModel.Schema = Convert.ToString(tableRow["Schema"]);
            tableModel.Name = Convert.ToString(tableRow["Name"]);
            tableModel.Description = Convert.ToString(tableRow["Description"]);

            // Columns
            foreach (DataRow row in this._tTableColumns.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture, "[Schema] = '{0}' AND [TableName] = '{1}'", tableModel.Schema, tableModel.Name)))
            {
                ColumnModel columnModel = this.CreateColumnModel(row);
                tableModel.Columns.Add(columnModel);
            }
            // Identity
            foreach (DataRow row in this._tIdentities.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture, "[Schema] = '{0}' AND [TableName] = '{1}'", tableModel.Schema, tableModel.Name)))
            {
                IdentityModel identityModel = this.CreateIdentityModel(row);
                tableModel.Identity = identityModel;
            }
            // PrimaryKeys
            foreach (IGrouping<string, DataRow> row in this._tIndices.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture, "[Schema] = '{0}' AND [TableName] = '{1}' AND [IsPrimaryKey] = 1", tableModel.Schema, tableModel.Name)).GroupBy(p => Convert.ToString(p["IndexName"])))
            {
                PrimaryKeyConstraintModel primaryKeyConstraintModel = this.CreatePrimaryKeyConstraintModel(row.ToArray());
                tableModel.Constraints.Add(primaryKeyConstraintModel);
            }
            // Indexes
            foreach (IGrouping<string, DataRow> row in this._tIndices.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture, "[Schema] = '{0}' AND [TableName] = '{1}' AND [IsPrimaryKey] = 0", tableModel.Schema, tableModel.Name)).GroupBy(p => Convert.ToString(p["IndexName"])))
            {
                IndexModel indexModel = this.CreateIndexModel(row.ToArray());
                tableModel.Indexes.Add(indexModel);
            }

            return tableModel;
        }

        private ViewModel CreateViewModel(DataRow tableRow)
        {
            ViewModel viewModel = new ViewModel();
            viewModel.Schema = Convert.ToString(tableRow["Schema"]);
            viewModel.Name = Convert.ToString(tableRow["Name"]);
            viewModel.Description = Convert.ToString(tableRow["Description"]);
            // Columns
            foreach (DataRow row in this._tViewColumns.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture, "[Schema] = '{0}' AND [TableName] = '{1}'", viewModel.Schema, viewModel.Name)))
            {
                ColumnModel columnModel = this.CreateColumnModel(row);
                viewModel.Columns.Add(columnModel);
            }
            return viewModel;
        }

        private ColumnModel CreateColumnModel(DataRow columnRow)
        {
            ColumnModel columnModel = new ColumnModel();
            columnModel.Name = Convert.ToString(columnRow["ColumnName"]);
            columnModel.Type = DatabaseHelper.GetDbType((SqlColumnType)Enum.Parse(typeof(SqlColumnType), Convert.ToString(columnRow["Type"])));
            columnModel.Length = Convert.ToInt32(columnRow["Length"]);
            columnModel.Precision = !Convert.IsDBNull(columnRow["Precision"]) ? (int?)Convert.ToInt32(columnRow["Precision"]) : null;
            columnModel.Scale = !Convert.IsDBNull(columnRow["Scale"]) ? (int?)Convert.ToInt32(columnRow["Scale"]) : null;
            columnModel.Nullable = Convert.ToBoolean(columnRow["Nullable"]);
            columnModel.Collation = !Convert.IsDBNull(columnRow["Collation"]) ? Convert.ToString(columnRow["Collation"]) : null;
            columnModel.DefaultValue = !Convert.IsDBNull(columnRow["DefaultValue"]) ? Convert.ToString(columnRow["DefaultValue"]) : null;
            columnModel.Description = !Convert.IsDBNull(columnRow["Description"]) ? Convert.ToString(columnRow["Description"]) : null;
            return columnModel;
        }

        private IdentityModel CreateIdentityModel(DataRow identityRow)
        {
            IdentityModel identityModel = new IdentityModel();
            identityModel.ColumnName = Convert.ToString(identityRow["ColumnName"]);
            identityModel.Seed = Convert.ToInt64(identityRow["Seed"]);
            identityModel.Increment = Convert.ToInt64(identityRow["Increment"]);
            return identityModel;
        }

        private PrimaryKeyConstraintModel CreatePrimaryKeyConstraintModel(DataRow[] primaryKeyConstraintRows)
        {
            PrimaryKeyConstraintModel primaryKeyConstraintModel = new PrimaryKeyConstraintModel();
            primaryKeyConstraintModel.Name = Convert.ToString(primaryKeyConstraintRows[0]["IndexName"]);
            primaryKeyConstraintModel.Columns = primaryKeyConstraintRows.Select(p => Convert.ToString(p["ColumnName"])).ToArray();
            return primaryKeyConstraintModel;
        }

        private IndexModel CreateIndexModel(DataRow[] indexRows)
        {
            IndexModel indexModel = new IndexModel();
            indexModel.Name = Convert.ToString(indexRows[0]["IndexName"]);
            indexModel.IsPrimaryKey = Convert.ToBoolean(indexRows[0]["IsPrimaryKey"]);
            indexModel.IsClustered = Convert.ToBoolean(indexRows[0]["IsClustered"]);
            indexModel.IsUnique = Convert.ToBoolean(indexRows[0]["IsUnique"]);
            foreach (DataRow row in indexRows)
            {
                ColumnSortModel columnSortModel = new ColumnSortModel(Convert.ToString(row["ColumnName"]), Convert.ToBoolean(row["IsDescending"]) ? SortOrder.Descending : SortOrder.Ascending);
                indexModel.ColumnSorts.Add(columnSortModel);
            }
            return indexModel;
        }

        private RelationshipModel CreateRelationshipModel(DataRow[] relationshipRows)
        {
            RelationshipModel relationshipModel = new RelationshipModel();
            relationshipModel.Name = Convert.ToString(relationshipRows[0]["ForeignKeyName"]);
            relationshipModel.PrincipalEnd = new EndModel(Convert.ToString(relationshipRows[0]["PrincipalTableSchema"]), Convert.ToString(relationshipRows[0]["PrincipalTableName"]), relationshipRows.Select(p => Convert.ToString(p["PrincipalColumnName"])).ToArray());
            relationshipModel.DependentEnd = new EndModel(Convert.ToString(relationshipRows[0]["DependentTableSchema"]), Convert.ToString(relationshipRows[0]["DependentTableName"]), relationshipRows.Select(p => Convert.ToString(p["DependentColumnName"])).ToArray());
            return relationshipModel;
        }
    }
}
