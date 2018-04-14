using System;

namespace Meision.Algorithms
{
    public abstract class Endian
    {

        #region Nested Type

        private sealed class BigEndian : Endian
        {

            #region Byte
            public override byte[] ToBytes(Byte value)
            {
                byte[] buffer = new byte[sizeof(Byte)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Byte value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Byte)) > bytes.Length), nameof(index));

                bytes[index] = value;
            }

            public override Byte GetByte(byte[] data)
            {
                return this.GetByte(data, 0);
            }

            public override Byte GetByte(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Byte)) > data.Length), nameof(index));

                return data[index];
            }
            #endregion Byte

            #region SByte
            public override byte[] ToBytes(SByte value)
            {
                byte[] buffer = new byte[sizeof(SByte)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(SByte value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(SByte)) > bytes.Length), nameof(index));

                bytes[index] = (Byte)value;
            }

            public override SByte GetSByte(byte[] bytes)
            {
                return this.GetSByte(bytes, 0);
            }

            public override SByte GetSByte(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(SByte)) > bytes.Length), nameof(index));

                return (SByte)bytes[index];
            }
            #endregion SByte

            #region Boolean
            public override byte[] ToBytes(Boolean value)
            {
                byte[] buffer = new byte[sizeof(Boolean)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Boolean value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Boolean)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Boolean));
            }

            public override Boolean GetBoolean(byte[] data)
            {
                return this.GetBoolean(data, 0);
            }

            public override Boolean GetBoolean(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Boolean)) > data.Length), nameof(index));

                return data[index] != 0;
            }
            #endregion Boolean

            #region Char
            public override byte[] ToBytes(Char value)
            {
                byte[] buffer = new byte[sizeof(Char)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Char value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Char)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Char));
            }

            public override Char GetChar(byte[] data)
            {
                return this.GetChar(data, 0);
            }

            public override Char GetChar(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Char)) > data.Length), nameof(index));

                return (Char)this.GetUInt16(data, index);
            }
            #endregion Char

            #region Int16
            public override byte[] ToBytes(Int16 value)
            {
                byte[] buffer = new byte[sizeof(Int16)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Int16 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int16)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Int16));
            }

            public override Int16 GetInt16(byte[] data)
            {
                return this.GetInt16(data, 0);
            }

            public override Int16 GetInt16(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int16)) > data.Length), nameof(index));

                Int16 temp = 0;
                int end = index + sizeof(Int16);
                for (int i = index; i < end; i++)
                {
                    temp <<= Meision.Algorithms.UnitTable.ByteToBit;
                    temp |= ((Int16)data[i]);
                }
                return temp;
            }
            #endregion Int16

            #region UInt16
            public override byte[] ToBytes(UInt16 value)
            {
                byte[] buffer = new byte[sizeof(UInt16)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(UInt16 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt16)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(UInt16));
            }

            public override UInt16 GetUInt16(byte[] data)
            {
                return this.GetUInt16(data, 0);
            }

            public override UInt16 GetUInt16(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt16)) > data.Length), nameof(index));

                UInt16 temp = 0;
                int end = index + sizeof(UInt16);
                for (int i = index; i < end; i++)
                {
                    temp <<= Meision.Algorithms.UnitTable.ByteToBit;
                    temp |= (UInt16)data[i];
                }
                return temp;
            }
            #endregion UInt16

            #region Int32
            public override byte[] ToBytes(Int32 value)
            {
                byte[] buffer = new byte[sizeof(Int32)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Int32 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int32)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Int32));
            }

            public override Int32 GetInt32(byte[] data)
            {
                return this.GetInt32(data, 0);
            }

            public override Int32 GetInt32(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int32)) > data.Length), nameof(index));

                Int32 temp = 0;
                int end = index + sizeof(Int32);
                for (int i = index; i < end; i++)
                {
                    temp <<= Meision.Algorithms.UnitTable.ByteToBit;
                    temp |= (Int32)data[i];
                }
                return temp;
            }
            #endregion Int32

            #region UInt32
            public override byte[] ToBytes(UInt32 value)
            {
                byte[] buffer = new byte[sizeof(UInt32)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(UInt32 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt32)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(UInt32));
            }

            public override UInt32 GetUInt32(byte[] data)
            {
                return this.GetUInt32(data, 0);
            }

            public override UInt32 GetUInt32(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt32)) > data.Length), nameof(index));

                UInt32 temp = 0;
                int end = index + sizeof(UInt32);
                for (int i = index; i < end; i++)
                {
                    temp <<= Meision.Algorithms.UnitTable.ByteToBit;
                    temp |= (UInt32)data[i];
                }
                return temp;
            }
            #endregion UInt32

            #region Int64
            public override byte[] ToBytes(Int64 value)
            {
                byte[] buffer = new byte[sizeof(Int64)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Int64 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int64)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Int64));
            }

            public override Int64 GetInt64(byte[] data)
            {
                return this.GetInt64(data, 0);
            }

            public override Int64 GetInt64(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int64)) > data.Length), nameof(index));

                Int64 temp = 0;
                int end = index + sizeof(Int64);
                for (int i = index; i < end; i++)
                {
                    temp <<= Meision.Algorithms.UnitTable.ByteToBit;
                    temp |= (Int64)data[i];
                }
                return temp;
            }
            #endregion Int64

            #region UInt64
            public override byte[] ToBytes(UInt64 value)
            {
                byte[] buffer = new byte[sizeof(UInt64)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(UInt64 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt64)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(UInt64));
            }

            public override UInt64 GetUInt64(byte[] data)
            {
                return this.GetUInt64(data, 0);
            }

            public override UInt64 GetUInt64(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt64)) > data.Length), nameof(index));

                UInt64 temp = 0;
                int end = index + sizeof(UInt64);
                for (int i = index; i < end; i++)
                {
                    temp <<= Meision.Algorithms.UnitTable.ByteToBit;
                    temp |= (UInt64)data[i];
                }
                return temp;
            }
            #endregion UInt64

            #region Single
            public override byte[] ToBytes(Single value)
            {
                byte[] buffer = new byte[sizeof(Single)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Single value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Single)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Single));
            }

            public override Single GetSingle(byte[] data)
            {
                return this.GetSingle(data, 0);
            }

            public override unsafe Single GetSingle(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Single)) > data.Length), nameof(index));

                Int32 temp = this.GetInt32(data, index);
                return *(((float*)&temp));
            }
            #endregion Single

            #region Double
            public override byte[] ToBytes(Double value)
            {
                byte[] buffer = new byte[sizeof(Double)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Double value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Double)) > bytes.Length), nameof(index));

                Endian.__little.ToBytes(value, bytes, index);
                Meision.Collections.Bytes.Reverse(bytes, index, sizeof(Double));
            }

            public override Double GetDouble(byte[] data)
            {
                return this.GetDouble(data, 0);
            }

            public override unsafe Double GetDouble(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Double)) > data.Length), nameof(index));

                Int64 temp = this.GetInt64(data, index);
                return *(((float*)&temp));
            }
            #endregion Double

        }


        private sealed class LittleEndian : Endian
        {

            #region Byte
            public override byte[] ToBytes(Byte value)
            {
                byte[] buffer = new byte[sizeof(Byte)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(Byte value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Byte)) > bytes.Length), nameof(index));

                bytes[index] = value;
            }

            public override Byte GetByte(byte[] data)
            {
                return this.GetByte(data, 0);
            }

            public override Byte GetByte(byte[] data, int index)
            {
                ThrowHelper.ArgumentNull((data == null), nameof(data));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Byte)) > data.Length), nameof(index));

                return data[index];
            }
            #endregion Byte

            #region SByte
            public override byte[] ToBytes(SByte value)
            {
                byte[] buffer = new byte[sizeof(SByte)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override void ToBytes(SByte value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(SByte)) > bytes.Length), nameof(index));

                bytes[index] = (Byte)value;
            }

            public override SByte GetSByte(byte[] bytes)
            {
                return this.GetSByte(bytes, 0);
            }

            public override SByte GetSByte(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(SByte)) > bytes.Length), nameof(index));

                return (SByte)bytes[index];
            }
            #endregion SByte

            #region Boolean
            public override byte[] ToBytes(Boolean value)
            {
                byte[] buffer = new byte[sizeof(Boolean)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Boolean value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Boolean)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Boolean*)(pointer + index)) = value;
                }
            }

            public override Boolean GetBoolean(byte[] bytes)
            {
                return this.GetBoolean(bytes, 0);
            }

            public override unsafe Boolean GetBoolean(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Boolean)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Boolean*)(pointer + index));
                }
            }
            #endregion Boolean

            #region Char
            public override byte[] ToBytes(Char value)
            {
                byte[] buffer = new byte[sizeof(Char)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Char value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Char)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Char*)(pointer + index)) = value;
                }
            }

            public override Char GetChar(byte[] bytes)
            {
                return this.GetChar(bytes, 0);
            }

            public override unsafe Char GetChar(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Char)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Char*)(pointer + index));
                }
            }
            #endregion Char

            #region Int16
            public override byte[] ToBytes(Int16 value)
            {
                byte[] buffer = new byte[sizeof(Int16)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Int16 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int16)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Int16*)(pointer + index)) = value;
                }
            }

            public override Int16 GetInt16(byte[] bytes)
            {
                return this.GetInt16(bytes, 0);
            }

            public override unsafe Int16 GetInt16(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int16)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Int16*)(pointer + index));
                }
            }
            #endregion Int16

            #region UInt16
            public override byte[] ToBytes(UInt16 value)
            {
                byte[] buffer = new byte[sizeof(UInt16)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(UInt16 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt16)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((UInt16*)(pointer + index)) = value;
                }
            }

            public override UInt16 GetUInt16(byte[] bytes)
            {
                return this.GetUInt16(bytes, 0);
            }

            public override unsafe UInt16 GetUInt16(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt16)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((UInt16*)(pointer + index));
                }
            }
            #endregion UInt16

            #region Int32
            public override byte[] ToBytes(Int32 value)
            {
                byte[] buffer = new byte[sizeof(Int32)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Int32 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int32)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Int32*)(pointer + index)) = value;
                }
            }

            public override Int32 GetInt32(byte[] bytes)
            {
                return this.GetInt32(bytes, 0);
            }

            public override unsafe Int32 GetInt32(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int32)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Int32*)(pointer + index));
                }
            }
            #endregion Int32

            #region UInt32
            public override byte[] ToBytes(UInt32 value)
            {
                byte[] buffer = new byte[sizeof(UInt32)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(UInt32 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt32)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((UInt32*)(pointer + index)) = value;
                }
            }

            public override UInt32 GetUInt32(byte[] bytes)
            {
                return this.GetUInt32(bytes, 0);
            }

            public override unsafe UInt32 GetUInt32(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt32)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((UInt32*)(pointer + index));
                }
            }
            #endregion UInt32

            #region Int64
            public override byte[] ToBytes(Int64 value)
            {
                byte[] buffer = new byte[sizeof(Int64)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Int64 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int64)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Int64*)(pointer + index)) = value;
                }
            }

            public override Int64 GetInt64(byte[] bytes)
            {
                return this.GetInt64(bytes, 0);
            }

            public override unsafe Int64 GetInt64(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Int64)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Int64*)(pointer + index));
                }
            }
            #endregion Int64

            #region UInt64
            public override byte[] ToBytes(UInt64 value)
            {
                byte[] buffer = new byte[sizeof(UInt64)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(UInt64 value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt64)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((UInt64*)(pointer + index)) = value;
                }
            }

            public override UInt64 GetUInt64(byte[] bytes)
            {
                return this.GetUInt64(bytes, 0);
            }

            public override unsafe UInt64 GetUInt64(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(UInt64)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((UInt64*)(pointer + index));
                }
            }
            #endregion UInt64

            #region Single
            public override byte[] ToBytes(Single value)
            {
                byte[] buffer = new byte[sizeof(Single)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Single value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Single)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Single*)(pointer + index)) = value;
                }
            }

            public override Single GetSingle(byte[] bytes)
            {
                return this.GetSingle(bytes, 0);
            }

            public override unsafe Single GetSingle(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Single)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Single*)(pointer + index));
                }
            }
            #endregion Single

            #region Double
            public override byte[] ToBytes(Double value)
            {
                byte[] buffer = new byte[sizeof(Double)];
                this.ToBytes(value, buffer, 0);
                return buffer;
            }

            public override unsafe void ToBytes(Double value, byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Double)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    *((Double*)(pointer + index)) = value;
                }
            }

            public override Double GetDouble(byte[] bytes)
            {
                return this.GetDouble(bytes, 0);
            }

            public override unsafe Double GetDouble(byte[] bytes, int index)
            {
                ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
                ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
                ThrowHelper.ArgumentIndexOutOfRange(((index + sizeof(Double)) > bytes.Length), nameof(index));

                fixed (byte* pointer = bytes)
                {
                    return *((Double*)(pointer + index));
                }
            }
            #endregion Double


        }
        #endregion Nested Type

        #region Static
        private static readonly Endian __big = new BigEndian();
        public static Endian Big
        {
            get
            {
                return __big;
            }
        }

        private static readonly Endian __little = new LittleEndian();
        public static Endian Little
        {
            get
            {
                return __little;
            }
        }
        #endregion Static

        #region Constructor
        #endregion Constructor

        #region Method

        #region Byte
        public abstract byte[] ToBytes(Byte value);

        public abstract void ToBytes(Byte value, byte[] bytes, int index);

        public abstract Byte GetByte(byte[] bytes);

        public abstract Byte GetByte(byte[] bytes, int index);
        #endregion Byte

        #region SByte
        public abstract byte[] ToBytes(SByte value);

        public abstract void ToBytes(SByte value, byte[] bytes, int index);

        public abstract SByte GetSByte(byte[] bytes);

        public abstract SByte GetSByte(byte[] bytes, int index);
        #endregion SByte

        #region Boolean
        public abstract byte[] ToBytes(Boolean value);

        public abstract void ToBytes(Boolean value, byte[] bytes, int index);

        public abstract Boolean GetBoolean(byte[] bytes);

        public abstract Boolean GetBoolean(byte[] bytes, int index);
        #endregion Boolean

        #region Char
        public abstract byte[] ToBytes(Char value);

        public abstract void ToBytes(Char value, byte[] bytes, int index);

        public abstract Char GetChar(byte[] bytes);

        public abstract Char GetChar(byte[] bytes, int index);
        #endregion Char

        #region Int16
        public abstract byte[] ToBytes(Int16 value);

        public abstract void ToBytes(Int16 value, byte[] bytes, int index);

        public abstract Int16 GetInt16(byte[] bytes);

        public abstract Int16 GetInt16(byte[] bytes, int index);
        #endregion Int16

        #region UInt16
        public abstract byte[] ToBytes(UInt16 value);

        public abstract void ToBytes(UInt16 value, byte[] bytes, int index);

        public abstract UInt16 GetUInt16(byte[] bytes);

        public abstract UInt16 GetUInt16(byte[] bytes, int index);
        #endregion UInt16

        #region Int32
        public abstract byte[] ToBytes(Int32 value);

        public abstract void ToBytes(Int32 value, byte[] bytes, int index);

        public abstract Int32 GetInt32(byte[] bytes);

        public abstract Int32 GetInt32(byte[] bytes, int index);
        #endregion Int32

        #region UInt32
        public abstract byte[] ToBytes(UInt32 value);

        public abstract void ToBytes(UInt32 value, byte[] bytes, int index);

        public abstract UInt32 GetUInt32(byte[] bytes);

        public abstract UInt32 GetUInt32(byte[] bytes, int index);
        #endregion UInt32

        #region Int64
        public abstract byte[] ToBytes(Int64 value);

        public abstract void ToBytes(Int64 value, byte[] bytes, int index);

        public abstract Int64 GetInt64(byte[] bytes);

        public abstract Int64 GetInt64(byte[] bytes, int index);
        #endregion Int64

        #region UInt64
        public abstract byte[] ToBytes(UInt64 value);

        public abstract void ToBytes(UInt64 value, byte[] bytes, int index);

        public abstract UInt64 GetUInt64(byte[] bytes);

        public abstract UInt64 GetUInt64(byte[] bytes, int index);
        #endregion UInt64

        #region Single
        public abstract byte[] ToBytes(Single value);

        public abstract void ToBytes(Single value, byte[] bytes, int index);

        public abstract Single GetSingle(byte[] bytes);

        public abstract Single GetSingle(byte[] bytes, int index);
        #endregion Single

        #region Double
        public abstract byte[] ToBytes(Double value);

        public abstract void ToBytes(Double value, byte[] bytes, int index);

        public abstract Double GetDouble(byte[] bytes);

        public abstract Double GetDouble(byte[] bytes, int index);
        #endregion Double

        #endregion Method
    }
}
