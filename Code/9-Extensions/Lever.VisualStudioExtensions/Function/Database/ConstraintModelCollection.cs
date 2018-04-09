using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ConstraintModelCollection : Collection<ConstraintModel>, IXmlSerializable
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
        public ConstraintModelCollection(DataModel table)
        {
            this._table = table;
        }
        #endregion Constructor

        #region Method
        protected override void InsertItem(int index, ConstraintModel item)
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
        internal const string XmlElement_Tag = "Constraints";

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
                        case UniqueConstraintModel.XmlElement_Tag:
                            {
                                ConstraintModel constraint = new UniqueConstraintModel();
                                constraint.ReadXml(reader);
                                this.Add(constraint);
                            }
                            break;
                        case PrimaryKeyConstraintModel.XmlElement_Tag:
                            {
                                ConstraintModel constraint = new PrimaryKeyConstraintModel();
                                constraint.ReadXml(reader);
                                this.Add(constraint);
                            }
                            break;
                        case CheckConstraintModel.XmlElement_Tag:
                            {
                                ConstraintModel constraint = new CheckConstraintModel();
                                constraint.ReadXml(reader);
                                this.Add(constraint);
                            }
                            break;
                        case DefaultConstraintModel.XmlElement_Tag:
                            {
                                ConstraintModel constraint = new DefaultConstraintModel();
                                constraint.ReadXml(reader);
                                this.Add(constraint);
                            }
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
            foreach (ConstraintModel item in this)
            {
                XmlRootAttribute attribute = Attribute.GetCustomAttribute(item.GetType(), typeof(XmlRootAttribute)) as XmlRootAttribute;
                writer.WriteStartElement(attribute.ElementName);
                item.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
        #endregion Method
    }
}
