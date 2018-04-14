using System;
using System.Runtime.InteropServices;
using Meision.Algorithms;
using Meision.Resources;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Variable64
    {
        #region Static
        public static bool operator ==(Variable64 item1, Variable64 item2)
        {
            return (item1._value == item2._value);
        }

        public static bool operator !=(Variable64 item1, Variable64 item2)
        {
            return !(item1 == item2);
        }
        #endregion Static

        #region Field & Property
        private Int64 _value;
        
        public Int64 Int64
        {
            get
            {
                return this._value;
            }
        }

        public UInt64 UInt64
        {
            get
            {
                return (UInt64)this._value;
            }
        }

        public Double Double
        {
            get
            {
                unsafe
                {
                    fixed (Int64* point = &this._value)
                    {
                        return *(Double*)(byte*)point;
                    }
                }
            }
        }

        public Tuple<Variable32, Variable32> Tuple
        {
            get
            {
                int high = Number.GetHigh(this._value);
                int low = Number.GetLow(this._value);
                return new Tuple<Variable32, Variable32>(
                    new Variable32(high),
                    new Variable32(low));
            }
        }

        #endregion Field & Property

        #region Constructor
        public Variable64(long value)
        {
            this._value = value;
        }

        public Variable64(byte[] buffer)
            : this(buffer, 0, buffer.Length)
        {
        }

        public Variable64(byte[] bytes, int index, int count)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
            ThrowHelper.ArgumentException((count != sizeof(Int64)), SR.Variable64_Exception_SizeUnexpected, nameof(count));

            unsafe
            {
                fixed (byte* point = bytes)
                {
                    this._value = *((Int64*)(point + index));
                }
            }
        }
        #endregion Constructor

        #region Method
        public bool Equals(Variable64 item)
        {
            return (this._value == item._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Variable64))
            {
                return false;
            }

            return (this._value == ((Variable64)obj)._value);
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
