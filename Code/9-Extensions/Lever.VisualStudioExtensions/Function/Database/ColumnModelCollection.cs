using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ColumnModelCollection : Collection<ColumnModel>, IXmlSerializable
    {
        #region Static
        protected const char ColumnSeparator = ',';
        #endregion Static

        #region Field & Property
        private DataModel _owner;
        public DataModel Owner
        {
            get
            {
                return this._owner;
            }
        }

        public ColumnModel this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                foreach (ColumnModel model in this)
                {
                    if (name.Equals(model.Name, StringComparison.Ordinal))
                    {
                        return model;
                    }
                }

                return null;
            }
        }
        #endregion Field & Property

        #region Constructor
        public ColumnModelCollection(DataModel owner)
        {
            this._owner = owner;
        }
        #endregion Constructor

        #region Method
        public void Add(string name, DbType type)
        {
            ColumnModel column = new ColumnModel(name);
            column.Type = type;
            this.Add(column);
        }

        public void Add(string name, DbType type, bool nullable)
        {
            ColumnModel column = new ColumnModel(name);
            column.Type = type;
            column.Nullable = nullable;
            this.Add(column);
        }

        public void Add(string name, DbType type, int length, bool nullable)
        {
            ColumnModel column = new ColumnModel(name);
            column.Type = type;
            column.Length = length;
            column.Nullable = nullable;
            this.Add(column);
        }

        public void Add(string name, DbType type, int precision, int scale, bool nullable)
        {
            ColumnModel column = new ColumnModel(name);
            column.Type = type;
            column.Precision = precision;
            column.Scale = scale;
            column.Nullable = nullable;
            this.Add(column);
        }

        protected override void InsertItem(int index, ColumnModel item)
        {
            if (this[item.Name] != null)
            {
                throw new InvalidOperationException(SR.ColumnModelCollection_Exception_NameExists);
            }
            item.Owner = this._owner;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Owner = null;

            base.RemoveItem(index);
        }

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Columns";

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

            // Element
            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                while (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case ColumnModel.XmlElement_Tag:
                            ColumnModel column = new ColumnModel();
                            column.ReadXml(reader);
                            this.Add(column);
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
            foreach (ColumnModel item in this)
            {
                writer.WriteStartElement(ColumnModel.XmlElement_Tag);
                item.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members

        public override string ToString()
        {
            if (this.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                if (i != 0)
                {
                    builder.Append(ColumnSeparator);
                }

                builder.Append(this[i].Name);
            }
            return builder.ToString();
        }
        #endregion Method

    }
}
