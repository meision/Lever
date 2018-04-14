using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Meision.Resources;

namespace Meision.IO
{
    public class BufferStream : System.IO.Stream, IDataHandler
    {
        private class DataPin : IDataPin, IDisposable
        {
            private BufferStream _stream;

            private int _savedPosition;
            public int SavedPosition
            {
                get
                {
                    return this._savedPosition;
                }
            }

            internal DataPin(BufferStream stream)
            {
                this._stream = stream;
                this._savedPosition = (int)this._stream.Position;
            }

            ~DataPin()
            {
                this.Dispose(false);
            }
            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (this._stream != null)
                {
                    this._stream.Position = this._savedPosition;
                    this._stream = null;
                }
            }
        }

        private const int INITIAL_LENGTH = 256;
        private const int MAX_LENGTH = int.MaxValue;

        private byte[] _buffer;    // Either allocated internally or externally.
        /// <summary>
        /// Bytes array
        /// </summary>
        public byte[] Buffer
        {
            get
            {
                return this._buffer;
            }
        }
        private int _start;
        /// <summary>
        /// Start location in buffer.
        /// </summary>
        public int Start
        {
            get
            {
                return this._start;
            }
        }
        private int _end;
        /// <summary>
        /// End location in buffer.
        /// </summary>
        public int End
        {
            get
            {
                return this._end;
            }
        }
        public override long Length
        {
            get
            {
                return this._end - this._start;
            }
        }
        int IDataHandler.Length
        {
            get
            {
                return (int)this.Length;
            }
        }

        private int _index;
        public int Index
        {
            get
            {
                return this._index;
            }
        }
        public override long Position
        {
            get
            {
                return this._index - this._start;
            }
            set
            {
                this.EnsureNotClosed();
                ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));
                ThrowHelper.ArgumentOutOfRange((value > MAX_LENGTH - this._start), nameof(value), SR.Stream_Exception_StreamLength);

