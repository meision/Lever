using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class RelationshipModelCollection : Collection<RelationshipModel>, IXmlSerializable
    {
        #region Field & Property
        private DatabaseModel _database;
        public DatabaseModel Database
        {
            get
            {
                return this._database;
            }
        }

        public RelationshipModel this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                foreach (RelationshipModel model in this)
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
        public RelationshipModelCollection(DatabaseModel database)
        {
            this._database = database;
        }
        #endregion Constructor

        #region Method
        public RelationshipModel Find(DataModel table1, DataModel table2)
        {
            if (table1 == null)
            {
                throw new ArgumentNullException("table1");
            }
            if (table2 == null)
            {
                throw new ArgumentNullException("table2");
            }

            return this.Find(table1.Name, table2.Name);
        }

        public RelationshipModel Find(string table1, string table2)
        {
            if (table1 == null)
            {
                throw new ArgumentNullException("table1");
            }
            if (table2 == null)
            {
                throw new ArgumentNullException("table2");
            }

            foreach (RelationshipModel relationship in this)
            {
                if ((relationship.PrincipalEnd.Table.Equals(table1, StringComparison.OrdinalIgnoreCase) && relationship.DependentEnd.Table.Equals(table2, StringComparison.OrdinalIgnoreCase))
                 || (relationship.PrincipalEnd.Table.Equals(table2, StringComparison.OrdinalIgnoreCase) && relationship.DependentEnd.Table.Equals(table1, StringComparison.OrdinalIgnoreCase)))
                {
                    return relationship;
                }
            }

            return null;
        }

        protected override void InsertItem(int index, RelationshipModel item)
        {
            item.Database = this._database;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Database = null;

            base.RemoveItem(index);
        }

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Relationships";

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
                        case RelationshipModel.XmlElement_Tag:
                            RelationshipModel relationship = new RelationshipModel();
                            relationship.ReadXml(reader);
                            this.Add(relationship);
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
            foreach (RelationshipModel item in this)
            {
                writer.WriteStartElement(RelationshipModel.XmlElement_Tag);
                item.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
        #endregion Method
    }
}
