//using System;
//using System.Runtime.InteropServices;
//using System.Text;
//using Meision.Algorithms;
//using Meision.Collections;
//using Meision.Resources;
//using System.Reflection;

//namespace Meision.IO
//{
//    public sealed class DataHandler : IDataHandler
//    {
//        public class DataPin : IDataPin, IDisposable
//        {
//            private DataHandler _handler;

//            private int _savedOffset;
//            public int SavedOffset
//            {
//                get
//                {
//                    return this._savedOffset;
//                }
//            }

//            internal DataPin(DataHandler handler)
//            {
//                this._handler = handler;
//                this._savedOffset = this._handler.Position;
//            }

//            ~DataPin()
//            {
//                this.Dispose(false);
//            }

//            public void Dispose()
//            {
//                this.Dispose(true);
//                GC.SuppressFinalize(this);
//            }

//            protected virtual void Dispose(bool disposing)
//            {
//                if (disposing)
//                {
//                    this._handler.Position = this._savedOffset;
//                }
//            }
//        }

//        private const int InitialSize = 0x100;

//        private Endian _endian;
//        public Endian Endian
//        {
//            get
//            {
//                return this._endian;
//            }
//            set
//            {
//                this._endian = value;
//            }
//        }

//        private byte[] _buffer;
//        public byte[] Buffer
//        {
//            get
//            {
//                if (this._expandable)
//                {
//                    throw new InvalidOperationException(SR.DataHandler_CanNotAccess);
//                }

//                return this._buffer;
//            }
//        }

//        private bool _expandable;

//        private int _start;
//        public int Start
//        {
//            get
//            {
//                return this._start;
//            }
//        }

//        private int _end;
//        public int End
//        {
//            get
//            {
//                return this._end;
//            }
//        }

//        public int Length
//        {
//            get
//            {
//                return this._end - this._start;
//            }
//        }

//        private int _offset;
//        /// <summary>
//        /// Get or set buffer index relative to start
//        /// </summary>
//        public int Position
//        {
//            get
//            {
//                return this._offset;
//            }
//            set
//            {
//                ThrowHelper.ArgumentOutOfRange(((value < 0) || (value > this.Length)), nameof(value));

//                this._offset = value;
//            }
//        }
//        /// <summary>
//        /// Get buffer index relative to 0
//        /// </summary>
//        public int Index
//        {
//            get
//            {
//                return this._start + this._offset;
//            }
//        }

//        public int Remaining
//        {
//            get
//            {
//                return this.Length - this.Position;
//            }
//        }

//        public byte Current
//        {
//            get
//            {
//                return this._buffer[this._offset];
//            }
//            set
//            {
//                this._buffer[this._offset] = value;
//            }
//        }

//        public DataHandler(int capacity = 0)
//        {
//            ThrowHelper.ArgumentMustNotNegative((capacity < 0), nameof(capacity));

//            this._buffer = new byte[capacity];
//            this._start = 0;
//            this._end = 0;
//            this._expandable = true;
//            this._endian = Endian.Little;
//        }

//        public DataHandler(byte[] buffer)
//            : this(buffer, Endian.Little)
//        {
//        }

//        public DataHandler(byte[] buffer, Endian endian)
//            : this(buffer, 0, buffer.Length, endian)
//        {
//        }

//        public DataHandler(byte[] buffer, int index, int length, Endian endian)
//        {
//            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
//            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
//            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
//            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + length) > buffer.Length));

//            this._buffer = buffer;
//            this._start = index;
//            this._end = this._start + length;
//            this._expandable = false;
//            this._endian = endian;
//        }

//        public int PositionToIndex(int position)
//        {
//            ThrowHelper.ArgumentOutOfRange(((position < 0) || (position > this.Length)), nameof(position));

//            int index = this._start + position;
//            return index;
//        }

//        public int IndexToPosition(int index)
//        {
//            int position = index - this._start;
//            ThrowHelper.ArgumentOutOfRange(((position < 0) || (position > this.Length)), nameof(index));
//            return position;
//        }

//        private void EnsureAvailableForRead(int value)
//        {
//            ThrowHelper.ArgumentOutOfRange((this.Length - this._offset < value), nameof(value), SR.DataHandler_OutOfBytes);
//        }

//        private void EnsureAvailableForWrite(int value)
//        {
//            if ((this.Length - this._offset) >= value)
//            {
//                return;
//            }
//            if (!this._expandable)
//            {
//                throw new InvalidOperationException(SR.DataHandler_OutOfBytes);
//            }
//            if ((this._buffer.Length - this._offset) >= value)
//            {
//                return;
//            }

//            int capacity = Number.GetMax(this.Index + value, DataHandler.InitialSize, this._buffer.Length * 2);
//            this.Expand(capacity);
//        }

