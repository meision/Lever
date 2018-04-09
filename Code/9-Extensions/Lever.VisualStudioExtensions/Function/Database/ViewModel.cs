using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ViewModel : DataModel, IXmlSerializable
    {
        #region Field & Property
        #endregion Field & Property

        #region Constructor
        internal ViewModel()
        {
        }

        public ViewModel(string name)
            : this(DefaultSchemaName, name)
        {
        }

        public ViewModel(string schema, string name)
            : base(schema, name)
        {
        }
        #endregion Constructor

        #region Method
        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "View";
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
        }
        #endregion IXmlSerializable Members

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.{1}", this.Schema, this.Name);
        }
        #endregion Method
    }
}