                this._index = this._start + (int)value;
            }
        }
        int IDataHandler.Position
        {
            get
            {
                return (int)this.Position;
            }
            set
            {
                this.Position = value;
            }
        }

        public int Remaining
        {
            get
            {
                return this._end - this._index;
            }
        }

        // Note that _capacity == _buffer.Length for non-user-provided byte[]'s
        public int Capacity
        {
            get
            {
                if (this._expandable)
                {
                    return this._buffer.Length - _start;
                }
                else
                {
                    return this._end - this._start;
                }
            }
            set
            {
                this.EnsureNotClosed();
                this.EnsureExpandable();

                // Only update the capacity if the MS is expandable and the value is different than the current capacity.
                // Special behavior if the MS isn't expandable: we don't throw if value is the same as the current capacity
                ThrowHelper.ArgumentOutOfRange((value < this.Length), nameof(value), SR.Stream_Exception_SmallCapacity);
                if ((this._buffer.Length - this._start) == value)
                {
                    return;
                }

                // MemoryStream has this invariant: _origin > 0 => !expandable (see ctors)
                byte[] newBuffer = new byte[this._start + value];
                System.Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _end);
                this._buffer = newBuffer;
            }
        }

        private bool _expandable;  // User-provided buffers aren't expandable.
        private bool _writable;    // Can user write to this stream?
        private bool _isOpen;      // Is this stream open or closed?

        public override bool CanRead
        {
            get
            {
                return this._isOpen;
            }
        }
        public override bool CanSeek
        {
            get
            {
                return this._isOpen;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return this._writable;
            }
        }

        public byte Current
        {
            get
            {
                return this._buffer[this._index];
            }
            set
            {
                this._buffer[this._index] = value;
            }
        }

        private bool _isLittleEndian;

        public BufferStream(int capacity = 0, bool expandable = true)
            : this(capacity, expandable, BitConverter.IsLittleEndian)
        {
        }

        public BufferStream(int capacity, bool expandable, bool isLittleEndian)
        {
            ThrowHelper.ArgumentMustPositive((capacity < 0), nameof(capacity));

            this._buffer = capacity != 0 ? new byte[capacity] : Array.Empty<byte>();
            this._writable = true;
            this._expandable = expandable;
            this._start = 0;
            this._end = 0;
            this._isLittleEndian = isLittleEndian;
            this._isOpen = true;
        }

        public BufferStream(byte[] buffer, bool writable = true)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));

            this.InitFromExternBuffer(buffer, 0, buffer.Length, writable, BitConverter.IsLittleEndian);
        }

        public BufferStream(byte[] buffer, int offset, int count, bool writable = true)
            : this(buffer, offset, count, writable, BitConverter.IsLittleEndian)
        {
        }

        public BufferStream(byte[] buffer, int offset, int count, bool writable, bool isLittleEndian)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((count < 0), nameof(count));
            ThrowHelper.ArgumentIndexLengthOutOfArray((offset + count > buffer.Length));

            this.InitFromExternBuffer(buffer, offset, count, writable, isLittleEndian);
        }

        private void InitFromExternBuffer(byte[] buffer, int offset, int count, bool writable, bool isLittleEndian)
        {
            this._buffer = buffer;
            this._start = offset;
            this._index = offset;
            this._end = offset + count;
            this._writable = writable;
            this._expandable = false;
            this._isLittleEndian = isLittleEndian;
            this._isOpen = true;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    this._isOpen = false;
                    this._writable = false;
                    this._expandable = false;
                    // Don't set buffer to null - allow TryGetBuffer, GetBuffer & ToArray to work.
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void EnsureWriteable()
        {
            if (!this._writable)
            {
                throw new NotSupportedException(SR.Stream_Exception_WriteNotSupport);
            }
        }
        private void EnsureExpandable()
        {
            if (!this._expandable)
            {
                throw new NotSupportedException(SR.Stream_Exception_NotExpandable);
            }
        }
        private void EnsureNotClosed()
        {
            if (!_isOpen)
            {
                throw new ObjectDisposedException(null);
            }
        }
        private bool EnsureCapacity(int value)
        {
            // Check for overflow
            if (value < 0)
            {
                throw new IOException(SR.Stream_Exception_StreamTooLong);
            }

            int currentCapacity = this.Capacity;
            if (value > currentCapacity)
            {
                int newCapacity = value;
                if (newCapacity < INITIAL_LENGTH)
                    newCapacity = INITIAL_LENGTH;
                // We are ok with this overflowing since the next statement will deal
                // with the cases where _capacity*2 overflows.
                if (newCapacity < currentCapacity * 2)
                    newCapacity = currentCapacity * 2;
                // We want to expand the array up to Array.MaxArrayLengthOneDimensional
                // And we want to give the user the value that they asked for
                if ((long)(currentCapacity * 2) > MAX_LENGTH)
                    newCapacity = value > MAX_LENGTH ? value : MAX_LENGTH;

                this.Capacity = newCapacity;
                return true;
            }
            return false;
        }
        private void EnsureGetSpace(int size)
        {
            EnsureNotClosed();

            int newEnd = this._index + size;
            if (newEnd > this._end)
            {
                throw new EndOfStreamException(SR.Stream_Exception_NotExpandable);
            }
        }
        private void EnsureWriteSpace(int size)
        {
            EnsureNotClosed();
            EnsureWriteable();

            int newEnd = this._index + size;
            if (newEnd < 0)
            {
                throw new IOException(SR.Stream_Exception_StreamTooLong);
            }
            if (newEnd > _end)
            {
                bool mustZero = _index > _end;
                if (newEnd > this.Capacity)
                {
                    bool allocatedNewArray = EnsureCapacity(newEnd);
                    if (allocatedNewArray)
                        mustZero = false;
                }
                if (mustZero)
                {
                    Array.Clear(_buffer, _end, newEnd - _end);
                }
                _end = newEnd;
            }
        }

        public int PositionToIndex(int position)
        {
            ThrowHelper.ArgumentOutOfRange(((position < 0) || (position > this.Length)), nameof(position));

            int index = this._start + position;
            return index;
        }

        public int IndexToPosition(int index)
        {
            int position = index - this._start;
            ThrowHelper.ArgumentOutOfRange(((position < 0) || (position > this.Length)), nameof(index));
            return position;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            this.EnsureNotClosed();

            ThrowHelper.ArgumentOutOfRange((offset > MAX_LENGTH), nameof(offset));

            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        int newPosition = unchecked(_start + (int)offset);
                        if (offset < 0 || newPosition < _start)
                            throw new IOException(SR.Stream_Exception_SeekBeforeBegin);
                        _index = newPosition;
                        break;
                    }
                case SeekOrigin.Current:
                    {
                        int newPosition = unchecked(_index + (int)offset);
                        if (unchecked(_index + offset) < _start || newPosition < _start)
                            throw new IOException(SR.Stream_Exception_SeekBeforeBegin);
                        _index = newPosition;
                        break;
                    }
                case SeekOrigin.End:
                    {
                        int newPosition = unchecked(_end + (int)offset);
                        if (unchecked(_end + offset) < _start || newPosition < _start)
                            throw new IOException(SR.Stream_Exception_SeekBeforeBegin);
                        _index = newPosition;
                        break;
                    }
                default:
                    throw new ArgumentException(SR._Exception_EnumNotDefine, nameof(origin));
            }

            Debug.Assert(_index >= 0, "_position >= 0");
            return _index;
        }

        public override void SetLength(long value)
        {
            this.EnsureNotClosed();
            this.EnsureWriteable();

            ThrowHelper.ArgumentLengthOutOfRange((value < 0 || value > BufferStream.MAX_LENGTH), nameof(value));
            ThrowHelper.ArgumentOutOfRange((value > (BufferStream.MAX_LENGTH - this._start)), nameof(value), SR.Stream_Exception_StreamLength);

            int newLength = _start + (int)value;
            bool allocatedNewArray = EnsureCapacity(newLength);
            if (!allocatedNewArray && newLength > _end)
            {
                Array.Clear(_buffer, _end, newLength - _end);
            }
            _end = newLength;
            if (_index > newLength)
            {
                _index = newLength;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            this.EnsureNotClosed();

            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((count < 0), nameof(count));
            ThrowHelper.ArgumentIndexLengthOutOfArray((offset + count > buffer.Length));

            int n = Math.Min(this.Remaining, count);
            if (n <= 0)
            {
                return 0;
            }

            Debug.Assert(_index + n >= 0, "_position + n >= 0");  // len is less than 2^31 -1.

            if (n <= 8)
            {
                int byteCount = n;
                while (--byteCount >= 0)
                {
                    buffer[offset + byteCount] = this._buffer[this._index + byteCount];
                }
            }
            else
            {
                System.Buffer.BlockCopy(_buffer, _index, buffer, offset, n);
            }
            _index += n;
            return n;
        }

        public override int ReadByte()
        {
            this.EnsureNotClosed();

            if (this._index >= this._end)
            {
                return -1;
            }

            return this._buffer[this._index++];
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((count < 0), nameof(count));
            ThrowHelper.ArgumentIndexLengthOutOfArray((offset + count > buffer.Length));

            this.EnsureWriteSpace(count);

            if ((count <= 8) && (buffer != _buffer))
            {
                int byteCount = count;
                while (--byteCount >= 0)
                {
                    _buffer[_index + byteCount] = buffer[offset + byteCount];
                }
            }
            else
            {
                System.Buffer.BlockCopy(buffer, offset, _buffer, _index, count);
            }
            this._index += count;
        }

        public override void WriteByte(byte value)
        {
            this.EnsureWriteSpace(sizeof(byte));

            this._buffer[this._index] = value;
            this._index += sizeof(byte);
        }

        // Writes this MemoryStream to another stream.
        public virtual void WriteTo(Stream stream)
        {
            this.EnsureNotClosed();

            ThrowHelper.ArgumentNull((stream == null), nameof(stream));

            stream.Write(this._buffer, this._start, this._end - this._start);
        }

        public override void Flush()
        {
        }

        public byte[] ToArray()
        {
            byte[] destination = new byte[this._end - this._start];
            System.Buffer.BlockCopy(this._buffer, this._start, destination, 0, this._end - this._start);
            return destination;
        }

        /// <summary>
        /// Create a pin to stored current offset, and restore when dispose
        /// </summary>
        public IDataPin CreateDataPin()
        {
            DataPin pin = new DataPin(this);
            return pin;
        }
        /// <summary>
        /// Create a pin to stored current offset and set newOffset, and restore when dispose
        /// </summary>
        /// <param name="newOffset">new offset set to handler</param>
        public IDataPin CreateDataPin(int newPosition)
        {
            DataPin pin = new DataPin(this);
            this.Position = newPosition;
            return pin;
        }

        public byte[] ReadBytes(int size)
        {
            ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

            this.EnsureGetSpace(size);

            byte[] value = new byte[size];
            this.ReadBytes(value);
            return value;
        }

        public void ReadBytes(byte[] buffer)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));

            this.ReadBytes(buffer, 0, buffer.Length);
        }

        public void ReadBytes(byte[] buffer, int index, int count)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentLengthOutOfRange((count < 0), nameof(count));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + count) > buffer.Length));

            this.EnsureGetSpace(count);

            System.Buffer.BlockCopy(this._buffer, this._index, buffer, index, count);
            this._index += count;
        }

        public T ReadObject<T>() where T : struct
        {
            return (T)this.ReadObject(typeof(T));
        }

        public object ReadObject(Type type)
        {
            ThrowHelper.ArgumentNull((type == null), nameof(type));

            int size = (!type.IsEnum) ? Marshal.SizeOf(type) : Marshal.SizeOf(Enum.GetUnderlyingType(type));
            this.EnsureGetSpace(size);

            Type targetType = (!type.IsEnum) ? type : Enum.GetUnderlyingType(type);
            if (type == typeof(Byte))
            {
                return this.ReadUInt8();
            }
            else if (type == typeof(SByte))
            {
                return this.ReadInt8();
            }
            else if (type == typeof(Boolean))
            {
                return this.ReadBoolean();
            }
            else if (type == typeof(Int16))
            {
                return this.ReadInt16();
            }
            else if (type == typeof(UInt16))
            {
                return this.ReadUInt16();
            }
            else if (type == typeof(Int32))
            {
                return this.ReadInt32();
            }
            else if (type == typeof(UInt32))
            {
                return this.ReadUInt32();
            }
            else if (type == typeof(Int64))
            {
                return this.ReadInt64();
            }
            else if (type == typeof(UInt64))
            {
                return this.ReadUInt64();
            }
            else if (type == typeof(Single))
            {
                return this.ReadSingle();
            }
            else if (type == typeof(Double))
            {
                return this.ReadDouble();
            }
            else if (!type.IsClass)
            {
                object value = Activator.CreateInstance(type);
                foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    field.SetValue(value, this.ReadObject(field.FieldType));
                }
                return value;
            }
            else
            {
                throw new NotSupportedException(SR.DataHandler_Exception_ObjectNotSupport);
            }
        }

        byte IDataHandler.ReadByte()
        {
            return this.ReadUInt8();
        }
        public byte ReadUInt8()
        {
            this.EnsureGetSpace(sizeof(Byte));

            Byte value = this._buffer[this._index];

            this._index += sizeof(Byte);
            return value;
        }

        sbyte IDataHandler.ReadSByte()
        {
            return this.ReadInt8();
        }
        public sbyte ReadInt8()
        {
            this.EnsureGetSpace(sizeof(SByte));

            SByte value = (SByte)this._buffer[this._index];

            this._index += sizeof(SByte);
            return value;
        }

        public bool ReadBoolean()
        {
            this.EnsureGetSpace(sizeof(Boolean));

            Boolean value = this._buffer[this._index] != 0;

            this._index += sizeof(Boolean);
            return value;
        }

        public char ReadChar()
        {
            this.EnsureGetSpace(sizeof(Char));

            Char value;
            if (this._isLittleEndian)
            {
                value = (Char)(this._buffer[this._index + 1] << 8 | this._buffer[this._index + 0] << 0);
            }
            else
            {
                value = (Char)(this._buffer[this._index + 0] << 8 | this._buffer[this._index + 1] << 0);
            }

            this._index += sizeof(Char);
            return value;
        }

        public short ReadInt16()
        {
            this.EnsureGetSpace(sizeof(Int16));

            Int16 value;
            if (this._isLittleEndian)
            {
                value = (Int16)(this._buffer[this._index + 1] << 8 | this._buffer[this._index + 0] << 0);
            }
            else
            {
                value = (Int16)(this._buffer[this._index + 0] << 8 | this._buffer[this._index + 1] << 0);
            }

            this._index += sizeof(Int16);
            return value;
        }

        public ushort ReadUInt16()
        {
            this.EnsureGetSpace(sizeof(UInt16));

            UInt16 value;
            if (this._isLittleEndian)
            {
                value = (UInt16)(this._buffer[this._index + 1] << 8 | this._buffer[this._index + 0] << 0);
            }
            else
            {
                value = (UInt16)(this._buffer[this._index + 0] << 8 | this._buffer[this._index + 1] << 0);
            }

            this._index += sizeof(UInt16);
            return value;
        }

        public int ReadInt32()
        {
            this.EnsureGetSpace(sizeof(Int32));

            Int32 value;
            if (this._isLittleEndian)
            {
                value = (Int32)this._buffer[this._index + 3] << 24
                      | (Int32)this._buffer[this._index + 2] << 16
                      | (Int32)this._buffer[this._index + 1] << 8
                      | (Int32)this._buffer[this._index + 0] << 0;
            }
            else
            {
                value = (Int32)this._buffer[this._index + 0] << 24
                      | (Int32)this._buffer[this._index + 1] << 16
                      | (Int32)this._buffer[this._index + 2] << 8
                      | (Int32)this._buffer[this._index + 3] << 0;
            }

            this._index += sizeof(Int32);
            return value;
        }

        public uint ReadUInt32()
        {
            this.EnsureGetSpace(sizeof(UInt32));

            UInt32 value;
            if (this._isLittleEndian)
            {
                value = (UInt32)this._buffer[this._index + 3] << 24
                      | (UInt32)this._buffer[this._index + 2] << 16
                      | (UInt32)this._buffer[this._index + 1] << 8
                      | (UInt32)this._buffer[this._index + 0] << 0;
            }
            else
            {
                value = (UInt32)this._buffer[this._index + 0] << 24
                      | (UInt32)this._buffer[this._index + 1] << 16
                      | (UInt32)this._buffer[this._index + 2] << 8
                      | (UInt32)this._buffer[this._index + 3] << 0;
            }

            this._index += sizeof(UInt32);
            return value;
        }

        public long ReadInt64()
        {
            this.EnsureGetSpace(sizeof(Int64));

            Int64 value;
            if (this._isLittleEndian)
            {
                value = (Int64)this._buffer[this._index + 7] << 56
                      | (Int64)this._buffer[this._index + 6] << 48
                      | (Int64)this._buffer[this._index + 5] << 40
                      | (Int64)this._buffer[this._index + 4] << 32
                      | (Int64)this._buffer[this._index + 3] << 24
                      | (Int64)this._buffer[this._index + 2] << 16
                      | (Int64)this._buffer[this._index + 1] << 8
                      | (Int64)this._buffer[this._index + 0] << 0;
            }
            else
            {
                value = (Int64)this._buffer[this._index + 0] << 56
                      | (Int64)this._buffer[this._index + 1] << 48
                      | (Int64)this._buffer[this._index + 2] << 40
                      | (Int64)this._buffer[this._index + 3] << 32
                      | (Int64)this._buffer[this._index + 4] << 24
                      | (Int64)this._buffer[this._index + 5] << 16
                      | (Int64)this._buffer[this._index + 6] << 8
                      | (Int64)this._buffer[this._index + 7] << 0;
            }

            this._index += sizeof(Int64);
            return value;
        }

        public ulong ReadUInt64()
        {
            this.EnsureGetSpace(sizeof(UInt64));

            UInt64 value;
            if (this._isLittleEndian)
            {
                value = (UInt64)this._buffer[this._index + 7] << 56
                      | (UInt64)this._buffer[this._index + 6] << 48
                      | (UInt64)this._buffer[this._index + 5] << 40
                      | (UInt64)this._buffer[this._index + 4] << 32
                      | (UInt64)this._buffer[this._index + 3] << 24
                      | (UInt64)this._buffer[this._index + 2] << 16
                      | (UInt64)this._buffer[this._index + 1] << 8
                      | (UInt64)this._buffer[this._index + 0] << 0;
            }
            else
            {
                value = (UInt64)this._buffer[this._index + 0] << 56
                      | (UInt64)this._buffer[this._index + 1] << 48
                      | (UInt64)this._buffer[this._index + 2] << 40
                      | (UInt64)this._buffer[this._index + 3] << 32
                      | (UInt64)this._buffer[this._index + 4] << 24
                      | (UInt64)this._buffer[this._index + 5] << 16
                      | (UInt64)this._buffer[this._index + 6] << 8
                      | (UInt64)this._buffer[this._index + 7] << 0;
            }

            this._index += sizeof(UInt64);
            return value;
        }

        public float ReadSingle()
        {
            Int32 references = this.ReadInt32();
            unsafe
            {
                Single value = *(Single*)&references;
                return value;
            }
        }

        public double ReadDouble()
        {
            Int64 references = this.ReadInt64();
            unsafe
            {
                Double value = *(Double*)&references;
                return value;
            }
        }

        public string ReadStringWithTerminator()
        {
            return this.ReadStringWithTerminator(Encoding.UTF8);
        }

        public string ReadStringWithTerminator(Encoding encoding)
        {
            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

            switch (encoding.CodePage)
            {
                case 1200: // Unicode
                case 1201: // BigEndianUnicode
                    for (int i = this._index; i < this._end; i += sizeof(char))
                    {
                        if (i + sizeof(char) > this._end)
                        {
                            break;
                        }

                        if ((this._buffer[i] == 0) && (this._buffer[i + 1] == 0))
                        {
                            string value = encoding.GetString(this._buffer, this._index, i - this._index);
                            this._index = i + sizeof(char);
                            return value;
                        }
                    }
                    throw new EndOfStreamException(SR.Stream_Exception_NotExpandable);
                default:
                    for (int i = this._index; i < this._end; i += sizeof(byte))
                    {
                        if (this._buffer[i] == 0)
                        {
                            string value = encoding.GetString(this._buffer, this._index, i - this._index);
                            this._index = i + sizeof(byte);
                            return value;
                        }
                    }
                    throw new EndOfStreamException(SR.Stream_Exception_NotExpandable);
            }
        }

        public string ReadString(int length)
        {
            return this.ReadString(length, Encoding.UTF8);
        }

        public string ReadString(int length, Encoding encoding)
        {
            ThrowHelper.ArgumentMustNotNegative((length < 0), nameof(length));
            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

            this.EnsureGetSpace(length);
            string value = encoding.GetString(this._buffer, this._index, length);
            this._index += length;
            return value;
        }

        public void WriteBytes(byte[] values)
        {
            ThrowHelper.ArgumentNull((values == null), nameof(values));

            this.WriteBytes(values, 0, values.Length);
        }

        public void WriteBytes(byte[] values, int index, int count)
        {
            ThrowHelper.ArgumentNull((values == null), nameof(values));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentLengthOutOfRange((count < 0), nameof(count));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + count) > values.Length));

            this.EnsureWriteSpace(count);

            System.Buffer.BlockCopy(values, index, this._buffer, this._index, count);
            this._index += count;
        }

        public void WriteZeroBytes(int count)
        {
            ThrowHelper.ArgumentMustNotNegative(count < 0, nameof(count));

            this.EnsureWriteSpace(count);

            Array.Clear(this._buffer, this._index, count);
            this._index += count;
        }

        public void WriteObject<T>(T value)
        {
            this.WriteObject((object)value);
        }

        public void WriteObject(object value)
        {
            ThrowHelper.ArgumentNull((value == null), nameof(value));

            int size = (!value.GetType().IsEnum) ? Marshal.SizeOf(value) : Marshal.SizeOf(Enum.GetUnderlyingType(value.GetType()));
            this.EnsureWriteSpace(size);

            Algorithms.ObjectConvert.ToBytes(value, size, this._buffer, this._index);
            this._index += sizeof(Byte);
        }

        void IDataHandler.WriteByte(byte value)
        {
            this.WriteUInt8(value);
        }
        public void WriteUInt8(byte value)
        {
            this.EnsureWriteSpace(sizeof(Byte));

            this._buffer[this._index] = value;

            this._index += sizeof(Byte);
        }

        void IDataHandler.WriteSByte(sbyte value)
        {
            this.WriteInt8(value);
        }
        public void WriteInt8(sbyte value)
        {
            this.EnsureWriteSpace(sizeof(SByte));

            this._buffer[this._index] = (Byte)value;

            this._index += sizeof(SByte);
        }

        public void WriteBoolean(bool value)
        {
            this.EnsureWriteSpace(sizeof(Boolean));

            this._buffer[this._index] = value ? (byte)1 : (byte)0;

            this._index += sizeof(Boolean);
        }

        public void WriteChar(char value)
        {
            this.EnsureWriteSpace(sizeof(Char));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteInt16(short value)
        {
            this.EnsureWriteSpace(sizeof(Int16));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteUInt16(ushort value)
        {
            this.EnsureWriteSpace(sizeof(UInt16));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteInt32(int value)
        {
            this.EnsureWriteSpace(sizeof(Int32));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 24);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 24);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteUInt32(uint value)
        {
            this.EnsureWriteSpace(sizeof(UInt32));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 24);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 24);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteInt64(long value)
        {
            this.EnsureWriteSpace(sizeof(Int64));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 24);
                this._buffer[this._index++] = (byte)(value >> 32);
                this._buffer[this._index++] = (byte)(value >> 40);
                this._buffer[this._index++] = (byte)(value >> 48);
                this._buffer[this._index++] = (byte)(value >> 56);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 56);
                this._buffer[this._index++] = (byte)(value >> 48);
                this._buffer[this._index++] = (byte)(value >> 40);
                this._buffer[this._index++] = (byte)(value >> 32);
                this._buffer[this._index++] = (byte)(value >> 24);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteUInt64(ulong value)
        {
            this.EnsureWriteSpace(sizeof(UInt64));

            if (this._isLittleEndian)
            {
                this._buffer[this._index++] = (byte)(value >> 0);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 24);
                this._buffer[this._index++] = (byte)(value >> 32);
                this._buffer[this._index++] = (byte)(value >> 40);
                this._buffer[this._index++] = (byte)(value >> 48);
                this._buffer[this._index++] = (byte)(value >> 56);
            }
            else
            {
                this._buffer[this._index++] = (byte)(value >> 56);
                this._buffer[this._index++] = (byte)(value >> 48);
                this._buffer[this._index++] = (byte)(value >> 40);
                this._buffer[this._index++] = (byte)(value >> 32);
                this._buffer[this._index++] = (byte)(value >> 24);
                this._buffer[this._index++] = (byte)(value >> 16);
                this._buffer[this._index++] = (byte)(value >> 8);
                this._buffer[this._index++] = (byte)(value >> 0);
            }
        }

        public void WriteSingle(float value)
        {
            unsafe
            {
                Int32 references = *(Int32*)&value;
                this.WriteInt32(references);
            }
        }

        public void WriteDouble(double value)
        {
            unsafe
            {
                Int64 references = *(Int64*)&value;
                this.WriteInt64(references);
            }
        }

        public void WriteStringWithTerminator(string value)
        {
            this.WriteStringWithTerminator(value, Encoding.UTF8);
        }

        public void WriteStringWithTerminator(string value, Encoding encoding)
        {
            ThrowHelper.ArgumentNull((value == null), nameof(value));
            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

            int count;
            switch (encoding.CodePage)
            {
                case 1200: // Unicode
                case 1201: // BigEndianUnicode
                    count = sizeof(char);
                    break;
                default:
                    count = sizeof(byte);
                    break;
            }
            count += encoding.GetByteCount(value);
            this.EnsureWriteSpace(count);

            encoding.GetBytes(value, 0, value.Length, this._buffer, this._index);
            this._index += count;
        }

        public void WriteString(string value)
        {
            this.WriteString(value, Encoding.UTF8);
        }

        public void WriteString(string value, Encoding encoding)
        {
            ThrowHelper.ArgumentNull((value == null), nameof(value));
            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

            int count = encoding.GetByteCount(value);
            this.EnsureWriteSpace(count);

            encoding.GetBytes(value, 0, value.Length, this._buffer, this._index);
            this._index += count;
        }

        public byte[] ExportAllBytes()
        {
            return this.ToArray();
        }

        public byte[] ExportUsedBytes()
        {
            int end = Math.Min(this._end, this._index);
            byte[] destination = new byte[end - this._start];
            System.Buffer.BlockCopy(this._buffer, this._start, destination, 0, end - this._start);
            return destination;
        }
    }
}