//        private void Expand(int capacity)
//        {
//            byte[] array = new byte[capacity];
//            Bytes.Copy(this._buffer, 0, array, 0, this._buffer.Length);
//            this._buffer = array;
//        }

//        /// <summary>
//        /// Create a pin to stored current offset, and restore when dispose
//        /// </summary>
//        public IDataPin CreateDataPin()
//        {
//            DataPin pin = new DataPin(this);
//            return pin;
//        }

//        /// <summary>
//        /// Create a pin to stored current offset and set newOffset, and restore when dispose
//        /// </summary>
//        /// <param name="newOffset">new offset set to handler</param>
//        public IDataPin CreateDataPin(int newOffset)
//        {
//            DataPin pin = new DataPin(this);
//            this.Position = newOffset;
//            return pin;
//        }

//        public byte[] ReadBytes(int size)
//        {
//            ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

//            this.EnsureAvailableForRead(size);

//            byte[] value = new byte[size];
//            this.ReadBytes(value);
//            return value;
//        }

//        public void ReadBytes(byte[] buffer)
//        {
//            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));

//            this.ReadBytes(buffer, 0, buffer.Length);
//        }

//        public void ReadBytes(byte[] buffer, int index, int count)
//        {
//            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
//            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
//            ThrowHelper.ArgumentLengthOutOfRange((count < 0), nameof(count));
//            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + count) > buffer.Length));

//            this.EnsureAvailableForRead(count);

//            Bytes.Copy(this._buffer, this.Index, buffer, index, count);
//            this._offset += count;
//        }

//        public T ReadObject<T>() where T : struct
//        {
//            return (T)this.ReadObject(typeof(T));
//        }

//        public object ReadObject(Type type)
//        {
//            ThrowHelper.ArgumentNull((type == null), nameof(type));

//            //if (!type.IsEnum)
//            //{
//            //    StructLayoutAttribute attribute = (StructLayoutAttribute)Attribute.GetCustomAttribute(type, typeof(StructLayoutAttribute));
//            //    if ((attribute == null)
//            //     || (attribute.Value != LayoutKind.Sequential)
//            //     || (attribute.Pack != 1))
//            //    {
//            //        throw new NotSupportedException(SR.DataHandler_Exception_ObjectNotSupport);
//            //    }
//            //}

//            int size = (!type.IsEnum) ? Marshal.SizeOf(type) : Marshal.SizeOf(Enum.GetUnderlyingType(type));
//            this.EnsureAvailableForRead(size);

//            if (this._endian == Endian.Little)
//            {
//                object value = ObjectConvert.FromBytes(type, size, this._buffer, this.Index);
//                this._offset += size;
//                return value;
//            }
//            else
//            {
//                Type targetType = (!type.IsEnum) ? type : Enum.GetUnderlyingType(type);
//                if (type == typeof(Byte))
//                {
//                    return this.ReadByte();
//                }
//                else if (type == typeof(SByte))
//                {
//                    return this.ReadSByte();
//                }
//                else if (type == typeof(Boolean))
//                {
//                    return this.ReadBoolean();
//                }
//                else if (type == typeof(Int16))
//                {
//                    return this.ReadInt16();
//                }
//                else if (type == typeof(UInt16))
//                {
//                    return this.ReadUInt16();
//                }
//                else if (type == typeof(Int32))
//                {
//                    return this.ReadInt32();
//                }
//                else if (type == typeof(UInt32))
//                {
//                    return this.ReadUInt32();
//                }
//                else if (type == typeof(Int64))
//                {
//                    return this.ReadInt64();
//                }
//                else if (type == typeof(UInt64))
//                {
//                    return this.ReadUInt64();
//                }
//                else if (type == typeof(Single))
//                {
//                    return this.ReadSingle();
//                }
//                else if (type == typeof(Double))
//                {
//                    return this.ReadDouble();
//                }
//                else if (!type.IsClass)
//                {
//                    object value = Activator.CreateInstance(type);
//                    foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
//                    {
//                        field.SetValue(value, this.ReadObject(field.FieldType));
//                    }
//                    return value;
//                }
//                else
//                {
//                    throw new NotSupportedException(SR.DataHandler_Exception_ObjectNotSupport);
//                }
//            }
//        }

//        public Byte ReadByte()
//        {
//            this.EnsureAvailableForRead(sizeof(Byte));

//            Byte value = this._endian.GetByte(this._buffer, this.Index);
//            this._offset += sizeof(Byte);
//            return value;
//        }

//        public SByte ReadSByte()
//        {
//            this.EnsureAvailableForRead(sizeof(SByte));

//            SByte value = this._endian.GetSByte(this._buffer, this.Index);
//            this._offset += sizeof(SByte);
//            return value;
//        }

