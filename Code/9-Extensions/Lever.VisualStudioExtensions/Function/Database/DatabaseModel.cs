using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Meision.Database
{
    [XmlRoot(ElementName = DatabaseModel.XmlElement_Tag)]
    public class DatabaseModel : IXmlSerializable
    {
        #region Nested Type
        private class DatabaseModelXmlTextWriter : XmlTextWriter
        {
            public DatabaseModelXmlTextWriter(Stream stream)
                : base(stream, new UTF8Encoding(false, true))
            {
            }

            public override void WriteStartDocument()
            {
                base.WriteStartDocument();
                //this.WriteDocType("CCT", null, null, "<!ELEMENT Database (Table|View)*>");
            }
        }
        #endregion Nested Type

        #region Static
        public static DatabaseModel Create(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            MemoryStream stream = new MemoryStream(data);
            return Create(stream);
        }

        public static DatabaseModel Create(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            XmlTextReader reader = new XmlTextReader(stream);
            reader.WhitespaceHandling = WhitespaceHandling.Significant;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DatabaseModel));
                DatabaseModel model = serializer.Deserialize(reader) as DatabaseModel;
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion Static

        #region Field & Property
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private TableModelCollection _tables;
        public TableModelCollection Tables
        {
            get
            {
                if (this._tables == null)
                {
                    this._tables = new TableModelCollection(this);
                }

                return this._tables;
            }
        }

        private RelationshipModelCollection _relationships;
        public RelationshipModelCollection Relationships
        {
            get
            {
                if (this._relationships == null)
                {
                    this._relationships = new RelationshipModelCollection(this);
                }

                return this._relationships;
            }
        }

        private ViewModelCollection _views;
        public ViewModelCollection Views
        {
            get
            {
                if (this._views == null)
                {
                    this._views = new ViewModelCollection(this);
                }

                return this._views;
            }
        }
        #endregion Field & Property

        #region Constructor
        public DatabaseModel()
        {
        }

        public DatabaseModel(string name)
        {
            this._name = name;
        }
        #endregion Constructor

        #region Method
        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Database";
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
                        case TableModelCollection.XmlElement_Tag:
                            this.Tables.ReadXml(reader);
                            break;
                        case RelationshipModelCollection.XmlElement_Tag:
                            this.Relationships.ReadXml(reader);
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
            if (this.Name != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Name, this._name);
            }
            // Tables
            writer.WriteStartElement(TableModelCollection.XmlElement_Tag);
            this.Tables.WriteXml(writer);
            writer.WriteEndElement();
            // Relationship
            writer.WriteStartElement(RelationshipModelCollection.XmlElement_Tag);
            this.Relationships.WriteXml(writer);
            writer.WriteEndElement();
        }
        #endregion IXmlSerializable Members

        public byte[] ToBytes()
        {
            MemoryStream stream = new MemoryStream();
            DatabaseModelXmlTextWriter writer = new DatabaseModelXmlTextWriter(stream);
            (new XmlSerializer(typeof(DatabaseModel))).Serialize(
                writer,
                this);
            return stream.ToArray();
        }

        public override string ToString()
        {
            return this._name;
        }
        #endregion Method
    }
}
