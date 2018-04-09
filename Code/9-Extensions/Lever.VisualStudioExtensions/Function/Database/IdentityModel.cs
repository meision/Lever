using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class IdentityModel : IExpressible, IXmlSerializable
    {
        #region Static
        private const long DefaultSeed = 1;
        private const long DefaultIncrement = 1;
        private const char Separator_SeedIncrement = ',';
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

        private string _columnName;
        public string ColumnName
        {
            get
            {
                return this._columnName;
            }
            set
            {
                this._columnName = value;
            }
        }

        private long _seed;
        public long Seed
        {
            get
            {
                return this._seed;
            }
            set
            {
                this._seed = value;
            }
        }

        private long _increment;
        public long Increment
        {
            get
            {
                return this._increment;
            }
            set
            {
                this._increment = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        public IdentityModel()
        {
        }

        public IdentityModel(string columnName, long seed = DefaultSeed, long increment = DefaultIncrement)
        {
            this._columnName = columnName;
            this._seed = seed;
            this._increment = increment;
        }

        public void ParseExpression(string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            string[] segments = expression.Split(Separator_SeedIncrement);
            this._seed = Convert.ToInt64(segments[0]);
            this._increment = Convert.ToInt64(segments[1]);
        }
        #endregion Constructor

        #region Method
        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Identity";
        internal const string XmlAttribute_ColumnName = "ColumnName";
        internal const string XmlAttribute_Seed = "Seed";
        internal const string XmlAttribute_Increment = "Increment";

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
                    case XmlAttribute_ColumnName:
                        this.ColumnName = reader.Value;
                        break;
                    case XmlAttribute_Seed:
                        this.Seed = Convert.ToInt64(reader.Value);
                        break;
                    case XmlAttribute_Increment:
                        this.Increment = Convert.ToInt64(reader.Value);
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
            writer.WriteAttributeString(XmlAttribute_ColumnName, this.ColumnName);
            writer.WriteAttributeString(XmlAttribute_Seed, this.Seed.ToString());
            writer.WriteAttributeString(XmlAttribute_Increment, this.Increment.ToString());
        }
        #endregion IXmlSerializable Members

        public string ToExpression()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this._seed);
            builder.Append(Separator_SeedIncrement);
            builder.Append(this._increment);
            return builder.ToString();
        }
        #endregion Method
    }
}
