﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Meision.Database
{
    public class ViewModelCollection : Collection<ViewModel>, IXmlSerializable
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

        public ViewModel this[string name]
        {
            get
            {
                return this[ViewModel.DefaultSchemaName, name];
            }
        }

        public ViewModel this[string schema, string name]
        {
            get
            {
                if (schema == null)
                {
                    throw new ArgumentNullException("schema");
                }
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                foreach (ViewModel model in this)
                {
                    if (schema.Equals(model.Schema, StringComparison.OrdinalIgnoreCase)
                     && name.Equals(model.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return model;
                    }
                }

                return null;
            }
        }
        #endregion Field & Property

        #region Constructor
        public ViewModelCollection(DatabaseModel database)
        {
            this._database = database;
        }
        #endregion Constructor

        #region Method
        protected override void InsertItem(int index, ViewModel item)
        {
            if (this[item.Schema, item.Name] != null)
            {
                throw new InvalidOperationException(SR.TableModelCollection_Exception_NameExists);
            }
            item.Database = this._database;

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this[index].Database = null;

            base.RemoveItem(index);
        }

        #region IXmlSerializable Members
        internal const string XmlElement_Tag = "Tables";

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
                // Read first element
                reader.ReadStartElement();
                while (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case ViewModel.XmlElement_Tag:
                            ViewModel view = new ViewModel();
                            view.ReadXml(reader);
                            this.Add(view);
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
            foreach (ViewModel item in this)
            {
                writer.WriteStartElement(ViewModel.XmlElement_Tag);
                (item as IXmlSerializable).WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
        #endregion Method

    }
}
