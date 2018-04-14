using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Meision.Resources;

namespace Meision.IO
{
    public class CSVReader
    {
        #region Static
        #endregion Static

        #region Field & Property
        private TextReader _reader;

        private CSVRecordType _recordType;
        public CSVRecordType CurrentRecordType
        {
            get
            {
                return this._recordType;
            }
        }

        private List<string> _items;


        public string this[int index]
        {
            get
            {
                if ((this._recordType != CSVRecordType.Header)
                 && (this._recordType != CSVRecordType.Data))
                {
                    throw new InvalidOperationException(SR.CSVReader_Exception_InvalidRecord);
                }

                return this._items[index];
            }
        }

        public int ColumnCount
        {
            get
            {
                if (this._items == null)
                {
                    throw new InvalidOperationException(SR.CSVReader_Exception_NotRead); 
                }

                return this._items.Count;
            }
        }

        private int _readLineCount;
        public int ReadLineCount
        {
            get
            {
                return this._readLineCount;
            }
        }
        #endregion Field & Property

        #region Constructor
        public CSVReader(TextReader reader)
        {
            ThrowHelper.ArgumentNull((reader == null), nameof(reader));

            this._reader = reader;
        }
        #endregion Constructor

        #region Method
        public bool Read()
        {
            switch (this._recordType)
            {
                case CSVRecordType.None:
                    {
                        List<string> items = this.ReadItems();
                        if (items == null)
                        {
                            this._items = null;
                            this._recordType = CSVRecordType.None;
                            return false;
                        }

                        this._items = items;
                        this._readLineCount++;
                        this._recordType = CSVRecordType.Header;
                    }
                    return true;
                case CSVRecordType.Header:
                case CSVRecordType.Data:
                    {
                        List<string> items = this.ReadItems();
                        if (items == null)
                        {
                            this._items = null;
                            this._recordType = CSVRecordType.None;
                            return false;
                        }
                        if (items.Count != this._items.Count)
                        {
                            throw new ArgumentException(SR.CSVReader_Exception_DataCountMismatch);
                        }

                        this._items = items;
                        this._readLineCount++;
                        this._recordType = CSVRecordType.Data;
                    }
                    return true;
                case CSVRecordType.End:
                    return false;
                default:
                    return false;
            }
        }


        private List<string> ReadItems()
        {
            List<string> items = new List<string>();
            bool quoted = false;
            StringBuilder builder = new StringBuilder();

            while (true)
            {
                int currentValue = this._reader.Read();
                if (currentValue < 0)
                {
                    if ((items.Count == 0)
                     && (builder.Length == 0))
                    {
                        return null;
                    }

                    throw new InvalidOperationException(SR.CSVReader_Exception_InvalidCSVFormat);
                }

                char currentChar = (char)currentValue;
                switch (currentChar)
                {
                    case ',':// Delimiter
                        if (quoted)
                        {
                            builder.Append(currentChar);
                        }
                        else
                        {
                            quoted = false;
                            items.Add(builder.ToString());
                            builder = new StringBuilder();
                        }
                        break;
                    case '"':// Quote
                        if (quoted)
                        {
                            int nextValue = this._reader.Read();
                            if (nextValue < 0)
                            {
                                throw new InvalidOperationException(SR.CSVReader_Exception_InvalidCSVFormat);
                            }

                            char nextChar = (char)nextValue;
                            switch (nextChar)
                            {
                                case '"':// Quote
                                    builder.Append(nextChar);
                                    break;
                                case ',':// Delimiter
                                    quoted = false;
                                    items.Add(builder.ToString());
                                    builder = new StringBuilder();
                                    break;
                                case '\r': // CR
                                    if (this._reader.Read() != '\n')
                                    {
                                        throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.InvariantCulture, SR.CSVReader_Exception_ExceptCharacter, '\n', items.Count + 1));
                                    }
                                    goto case '\n';
                                case '\n': // LF
                                    items.Add(builder.ToString());
                                    return items;
                                default:
                                    throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.InvariantCulture, SR.CSVReader_Exception_InvalidCharacter, nextChar, items.Count + 1));
                            }
                        }
                        else
                        {
                            if (builder.Length == 0)
                            {
                                quoted = true;
                            }
                            else
                            {
                                throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.InvariantCulture, SR.CSVReader_Exception_InvalidCharacter, currentChar, items.Count + 1));
                            }
                        }
                        break;
                    case '\r':
                        if (quoted)
                        {
                            builder.Append(currentChar);
                            break;
                        }
                        else
                        {
                            if (this._reader.Read() != '\n')
                            {
                                throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.InvariantCulture, SR.CSVReader_Exception_ExceptCharacter, '\n', items.Count + 1));
                            }
                            goto case '\n';
                        }
                    case '\n':
                        if (quoted)
                        {
                            builder.Append(currentChar);
                            break;
                        }
                        else
                        {
                            items.Add(builder.ToString());
                            return items;
                        }
                    default:
                        builder.Append(currentChar);
                        break;
                }
            }
        }

        public string[] GetCurrentItems()
        {
            if (this._items == null)
            {
                throw new InvalidOperationException(SR.CSVReader_Exception_NotRead);
            }

            return this._items.ToArray();
        }
        #endregion Method
    }
}
