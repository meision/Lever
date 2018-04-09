using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class IndexModelCollection : Collection<IndexModel>, IXmlSerializable
    {
        #region Field & Property
        private DataModel _table;
        public DataModel Table
        {
            get
            {
                return this._table;
            }
        }
        #endregion Field & Property

        #region Constructor
        public IndexModelCollection(DataModel table)
        {
            this._table = table;
        }
        #endregion Constructor

        #region Method
        protected override void InsertItem(int index, IndexModel item)
        {
            item.Table = this._table;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Table = null;

            base.RemoveItem(index);
        }

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Indexes";

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            // Attributes
            while (reader.MoveToNextAttribute())
            {
            }

            reader.MoveToContent();

            // Elements
            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                while (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case IndexModel.XmlElement_Tag:
                            IndexModel index = new IndexModel();
                            index.ReadXml(reader);
                            this.Add(index);
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
            foreach (IndexModel item in this)
            {
                writer.WriteStartElement(IndexModel.XmlElement_Tag);
                item.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
        #endregion Method
    }
}
