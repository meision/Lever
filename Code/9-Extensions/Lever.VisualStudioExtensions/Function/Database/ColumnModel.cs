using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ColumnModel : IXmlSerializable
    {
        #region Field & Property
        private DataModel _owner;
        public DataModel Owner
        {
            get
            {
                return this._owner;
            }
            internal set
            {
                this._owner = value;
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

        private string _originType;
        public string OriginType
        {
            get
            {
                return this._originType;
            }
            set
            {
                this._originType = value;
            }
        }

        private DbType _type;
        public DbType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        
        private int? _length;
        public int? Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }

        private int? _precision;
        public int? Precision
        {
            get
            {
                return this._precision;
            }
            set
            {
                this._precision = value;
            }
        }

        private int? _scale;
        public int? Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                this._scale = value;
            }
        }

        private bool _nullable;
        public bool Nullable
        {
            get
            {
                return this._nullable;
            }
            set
            {
                this._nullable = value;
            }
        }

        private string _collation;
        public string Collation
        {
            get
            {
                return this._collation;
            }
            set
            {
                this._collation = value;
            }
        }

        private string _defaultValue;
        public string DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                this._defaultValue = value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal ColumnModel()
        {
        }

        public ColumnModel(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length == 0)
            {
                throw new ArgumentException(SR.ColumnModel_Exception_NameEmpty);
            }

            this._name = name;
        }
        #endregion Constructor

        #region Method
        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Column";
        internal const string XmlAttribute_Name = "Name";
        internal const string XmlAttribute_OriginType = "OriginType";
        internal const string XmlAttribute_Type = "Type";
        internal const string XmlAttribute_Length = "Length";
        internal const string XmlAttribute_Precision = "Precision";
        internal const string XmlAttribute_Scale = "Scale";
        internal const string XmlAttribute_Nullable = "Nullable";
        internal const string XmlAttribute_Collation = "Collation";
        internal const string XmlAttribute_DefaultValue = "DefaultValue";
        internal const string XmlAttribute_Description = "Description";

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
                    case XmlAttribute_Type:
                        this.Type = (DbType)Enum.Parse(typeof(DbType), reader.Value);
                        break;
                    case XmlAttribute_OriginType:
                        this.OriginType = reader.Value;
                        break;
                    case XmlAttribute_Length:
                        if (reader.Value != "max")
                        {
                            this.Length = Convert.ToInt32(reader.Value);
                        }
                        else
                        {
                            this.Length = -1;
                        }
                        break;
                    case XmlAttribute_Precision:
                        this.Precision = Convert.ToInt32(reader.Value);
                        break;
                    case XmlAttribute_Scale:
                        this.Scale = Convert.ToInt32(reader.Value);
                        break;
                    case XmlAttribute_Nullable:
                        this.Nullable = Convert.ToBoolean(reader.Value);
                        break;
                    case XmlAttribute_Collation:
                        this.Collation = Convert.ToString(reader.Value);
                        break;
                    case XmlAttribute_DefaultValue:
                        this.DefaultValue = Convert.ToString(reader.Value);
                        break;
                    case XmlAttribute_Description:
                        this.Description = Convert.ToString(reader.Value);
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
            writer.WriteAttributeString(XmlAttribute_Name, this.Name);
            writer.WriteAttributeString(XmlAttribute_Type, this.Type.ToString());
            writer.WriteAttributeString(XmlAttribute_OriginType, this.OriginType);
            if (this.Length.HasValue)
            {
                writer.WriteAttributeString(XmlAttribute_Length, this.Length.ToString());
            }
            if (this.Precision.HasValue)
            {
                writer.WriteAttributeString(XmlAttribute_Precision, this.Precision.ToString());
            }
            if (this.Scale.HasValue)
            {
                writer.WriteAttributeString(XmlAttribute_Scale, this.Scale.ToString());
            }
            writer.WriteAttributeString(XmlAttribute_Nullable, this.Nullable.ToString());
            if (this.Collation != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Collation, this.Collation.ToString());
            }
            if (this.DefaultValue != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_DefaultValue, this.DefaultValue.ToString());
            }
            if (this.Description != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Description, this.Description.ToString());
            }
        }
        #endregion IXmlSerializable Members

        public override string ToString()
        {
            return this._name;
        }
        #endregion Method
    }
}
