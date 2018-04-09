using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.Database
{
    public abstract class DataModel
    {
        #region Static
        public const string DefaultSchemaName = "dbo";
        #endregion Static

        #region Field & Property
        private DatabaseModel _database;
        public DatabaseModel Database
        {
            get
            {
                return this._database;
            }
            internal set
            {
                this._database = value;
            }
        }

        private string _schema;
        public string Schema
        {
            get
            {
                return this._schema;
            }
            set
            {
                this._schema = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            internal set
            {
                this._name = value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return this._description;
            }
            internal set
            {
                this._description = value;
            }
        }

        private ColumnModelCollection _columns;
        public ColumnModelCollection Columns
        {
            get
            {
                if (this._columns == null)
                {
                    this._columns = new ColumnModelCollection(this);
                }

                return this._columns;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal DataModel()
        {
        }

        public DataModel(string name)
            : this(DefaultSchemaName, name)
        {
        }

        public DataModel(string schema, string name)
        {
            if (schema == null)
            {
                throw new ArgumentNullException("schema");
            }
            if (schema.Length == 0)
            {
                throw new ArgumentException(SR.DataModel_Exception_SchemaEmpty);
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length == 0)
            {
                throw new ArgumentException(SR.DataModel_Exception_NameEmpty);
            }

            this._name = name;
        }
        #endregion Constructor

        #region Method
        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.{1}", this._schema, this._name);
        }
        #endregion Method
    }
}
