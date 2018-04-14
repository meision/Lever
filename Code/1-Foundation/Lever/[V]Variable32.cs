using System;
using System.Runtime.InteropServices;
using Meision.Algorithms;
using Meision.Resources;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Variable32
    {
        #region Static
        public static bool operator ==(Variable32 item1, Variable32 item2)
        {
            return (item1._value == item2._value);
        }

        public static bool operator !=(Variable32 item1, Variable32 item2)
        {
            return !(item1 == item2);
        }
        #endregion Static

        #region Field & Property
        private Int32 _value;

        public Int32 Int32
        {
            get
            {
                return this._value;
            }
        }

        public UInt32 UInt32
        {
            get
            {
                return (UInt32)this._value;
            }
        }

        public Single Single
        {
            get
            {
                unsafe
                {
                    fixed (Int32* point = &this._value)
                    {
                        return *(Single*)(byte*)point;
                    }
                }
            }
        }

        public Tuple<Variable16, Variable16> Tuple
        {
            get
            {
                short high = Number.GetHigh(this._value);
                short low = Number.GetLow(this._value);
                return new Tuple<Variable16, Variable16>(
                    new Variable16(high),
                    new Variable16(low));
            }
        }
        #endregion Field & Property

        #region Constructor
        public Variable32(Int32 value)
        {
            this._value = value;
        }

        public Variable32(byte[] buffer)
            : this(buffer, 0, buffer.Length)
        {
        }

        public Variable32(byte[] bytes, int index, int count)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
            ThrowHelper.ArgumentException((count != sizeof(Int32)), SR.Variable32_Exception_SizeUnexpected, nameof(count));

            unsafe
            {
                fixed (byte* point = bytes)
                {
                    this._value = *((Int32*)(point + index));
                }
            }
        }
        #endregion Constructor

        #region Method
        public bool Equals(Variable32 item)
        {
            return (this._value == item._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Variable32))
            {
                return false;
            }

            return (this._value == ((Variable32)obj)._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public byte[] ToArray()
        {
            return Endian.Little.ToBytes(this._value);
        }

        public override string ToString()
        {
            return this._value.ToString();
        }
        #endregion Method
    }
}
