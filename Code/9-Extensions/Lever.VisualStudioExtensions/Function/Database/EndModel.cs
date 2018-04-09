using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class EndModel : IXmlSerializable
    {
        #region Field & Property
        private RelationshipModel _relationship;
        public RelationshipModel Relationship
        {
            get
            {
                return this._relationship;
            }
            internal set
            {
                this._relationship = value;
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

        private string _table;
        public string Table
        {
            get
            {
                return this._table;
            }
            set
            {
                this._table = value;
            }
        }

        private string[] _columns;
        public string[] Columns
        {
            get
            {
                return this._columns;
            }
            set
            {
                this._columns = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal EndModel()
        {
        }

        public EndModel(string table, string[] columns)
            : this(DataModel.DefaultSchemaName, table, columns)
        {
        }

        public EndModel(string schema, string table, string[] columns)
        {
            if (schema == null)
            {
                throw new ArgumentNullException("schema");
            }
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            this._schema = schema;
            this._table = table;
            this._columns = columns;
        }
        #endregion Constructor

        #region IXmlSerializable Members
        internal const string XmlElement_Principal = "PrincipalEnd";
        internal const string XmlElement_Dependent = "DependentEnd";
        internal const string XmlAttribute_Schema = "Schema";
        internal const string XmlAttribute_Table = "Table";
        internal const string XmlAttribute_Columns = "Columns";

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            // Attributes
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case XmlAttribute_Schema:
                        this.Schema = reader.Value;
                        break;
                    case XmlAttribute_Table:
                        this.Table = reader.Value;
                        break;
                    case XmlAttribute_Columns:
                        this.Columns = reader.Value.Split(new string[] { DatabaseHelper.Separator }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                }
            }

            reader.MoveToContent();

            // Element
            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            // Attributes
            if (this.Schema != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Schema, this.Schema);
            }
            if (this.Table != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Table, this.Table);
            }
            if (this.Columns != null)
            {
                writer.WriteAttributeString(XmlAttribute_Columns, string.Join(DatabaseHelper.Separator, this.Columns));
            }
        }
        #endregion IXmlSerializable Members
    }
}
