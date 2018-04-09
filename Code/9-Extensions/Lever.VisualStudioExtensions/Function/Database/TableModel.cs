using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class TableModel : DataModel, IXmlSerializable
    {
        #region Field & Property
        private IdentityModel _identity;
        public IdentityModel Identity
        {
            get
            {
                return this._identity;
            }
            set
            {
                this._identity = value;
            }
        }

        private ConstraintModelCollection _constraints;
        public ConstraintModelCollection Constraints
        {
            get
            {
                if (this._constraints == null)
                {
                    this._constraints = new ConstraintModelCollection(this);
                }

                return this._constraints;
            }
        }

        private IndexModelCollection _indexes;
        public IndexModelCollection Indexes
        {
            get
            {
                if (this._indexes == null)
                {
                    this._indexes = new IndexModelCollection(this);
                }

                return this._indexes;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal TableModel()
        {
        }

        public TableModel(string name)
            : this(DefaultSchemaName, name)
        {
        }

        public TableModel(string schema, string name)
            : base(schema, name)
        {
        }
        #endregion Constructor

        #region Method
        public string[] GetPrimaryKeys()
        {
            foreach (ConstraintModel constraint in this.Constraints)
            {
                PrimaryKeyConstraintModel primaryKeyConstraint = constraint as PrimaryKeyConstraintModel;
                if (primaryKeyConstraint != null)
                {
                    return primaryKeyConstraint.Columns;
                }
            }

            return null;
        }

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Table";
        internal const string XmlAttribute_Schema = "Schema";
        internal const string XmlAttribute_Name = "Name";

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
                    case XmlAttribute_Name:
                        this.Name = reader.Value;
                        break;
                }
            }

            reader.MoveToContent();

            // Element
            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                while (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case ColumnModelCollection.XmlElement_Tag:
                            this.Columns.ReadXml(reader);
                            break;
                        case ConstraintModelCollection.XmlElement_Tag:
                            this.Constraints.ReadXml(reader);
                            break;
                        case IndexModelCollection.XmlElement_Tag:
                            this.Indexes.ReadXml(reader);
                            break;
                        default:
                            throw new InvalidOperationException(reader.Name);
                    }
                }
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
            if (this.Name != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Name, this.Name);
            }

            // Columns
            if (this.Columns != null)
            {
                writer.WriteStartElement(ColumnModelCollection.XmlElement_Tag);
                this.Columns.WriteXml(writer);
                writer.WriteEndElement();
            }
            // Identity
            if (this.Identity != null)
            {
                writer.WriteStartElement(IdentityModel.XmlElement_Tag);
                this.Identity.WriteXml(writer);
                writer.WriteEndElement();
            }
            // Constraints
            if (this.Constraints != null)
            {
                writer.WriteStartElement(ConstraintModelCollection.XmlElement_Tag);
                this.Constraints.WriteXml(writer);
                writer.WriteEndElement();
            }
            // Indexes
            if (this.Indexes != null)
            {
                writer.WriteStartElement(IndexModelCollection.XmlElement_Tag);
                this.Indexes.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.{1}", this.Schema, this.Name);
        }
        #endregion Method
    }
}
