using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ColumnSortModelCollection : Collection<ColumnSortModel>, IXmlSerializable
    {
        #region Field & Property
        private IndexModel _index;
        public IndexModel Index
        {
            get
            {
                return this._index;
            }
        }
        #endregion Field & Property

        #region Constructor
        public ColumnSortModelCollection(IndexModel index)
        {
            this._index = index;
        }
        #endregion Constructor

        #region Method
        protected override void InsertItem(int index, ColumnSortModel item)
        {
            item.Index = this._index;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Index = null;

            base.RemoveItem(index);
        }

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "ColumnSorts";

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
                        case ColumnSortModel.XmlElement_Tag:
                            ColumnSortModel columnSort = new ColumnSortModel();
                            columnSort.ReadXml(reader);
                            this.Add(columnSort);
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
            foreach (ColumnSortModel item in this)
            {
                writer.WriteStartElement(ColumnSortModel.XmlElement_Tag);
                item.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
        #endregion Method
    }
}
