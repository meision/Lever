using System;
using System.IO;
using Meision.Resources;

namespace Meision.IO
{
    public class CSVWriter 
    {

        #region Field & Property
        private TextWriter _writer;

        private int _columnCount = -1;
        #endregion Field & Property

        #region Constructor
        public CSVWriter(TextWriter writer)
        {
            ThrowHelper.ArgumentNull((writer == null), nameof(writer));

            this._writer = writer;
        }
        #endregion Constructor

        #region Method
        public void WriteRecord(params object[] items)
        {
            string[] array = new string[items.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = items[i].ToString();
            }
            this.WriteRecord(array);
        }

        public void WriteRecord(params string[] items)
        {
            if (this._columnCount >= 0)
            {
                if (this._columnCount != items.Length)
                {
                    throw new InvalidOperationException(SR.CSVWriter_Exception_ColumnCountMismatch);
                }
            }
            else
            {
                this._columnCount = items.Length;
            }

            if (items.Length > 0)
            {
                // handle all except last one.
                for (int i = 0; i < items.Length; i++)
                {
                    string text = items[i];
                    if (text.Length > 0)
                    {
                        // Write data
                        int index = text.IndexOfAny(new char[] { ',', '"', '\n' });

                        if (index >= 0)
                        {
                            this._writer.Write('"');
                            foreach (char c in text)
                            {
                                switch (c)
                                {
                                    case '"':
                                        this._writer.Write('"');
                                        this._writer.Write('"');
                                        break;
                                    default:
                                        this._writer.Write(c);
                                        break;
                                }
                            }
                            this._writer.Write('"');
                        }
                        else
                        {
                            this._writer.Write(text);
                        }
                    }

                    // Write delimiter.
                    if (i < items.Length - 1)
                    {
                        this._writer.Write(',');
                    }
                }
            }

            this._writer.WriteLine();
        }

        public void Flush()
        {
            this._writer.Flush();
        }
        #endregion Method
    }
}