//        public Boolean ReadBoolean()
//        {
//            this.EnsureAvailableForRead(sizeof(Boolean));

//            Boolean value = this._endian.GetBoolean(this._buffer, this.Index);
//            this._offset += sizeof(Boolean);
//            return value;
//        }

//        public Char ReadChar()
//        {
//            this.EnsureAvailableForRead(sizeof(Char));

//            Char value = this._endian.GetChar(this._buffer, this.Index);
//            this._offset += sizeof(Char);
//            return value;
//        }

//        public Int16 ReadInt16()
//        {
//            this.EnsureAvailableForRead(sizeof(Int16));

//            Int16 value = this._endian.GetInt16(this._buffer, this.Index);
//            this._offset += sizeof(Int16);
//            return value;
//        }

//        public UInt16 ReadUInt16()
//        {
//            this.EnsureAvailableForRead(sizeof(UInt16));

//            UInt16 value = this._endian.GetUInt16(this._buffer, this.Index);
//            this._offset += sizeof(UInt16);
//            return value;
//        }

//        public Int32 ReadInt32()
//        {
//            this.EnsureAvailableForRead(sizeof(Int32));

//            Int32 value = this._endian.GetInt32(this._buffer, this.Index);
//            this._offset += sizeof(Int32);
//            return value;
//        }

//        public UInt32 ReadUInt32()
//        {
//            this.EnsureAvailableForRead(sizeof(UInt32));

//            UInt32 value = this._endian.GetUInt32(this._buffer, this.Index);
//            this._offset += sizeof(UInt32);
//            return value;
//        }

//        public Int64 ReadInt64()
//        {
//            this.EnsureAvailableForRead(sizeof(Int64));

//            Int64 value = this._endian.GetInt64(this._buffer, this.Index);
//            this._offset += sizeof(Int64);
//            return value;
//        }

//        public UInt64 ReadUInt64()
//        {
//            this.EnsureAvailableForRead(sizeof(UInt64));

//            UInt64 value = this._endian.GetUInt64(this._buffer, this.Index);
//            this._offset += sizeof(UInt64);
//            return value;
//        }

//        public Single ReadSingle()
//        {
//            this.EnsureAvailableForRead(sizeof(Single));

//            Single value = this._endian.GetSingle(this._buffer, this.Index);
//            this._offset += sizeof(Single);
//            return value;
//        }

//        public Double ReadDouble()
//        {
//            this.EnsureAvailableForRead(sizeof(Double));

//            Double value = this._endian.GetDouble(this._buffer, this.Index);
//            this._offset += sizeof(Double);
//            return value;
//        }

//        public Variable8 GetVariable8()
//        {
//            return new Variable8(this.ReadSByte());
//        }

//        public Variable16 GetVariable16()
//        {
//            return new Variable16(this.ReadInt16());
//        }

//        public Variable32 GetVariable32()
//        {
//            return new Variable32(this.ReadInt32());
//        }

//        public Variable64 GetVariable64()
//        {
//            return new Variable64(this.ReadInt64());
//        }

//        /// <summary>
//        /// WriteString with terminator
//        /// </summary>
//        /// <param name="value"></param>
//        public String ReadStringWithTerminator()
//        {
//            return this.ReadStringWithTerminator(Encoding.UTF8);
//        }

//        /// <summary>
//        /// WriteString with terminator
//        /// </summary>
//        /// <param name="value"></param>
//        public String ReadStringWithTerminator(Encoding encoding)
//        {
//            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

//            switch (encoding.CodePage)
//            {
//                case 1200: // Unicode
//                case 1201: // BigEndianUnicode
//                    for (int i = this.Index; i < this._end; i += sizeof(char))
//                    {
//                        if (i + sizeof(char) > this._end)
//                        {
//                            break;
//                        }

//                        if ((this._buffer[i] == 0) && (this._buffer[i + 1] == 0))
//                        {
//                            string value = encoding.GetString(this._buffer, this.Index, i - this.Index);
//                            this._offset = i + sizeof(char);
//                            return value;
//                        }
//                    }
//                    throw new InvalidOperationException(SR.DataHandler_OutOfBytes);
//                default:
//                    for (int i = this.Index; i < this._end; i += sizeof(byte))
//                    {
//                        if (this._buffer[i] == 0)
//                        {
//                            string value = encoding.GetString(this._buffer, this.Index, i - this.Index);
//                            this._offset = i + sizeof(byte);
//                            return value;
//                        }
//                    }
//                    throw new InvalidOperationException(SR.DataHandler_OutOfBytes);
//            }
//        }

//        public String ReadString(int length)
//        {
//            return this.ReadString(length, Encoding.UTF8);
//        }

//        public String ReadString(int length, Encoding encoding)
//        {
//            ThrowHelper.ArgumentMustNotNegative((length < 0), nameof(length));
//            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

