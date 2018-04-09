using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    [XmlRoot(ElementName = CheckConstraintModel.XmlElement_Tag)]
    public class CheckConstraintModel : ConstraintModel
    {
        #region Field & Property
        private string _expression;
        public string Expression
        {
            get
            {
                return this._expression;
            }
            set
            {
                this._expression = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        public CheckConstraintModel()
        {
        }

        public CheckConstraintModel(string expression)
            : this(null, expression)
        {
        }

        public CheckConstraintModel(string name, string expression)
            : base(name)
        {
            this._expression = expression;
        }
        #endregion Constructor

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Check";
        internal const string XmlAttribute_Expression = "Expression";

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
                    case XmlAttribute_Expression:
                        this.Expression = reader.Value;
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
            if (this.Expression != null)
            {
                writer.WriteAttributeString(XmlAttribute_Expression, this._expression);
            }
        }
        #endregion IXmlSerializable Members
    }
}
