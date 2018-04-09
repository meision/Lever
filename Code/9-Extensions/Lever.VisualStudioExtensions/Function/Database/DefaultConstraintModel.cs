using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    [XmlRoot(ElementName = DefaultConstraintModel.XmlElement_Tag)]
    public class DefaultConstraintModel : ConstraintModel
    {
        #region Field & Property
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

        private string _value;
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        public DefaultConstraintModel()
        {
        }

        public DefaultConstraintModel(string column, string value)
            : this(null, column, value)
        {
        }

        public DefaultConstraintModel(string name, string column, string value)
            : base(name)
        {
            this._column = column;
            this._value = value;
        }
        #endregion Constructor

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Default";
        internal const string XmlAttribute_Column = "Column";
        internal const string XmlAttribute_Value = "Value";

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            // Attributes
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case XmlAttribute_Name:
                        this.Name = reader.Value;
                        break;
                    case XmlAttribute_Column:
                        this.Column = reader.Value;
                        break;
                    case XmlAttribute_Value:
                        this.Value = reader.Value;
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

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (this.Name != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Name, this.Name);
            }
            if (this.Column != null)
            {
                writer.WriteAttributeString(XmlAttribute_Column, this.Column);
            }
            if (this.Value != null)
            {
                writer.WriteAttributeString(XmlAttribute_Value, this.Value);
            }
        }
        #endregion IXmlSerializable Members
    }
}