//            this.EnsureAvailableForRead(length);
//            string value = encoding.GetString(this._buffer, this.Index, length);
//            this._offset += length;
//            return value;
//        }

//        public void WriteBytes(byte[] values)
//        {
//            ThrowHelper.ArgumentNull((values == null), nameof(values));

//            this.WriteBytes(values, 0, values.Length);
//        }

//        public void WriteBytes(byte[] values, int index, int count)
//        {
//            ThrowHelper.ArgumentNull((values == null), nameof(values));
//            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
//            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + count) > values.Length));

//            this.EnsureAvailableForWrite(count);

//            Bytes.Copy(values, index, this._buffer, this.Index, count);
//            this._offset += count;
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteObject<T>(T value)
//        {
//            this.WriteObject((object)value);
//        }

//        public void WriteObject(object value)
//        {
//            ThrowHelper.ArgumentNull((value == null), nameof(value));

//            int size = (!value.GetType().IsEnum) ? Marshal.SizeOf(value) : Marshal.SizeOf(Enum.GetUnderlyingType(value.GetType()));
//            this.EnsureAvailableForWrite(size);

//            ObjectConvert.ToBytes(value, size, this._buffer, this.Index);
//            this._offset += size;
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteByte(Byte value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Byte));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Byte);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteSByte(SByte value)
//        {
//            this.EnsureAvailableForWrite(sizeof(SByte));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(SByte);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteBoolean(Boolean value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Boolean));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Boolean);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteChar(Char value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Char));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Char);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteInt16(Int16 value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Int16));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Int16);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteUInt16(UInt16 value)
//        {
//            this.EnsureAvailableForWrite(sizeof(UInt16));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(UInt16);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteInt32(Int32 value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Int32));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Int32);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteUInt32(UInt32 value)
//        {
//            this.EnsureAvailableForWrite(sizeof(UInt32));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(UInt32);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteInt64(Int64 value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Int64));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Int64);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteUInt64(UInt64 value)
//        {
//            this.EnsureAvailableForWrite(sizeof(UInt64));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(UInt64);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteSingle(Single value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Single));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Single);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteDouble(Double value)
//        {
//            this.EnsureAvailableForWrite(sizeof(Double));

//            this._endian.ToBytes(value, this._buffer, this.Index);
//            this._offset += sizeof(Double);
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void PutVariable8(Variable8 value)
//        {
//            this.WriteSByte(value.SByte);
//        }

//        public void PutVariable16(Variable16 value)
//        {
//            this.WriteInt16(value.Int16);
//        }

//        public void PutVariable32(Variable32 value)
//        {
//            this.WriteInt32(value.Int32);
//        }

//        public void PutVariable64(Variable64 value)
//        {
//            this.WriteInt64(value.Int64);
//        }

//        /// <summary>
//        /// WriteString with terminator
//        /// </summary>
//        /// <param name="value"></param>
//        public void WriteStringWithTerminator(String value)
//        {
//            this.WriteStringWithTerminator(value, Encoding.UTF8);
//        }

//        /// <summary>
//        /// WriteString with terminator
//        /// </summary>
//        /// <param name="value"></param>
//        public void WriteStringWithTerminator(String value, Encoding encoding)
//        {
//            ThrowHelper.ArgumentNull((value == null), nameof(value));
//            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

//            int size;
//            switch (encoding.CodePage)
//            {
//                case 1200: // Unicode
//                case 1201: // BigEndianUnicode
//                    size = sizeof(char);
//                    break;
//                default:
//                    size = sizeof(byte);
//                    break;
//            }
//            size += encoding.GetByteCount(value);
//            this.EnsureAvailableForWrite(size);

//            encoding.GetBytes(value, 0, value.Length, this._buffer, this.Index);
//            this._offset += size;
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public void WriteString(String value)
//        {
//            this.WriteString(value, Encoding.UTF8);
//        }

//        public void WriteString(String value, Encoding encoding)
//        {
//            ThrowHelper.ArgumentNull((value == null), nameof(value));
//            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

//            int length = encoding.GetByteCount(value);
//            this.EnsureAvailableForWrite(length);

//            encoding.GetBytes(value, 0, value.Length, this._buffer, this.Index);
//            this._offset += length;
//            if (this.Index > this._end)
//            {
//                this._end = this.Index;
//            }
//        }

//        public byte[] ExportAllBytes()
//        {
//            byte[] array = new byte[this._end - this._start];
//            Bytes.Copy(this._buffer, this._start, array, 0, this._end - this._start);
//            return array;
//        }

//        public byte[] ExportUsedBytes()
//        {
//            byte[] array = new byte[this.Index - this._start];
//            Bytes.Copy(this._buffer, this._start, array, 0, this.Index - this._start);
//            return array;
//        }
//    }
//}
