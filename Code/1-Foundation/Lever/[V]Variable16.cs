using System;
using System.Runtime.InteropServices;
using Meision.Algorithms;
using Meision.Resources;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Variable16
    {
        #region Static
        public static bool operator ==(Variable16 item1, Variable16 item2)
        {
            return (item1._value == item2._value);
        }

        public static bool operator !=(Variable16 item1, Variable16 item2)
        {
            return !(item1 == item2);
        }
        #endregion Static
        
        #region Field & Property
        private Int16 _value;
        
        public Int16 Int16
        {
            get
            {
                return this._value;
            }
        }

        public UInt16 UInt16
        {
            get
            {
                return (UInt16)this._value;
            }
        }

        public Tuple<Variable8, Variable8> Tuple
        {
            get
            {
                sbyte high = Number.GetHigh(this._value);
                sbyte low = Number.GetLow(this._value);
                return new Tuple<Variable8, Variable8>(
                    new Variable8(high),
                    new Variable8(low));
            }
        }
        #endregion Field & Property
        
        #region Constructor
        public Variable16(short value)
        {
            this._value = value;
        }

        public Variable16(byte[] buffer)
            : this(buffer, 0, buffer.Length)
        {
        }

        public Variable16(byte[] bytes, int index, int count)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
            ThrowHelper.ArgumentException((count != sizeof(Int16)), SR.Variable16_Exception_SizeUnexpected, nameof(count));

            unsafe
            {
                fixed (byte* point = bytes)
                {
                    this._value = *((Int16*)(point + index));
                }
            }
        }
        #endregion Constructor

        #region Method        
        public bool Equals(Variable16 item)
        {
            return (this._value == item._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Variable16))
            {
                return false;
            }

            return (this._value == ((Variable16)obj)._value);
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
