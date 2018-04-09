using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    [XmlRoot(ElementName = UniqueConstraintModel.XmlElement_Tag)]
    public class UniqueConstraintModel : ConstraintModel
    {
        #region Field & Property
        private string[] _columns;
        public string[] Columns
        {
            get
            {
                return this._columns;
            }
            set
            {
                this._columns = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        public UniqueConstraintModel()
        {
        }

        public UniqueConstraintModel(string[] columns)
            : this(null, columns)
        {
        }

        public UniqueConstraintModel(string name, string[] columns)
            : base(name)
        {
            this._columns = columns;
        }
        #endregion Constructor

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Unique";
        internal const string XmlAttribute_Columns = "Columns";

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
                    case XmlAttribute_Columns:
                        this.Columns = reader.Value.Split(new string[] { DatabaseHelper.Separator }, StringSplitOptions.RemoveEmptyEntries);
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
            if (this.Columns != null)
            {
                writer.WriteAttributeString(XmlAttribute_Columns, string.Join(DatabaseHelper.Separator, this.Columns));
            }
        }
        #endregion IXmlSerializable Members
    }
}
