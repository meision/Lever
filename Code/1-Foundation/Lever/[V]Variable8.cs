using System;
using System.Runtime.InteropServices;
using Meision.Algorithms;
using Meision.Resources;

namespace Meision
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Variable8
    {
        #region Static
        public static bool operator ==(Variable8 item1, Variable8 item2)
        {
            return (item1._value == item2._value);
        }

        public static bool operator !=(Variable8 item1, Variable8 item2)
        {
            return !(item1 == item2);
        }
        #endregion Static

        #region Field & Property
        private SByte _value;

        public SByte SByte
        {
            get
            {
                return this._value;
            }
        }

        public Byte Byte
        {
            get
            {
                return (Byte)this._value;
            }
        }
        #endregion Field & Property

        #region Constructor
        public Variable8(SByte value)
        {
            this._value = value;
        }

        public Variable8(byte[] buffer)
            : this(buffer, 0, buffer.Length)
        {
        }

        public Variable8(byte[] bytes, int index, int count)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
            ThrowHelper.ArgumentException((count != sizeof(Byte)), SR.Variable8_Exception_SizeUnexpected, nameof(count));

            unsafe
            {
                fixed (byte* point = bytes)
                {
                    this._value = *((SByte*)(point + index));
                }
            }
        }
        #endregion Constructor

        #region Method
        public bool Equals(Variable8 item)
        {
            return (this._value == item._value);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Variable8))
            {
                return false;
            }

            return (this._value == ((Variable8)obj)._value);
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
