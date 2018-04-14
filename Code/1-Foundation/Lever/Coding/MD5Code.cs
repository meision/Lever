using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Meision.Algorithms;
using Meision.Resources;

namespace Meision.Coding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MD5Code
    {
        #region Static
        public const int ByteLength = 16;
        public static readonly string RegularExpression = "[A-Za-z0-9]{32}";
        private const string UpperFormat = "{0:X16}{1:X16}";
        private const string LowerFormat = "{0:x16}{1:x16}";

        private static readonly MD5Code __empty = new MD5Code();
        public static MD5Code Empty
        {
            get
            {
                return MD5Code.__empty;
            }
        }

        public static bool operator ==(MD5Code code1, MD5Code code2)
        {
            return code1.Equals(code2);
        }

        public static bool operator !=(MD5Code code1, MD5Code code2)
        {
            return !code1.Equals(code2);
        }

        public static MD5Code HashFrom(byte[] buffer)
        {
            return MD5Code.HashFrom(buffer, 0, buffer.Length);
        }
        public static MD5Code HashFrom(byte[] buffer, int offset, int length)
        {
            byte[] data = Cryptography.MD5Encode(buffer, offset, length);
            return new MD5Code(data);
        }
        #endregion Static

        #region Field & Property
        private long _value1;
        private long _value2;
        #endregion Field & Property

        #region Constructor
        public MD5Code(string value)
        {
            ThrowHelper.ArgumentNull((value == null), nameof(value));
            ThrowHelper.ArgumentException((value.Length != MD5Code.ByteLength * Meision.Algorithms.UnitTable.HexTextCountPerByte), SR.MD5Code_Exception_InvalidText, nameof(value));
            ThrowHelper.ArgumentException((!Regex.IsMatch(value, "^" + RegularExpression + "$")), SR.MD5Code_Exception_InvalidText, nameof(value));

            unsafe
            {
                fixed (MD5Code* p = &this)
                {
                    byte* pointer = (byte*)p;
                    for (int i = 0; i < value.Length; i += 2)
                    {
                        *(pointer + i / 2) = Calculator.GetHexValue(value[i], value[i + 1]);
                    }
                }
            }
        }

        public MD5Code(byte[] codes)
        {
            ThrowHelper.ArgumentNull((codes == null), nameof(codes));
            ThrowHelper.ArgumentException((codes.Length != MD5Code.ByteLength), SR.MD5Code_Exception_InvalidBytes, nameof(codes));

            unsafe
            {
                fixed (MD5Code* p = &this)
                {
                    byte* pointer = (byte*)p;
                    for (int i = 0; i < MD5Code.ByteLength; i++)
                    {
                        *(pointer + i) = codes[i];
                    }
                }
            }
        }
        #endregion Constructor

        #region Method
        public bool Equals(MD5Code code)
        {
            return (this._value1 == code._value1) && (this._value2 == code._value2);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is MD5Code))
            {
                return false;
            }

            return this.Equals((MD5Code)obj);
        }

        public override int GetHashCode()
        {
            return this._value1.GetHashCode() ^ this._value2.GetHashCode();
        }

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[MD5Code.ByteLength];
            unsafe
            {
                fixed (MD5Code* p = &this)
                {
                    byte* pointer = (byte*)p;
                    for (int i = 0; i < MD5Code.ByteLength; i++)
                    {
                        buffer[i] = pointer[i];
                    }
                }
            }

            return buffer;
        }

        public string ToString(bool capitalization)
        {
            string format = capitalization ? MD5Code.UpperFormat : MD5Code.LowerFormat;
            char[] items = new char[MD5Code.ByteLength * Meision.Algorithms.UnitTable.HexTextCountPerByte];
            unsafe
            {
                fixed (MD5Code* p = &this)
                {
                    byte* pointer = (byte*)p;
                    for (int i = 0; i < MD5Code.ByteLength; i++)
                    {
                        Calculator.ToHexStringInner(pointer[i], items, i * 2, capitalization);
                    }
                }
            }

            string text = new string(items);
            return text;
        }

        public override string ToString()
        {
            return this.ToString(true);
        }
        #endregion Constructor
    }
}
