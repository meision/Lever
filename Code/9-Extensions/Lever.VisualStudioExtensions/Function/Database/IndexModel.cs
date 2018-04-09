using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class IndexModel : IXmlSerializable
    {
        #region Static
        #endregion Static

        #region Field & Property
        private DataModel _table;
        public DataModel Table
        {
            get
            {
                return this._table;
            }
            internal set
            {
                this._table = value;
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

        private bool _isPrimaryKey;
        public bool IsPrimaryKey
        {
            get
            {
                return this._isPrimaryKey;
            }
            set
            {
                this._isPrimaryKey = value;
            }
        }

        private bool _isClustered;
        public bool IsClustered
        {
            get
            {
                return this._isClustered;
            }
            set
            {
                this._isClustered = value;
            }
        }

        private bool _isUnique;
        public bool IsUnique
        {
            get
            {
                return this._isUnique;
            }
            set
            {
                this._isUnique = value;
            }
        }

        private ColumnSortModelCollection _columnSorts;
        public ColumnSortModelCollection ColumnSorts
        {
            get
            {
                if (this._columnSorts == null)
                {
                    this._columnSorts = new ColumnSortModelCollection(this);
                }

                return this._columnSorts;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal IndexModel()
        {
        }

        public IndexModel(string name, ColumnSortModel[] columnSorts)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length == 0)
            {
                throw new ArgumentException(SR.IndexModel_Exception_NameEmpty);
            }

            this._name = name;
            foreach (ColumnSortModel columnSort in columnSorts)
            {
                this.ColumnSorts.Add(columnSort);
            }
        }
        #endregion Constructor

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Index";
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
                        case ColumnSortModelCollection.XmlElement_Tag:
                            this.ColumnSorts.ReadXml(reader);
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
            writer.WriteAttributeString(XmlAttribute_Name, this.Name);

            // Elements
            if (this._columnSorts != null)
            {
                writer.WriteStartElement(ColumnSortModelCollection.XmlElement_Tag);
                this._columnSorts.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members

    }
}
