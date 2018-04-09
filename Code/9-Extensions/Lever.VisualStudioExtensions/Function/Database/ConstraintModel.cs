using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public abstract class ConstraintModel : IXmlSerializable
    {
        #region Field & Property
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

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
        #endregion Field & Property

        #region Constructor
        public ConstraintModel()
        {
        }

        public ConstraintModel(string name)
        {
            this._name = name;
        }
        #endregion Constructor

        #region Method
        #region IXmlSerializable Members
        internal const string XmlAttribute_Name = "name";

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public abstract void ReadXml(System.Xml.XmlReader reader);

        public abstract void WriteXml(System.Xml.XmlWriter writer);
        #endregion IXmlSerializable Members
        #endregion Method
    }
}
