using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class RelationshipModel : IXmlSerializable
    {
        #region Static
        #endregion Static

        #region Field & Property
        private DatabaseModel _database;
        public DatabaseModel Database
        {
            get
            {
                return this._database;
            }
            internal set
            {
                this._database = value;
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

        private bool _updateCascade;
        public bool UpdateCascade
        {
            get
            {
                return this._updateCascade;
            }
            set
            {
                this._updateCascade = value;
            }
        }

        private bool _deleteCascade;
        public bool DeleteCascade
        {
            get
            {
                return this._deleteCascade;
            }
            set
            {
                this._deleteCascade = value;
            }
        }

        private EndModel _principalEnd;
        public EndModel PrincipalEnd
        {
            get
            {
                return this._principalEnd;
            }
            internal set
            {
                this._principalEnd = value;
            }
        }

        private EndModel _dependentEnd;
        public EndModel DependentEnd
        {
            get
            {
                return this._dependentEnd;
            }
            internal set
            {
                this._dependentEnd = value;
            }
        }
        #endregion Field & Property

        #region Constructor
        internal RelationshipModel()
        {
        }

        public RelationshipModel(EndModel principalEnd, EndModel dependentEnd)
            : this("FK" + DatabaseHelper.Relation + principalEnd.Table + DatabaseHelper.Relation + dependentEnd.Table, principalEnd, dependentEnd)
        {
        }

        public RelationshipModel(string name, EndModel principalEnd, EndModel dependentEnd)
        {
            this._name = name;
            this._principalEnd = principalEnd;
            this._principalEnd.Relationship = this;
            this._dependentEnd = dependentEnd;
            this._dependentEnd.Relationship = this;
            this.UpdateCascade = true;
            this.DeleteCascade = true;
        }
        #endregion Constructor

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Relationship";
        internal const string XmlAttribute_Name = "Name";
        internal const string XmlAttribute_UpdateCascade = "UpdateCascade";
        internal const string XmlAttribute_DeleteCascade = "DeleteCascade";

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
                    case XmlAttribute_UpdateCascade:
                        this.UpdateCascade = Convert.ToBoolean(reader.Value);
                        break;
                    case XmlAttribute_DeleteCascade:
                        this.DeleteCascade = Convert.ToBoolean(reader.Value);
                        break;
                }
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
                        case EndModel.XmlElement_Principal:
                            this._principalEnd = new EndModel();
                            this._principalEnd.ReadXml(reader);
                            break;
                        case EndModel.XmlElement_Dependent:
                            this._dependentEnd = new EndModel();
                            this._dependentEnd.ReadXml(reader);
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
            // Attributes
            if (this.Name != default(string))
            {
                writer.WriteAttributeString(XmlAttribute_Name, this.Name);
            }
            if (this.UpdateCascade != default(bool))
            {
                writer.WriteAttributeString(XmlAttribute_UpdateCascade, this.UpdateCascade.ToString());
            }
            if (this.DeleteCascade != default(bool))
            {
                writer.WriteAttributeString(XmlAttribute_DeleteCascade, this.DeleteCascade.ToString());
            }

            // Elements
            if (this._principalEnd != null)
            {
                writer.WriteStartElement(EndModel.XmlElement_Principal);
                this._principalEnd.WriteXml(writer);
                writer.WriteEndElement();
            }
            if (this._dependentEnd != null)
            {
                writer.WriteStartElement(EndModel.XmlElement_Dependent);
                this._dependentEnd.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members

        public bool IsUnique()
        {
            PrimaryKeyConstraintModel primaryKeyModel = this.Database.Tables[this.DependentEnd.Schema, this.DependentEnd.Table].Constraints.OfType<PrimaryKeyConstraintModel>().FirstOrDefault();
            if (primaryKeyModel == null)
            {
                return false;
            }

            return this.DependentEnd.Columns.All(p => primaryKeyModel.Columns.Contains(p));
        }
    }
}
