using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ColumnSortModel : IXmlSerializable
    {
        #region Field & Property
        private IndexModel _index;
        public IndexModel Index
        {
            get
            {
                return this._index;
            }
            internal set
            {
                this._index = value;
            }
        }

        private string _column;
        public string Column
        {
            get
            {
                return this._column;
            }
            set
            {
                this._column = value;
            }
        }

        private SortOrder _sort;
        public SortOrder Sort
        {
            get
            {
                return this._sort;
            }
            set
            {
                this._sort = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal ColumnSortModel()
        {
        }

        public ColumnSortModel(string column, SortOrder sortOrder)
        {
            this._column = column;
            this._sort = sortOrder;
        }
        #endregion Constructor

        #region Method
        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "ColumnSort";
        internal const string XmlAttribute_Column = "Column";
        internal const string XmlAttribute_Sort = "Sort";

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
                    case XmlAttribute_Column:
                        this.Column = reader.Value;
                        break;
                    case XmlAttribute_Sort:
                        this.Sort = (SortOrder)Enum.Parse(typeof(SortOrder), reader.Value);
                        break;
                }
            }

            reader.MoveToContent();

            // Elements
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
            writer.WriteAttributeString(XmlAttribute_Column, this.Column);
            writer.WriteAttributeString(XmlAttribute_Sort, this.Sort.ToString());
        }
        #endregion IXmlSerializable Members

        public override string ToString()
        {
            string text = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0} {1}",
                this._column,
                this._sort);
            return text;
        }
        #endregion Method
    }
}
