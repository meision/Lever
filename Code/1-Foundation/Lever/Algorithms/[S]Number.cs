using System;
using System.Collections.Generic;
using System.Text;

namespace Meision.Algorithms
{
    public static class Number
    {
        #region BaseXEncode
        private const string Base2Templates = "01";

        private const string Base4Templates = "0123";

        private const string Base8Templates = "01234567";

        private const string Base16LowerTemplates = "0123456789abcdef";
        private const string Base16UpperTemplates = "0123456789ABCDEF";

        private const string Base32LowerTemplates = "0123456789abcdefghijklmnopqrstuv";
        private const string Base32UpperTemplates = "0123456789ABCDEFGHIJKLMNOPQRSTUV";

        private const string Base36LowerTemplates = "0123456789abcdefghijklmnopqrstuvwxyz";
        private const string Base36UpperTemplates = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const string Base62Templates = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static readonly Func<char, int> Searching = (c) =>
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            else if (c >= 'a' && c <= 'z')
            {
                return c - 'a' + 10;
            }
            else if (c >= 'A' && c <= 'Z')
            {
                return c - 'A' + 36;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(c));
            }
        };


        public static string BaseXEncode(long value, string templates)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));
            ThrowHelper.ArgumentNull((templates == null), nameof(templates));
            ThrowHelper.ArgumentStringMustNotEmpty((templates.Length == 0), nameof(templates));

            long current = value;
            System.Collections.Generic.Stack<char> result = new System.Collections.Generic.Stack<char>();
            do
            {
                result.Push(templates[(int)(current % templates.Length)]);
                current /= templates.Length;
            }
            while (current != 0);
            return new string(result.ToArray());
        }
        private static long BaseXDecode(string text, string templates, Func<char, int> searching)
        {
            long current = 0;
            foreach (char c in text)
            {
                current = current * templates.Length + searching(c);
            }
            return current;
        }
        public static long BaseXDecode(string text, string templates)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));
            ThrowHelper.ArgumentNull((templates == null), nameof(templates));
            ThrowHelper.ArgumentStringMustNotEmpty((templates.Length == 0), nameof(templates));

            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int i = 0; i < templates.Length; i++)
            {
                dictionary[templates[i]] = i;
            }
            return BaseXDecode(text, templates, (c) => dictionary[c]);
        }

        public static string Base2Encode(long value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            return BaseXEncode(value, Base2Templates);
        }
        public static long Base2Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text, Base2Templates, Searching);
        }

        public static string Base4Encode(long value)
        {
            return BaseXEncode(value, Base4Templates);
        }
        public static long Base4Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text, Base4Templates, Searching);
        }

        public static string Base8Encode(long value)
        {
            return BaseXEncode(value, Base8Templates);
        }
        public static long Base8Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text, Base8Templates, Searching);
        }

        public static string Base16Encode(long value, bool upperCase = false)
        {
            return BaseXEncode(value, upperCase ? Base16UpperTemplates : Base16LowerTemplates);
        }
        public static long Base16Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text.ToLowerInvariant(), Base16LowerTemplates, Searching);
        }

        public static string Base32Encode(long value, bool upperCase = false)
        {
            return BaseXEncode(value, upperCase ? Base32UpperTemplates : Base32LowerTemplates);
        }
        public static long Base32Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text.ToLowerInvariant(), Base32LowerTemplates, Searching);
        }

        public static string Base36Encode(long value, bool upperCase = false)
        {
            return BaseXEncode(value, upperCase ? Base36UpperTemplates : Base36LowerTemplates);
        }
        public static long Base36Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text.ToLowerInvariant(), Base36LowerTemplates, Searching);
        }

        public static string Base62Encode(long value)
        {
            return BaseXEncode(value, Base62Templates);
        }
        public static long Base62Decode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            ThrowHelper.ArgumentStringMustNotEmpty((text.Length == 0), nameof(text));

            return BaseXDecode(text, Base62Templates, Searching);
        }

        #endregion BaseXEncode

        #region Interval


        public static bool InOpenInterval<T>(T element, T left, T right) where T : IComparable
        {
            return (left.CompareTo(element) <= 0) && (element.CompareTo(right) <= 0);
        }

        public static bool InClosedInterval<T>(T element, T left, T right) where T : IComparable
        {
            return (left.CompareTo(element) < 0) && (element.CompareTo(right) < 0);
        }

        public static bool InLeftOpenRightClosedInterval<T>(T element, T left, T right) where T : IComparable
        {
            return (left.CompareTo(element) <= 0) && (element.CompareTo(right) < 0);
        }

        public static bool InLeftClosedRightOpenInterval<T>(T element, T left, T right) where T : IComparable
        {
            return (left.CompareTo(element) < 0) && (element.CompareTo(right) <= 0);
        }
        #endregion Interval

        public static bool In<T>(T value, params T[] items) where T : IComparable
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            foreach (T item in items)
            {
                if (item.CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static T GetMax<T>(params T[] items) where T : IComparable
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            T max = items[0];
            for (int i = 1; i < items.Length; i++)
            {
                if (items[i].CompareTo(max) > 0)
                {
                    max = items[i];
                }
            }
            return max;
        }

        public static T GetMin<T>(params T[] items) where T : IComparable
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            T min = items[0];
            for (int i = 1; i < items.Length; i++)
            {
                if (items[i].CompareTo(min) < 0)
                {
                    min = items[i];
                }
            }
            return min;
        }

        public static bool ContainsAll<T>(this IList<T> items, params T[] values) where T : IComparable
        {
            foreach(T value in values)
            {
                if (!items.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        #region UInt16 & Int16
        public static byte GetHigh(ushort value)
        {
            return (byte)(value >> 8);
        }
        public static ushort SetHigh(ushort original, byte value)
        {
            return (ushort)((original & 0x00ff) | ((ushort)value << 8));
        }
        public static byte GetLow(ushort value)
        {
            return (byte)value;
        }
        public static ushort SetLow(ushort original, byte value)
        {
            return (ushort)((original & 0xff00) | (ushort)value);
        }
        public static ushort GetUInt16(byte high, byte low)
        {
            return (ushort)((high << 8) | low);
        }

        public static sbyte GetHigh(short value)
        {
            return (sbyte)GetHigh((ushort)value);
        }
        public static short SetHigh(short original, sbyte value)
        {
            return (short)SetHigh((ushort)original, (byte)value);
        }
        public static sbyte GetLow(short value)
        {
            return (sbyte)GetLow((ushort)value);
        }
        public static short SetLow(short original, sbyte value)
        {
            return (short)SetLow((ushort)original, (byte)value);
        }
        public static short GetInt16(sbyte high, sbyte low)
        {
            return (short)GetUInt16((byte)high, (byte)low);
        }
        #endregion UInt16 & Int16

        #region UInt32 & Int32
        public static ushort GetHigh(uint value)
        {
            return (ushort)(value >> 16);
        }
        public static uint SetHigh(uint original, ushort value)
        {
            return (uint)(original & 0x0000ffff) | ((uint)value << 16);
        }
        public static ushort GetLow(uint value)
        {
            return (ushort)value;
        }
        public static uint SetLow(uint original, ushort value)
        {
            return (uint)(original & 0xffff0000) | (uint)value;
        }
        public static uint GetUInt32(ushort high, ushort low)
        {
            return (uint)(((uint)high << 16) | (uint)low);
        }

        public static short GetHigh(int value)
        {
            return (short)GetHigh((uint)value);
        }
        public static int SetHigh(int original, short value)
        {
            return (int)SetHigh((uint)original, (ushort)value);
        }
        public static short GetLow(int value)
        {
            return (short)GetLow((uint)value);
        }
        public static int SetLow(int original, short value)
        {
            return (int)SetLow((uint)original, (ushort)value);
        }
        public static int GetInt32(short high, short low)
        {
            return (int)GetUInt32((ushort)high, (ushort)low);
        }
        #endregion UInt32 & Int32

        #region UInt64 & Int64
        public static uint GetHigh(ulong value)
        {
            return (uint)(value >> 32);
        }
        public static ulong SetHigh(ulong original, uint value)
        {
            return (ulong)((original & 0x00000000ffffffff) | ((ulong)value << 32));
        }
        public static uint GetLow(ulong value)
        {
            return (uint)value;
        }
        public static ulong SetLow(ulong original, uint value)
        {
            return (ulong)((original & 0xffffffff00000000) | (ulong)value);
        }
        public static ulong GetUInt64(uint high, uint low)
        {
            return ((ulong)high << 32) | (ulong)low;
        }

        public static int GetHigh(long value)
        {
            return (int)GetHigh((ulong)value);
        }
        public static long SetHigh(long original, int value)
        {
            return (int)SetHigh((ulong)original, (uint)value);
        }
        public static int GetLow(long value)
        {
            return (int)GetLow((ulong)value);
        }
        public static long SetLow(long original, int value)
        {
            return (long)SetLow((ulong)original, (uint)value);
        }
        public static long GetInt64(int high, int low)
        {
            return (long)GetUInt64((uint)high, (uint)low);
        }
        #endregion UInt64 & Int64

        #region Bit
        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(Byte value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Byte) * UnitTable.ByteToBit)), nameof(index));

            Byte mask = (Byte)((Byte)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static Byte SetBit(Byte value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Byte) * UnitTable.ByteToBit)), nameof(index));

            Byte mask = (Byte)((Byte)1 << index);
            if (flag)
            {
                return (Byte)(value | mask);
            }
            else
            {
                return (Byte)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(SByte value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(SByte) * UnitTable.ByteToBit)), nameof(index));

            SByte mask = (SByte)((SByte)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static SByte SetBit(SByte value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(SByte) * UnitTable.ByteToBit)), nameof(index));

            SByte mask = (SByte)((SByte)1 << index);
            if (flag)
            {
                return (SByte)(value | mask);
            }
            else
            {
                return (SByte)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(Int16 value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Int16) * UnitTable.ByteToBit)), nameof(index));

            Int16 mask = (Int16)((Int16)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static Int16 SetBit(Int16 value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Int16) * UnitTable.ByteToBit)), nameof(index));

            Int16 mask = (Int16)((Int16)1 << index);
            if (flag)
            {
                return (Int16)(value | mask);
            }
            else
            {
                return (Int16)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(UInt16 value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(UInt16) * UnitTable.ByteToBit)), nameof(index));

            UInt16 mask = (UInt16)((UInt16)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static UInt16 SetBit(UInt16 value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(UInt16) * UnitTable.ByteToBit)), nameof(index));

            UInt16 mask = (UInt16)((UInt16)1 << index);
            if (flag)
            {
                return (UInt16)(value | mask);
            }
            else
            {
                return (UInt16)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(Int32 value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Int32) * UnitTable.ByteToBit)), nameof(index));

            Int32 mask = (Int32)((Int32)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static Int32 SetBit(Int32 value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Int32) * UnitTable.ByteToBit)), nameof(index));

            Int32 mask = (Int32)((Int32)1 << index);
            if (flag)
            {
                return (Int32)(value | mask);
            }
            else
            {
                return (Int32)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(UInt32 value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(UInt32) * UnitTable.ByteToBit)), nameof(index));

            UInt32 mask = (UInt32)((UInt32)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static UInt32 SetBit(UInt32 value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(UInt32) * UnitTable.ByteToBit)), nameof(index));

            UInt32 mask = (UInt32)((UInt32)1 << index);
            if (flag)
            {
                return (UInt32)(value | mask);
            }
            else
            {
                return (UInt32)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(Int64 value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Int64) * UnitTable.ByteToBit)), nameof(index));

            Int64 mask = (Int64)((Int64)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static Int64 SetBit(Int64 value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(Int64) * UnitTable.ByteToBit)), nameof(index));

            Int64 mask = (Int64)((Int64)1 << index);
            if (flag)
            {
                return (Int64)(value | mask);
            }
            else
            {
                return (Int64)(value & (~mask));
            }
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <returns>True is bit is 1, else false.</returns>
        public static bool GetBit(UInt64 value, int index)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(UInt64) * UnitTable.ByteToBit)), nameof(index));

            UInt64 mask = (UInt64)((UInt64)1 << index);
            return (value & mask) != 0;
        }

        /// <summary>
        /// Set bit mask for original
        /// </summary>
        /// <param name="value">data value</param>
        /// <param name="index">Index from right</param>
        /// <param name="flag">bit value</param>
        /// <returns>New value</returns>
        public static UInt64 SetBit(UInt64 value, int index, bool flag)
        {
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index >= sizeof(UInt64) * UnitTable.ByteToBit)), nameof(index));

            UInt64 mask = (UInt64)((UInt64)1 << index);
            if (flag)
            {
                return (UInt64)(value | mask);
            }
            else
            {
                return (UInt64)(value & (~mask));
            }
        }
        #endregion Bit


        /// <summary>
        /// Test is pow of two
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPowTwo(int value)
        {
            if (value <= 0)
            {
                return false;
            }

            // Not contains sign - 32rd bit.
            for (int i = 0; i < sizeof(int) * Meision.Algorithms.UnitTable.ByteBits - 1; i++)
            {
                if ((1 << i) == value)
                {
                    return true;
                }
            }

            return false;
        }

        private static Random random;
        public static int GetRandomValue(int min, int max)
        {
            if (random == null)
            {
                random = new Random();
            }

            return random.Next(min, max);
        }

        private static readonly string ChineseZero = "��";
        private static readonly string ChineseNegative = "��";
        private static readonly string ChinesePoint = "��";
        private static readonly string[] ChineseNumber = new string[] { "��", "һ", "��", "��", "��", "��", "��", "��", "��", "��" };
        private static readonly string[] ChinesePieces = new string[] { "ǧ", "��", "ʮ", "" };
        private static readonly string[] ChineseSegments = new string[] { "��", "��", "��", "" };
        public static string ToChinese(decimal number)
        {
            const int SegmentCapacity = 10000;

            StringBuilder builder = new StringBuilder();
            if (number < 0)
            {
                builder.Append(ChineseNegative);
            }

            long integerPart = (long)Math.Abs(Math.Truncate(number));
            // >= 1 0000 0000 0000 0000
            if (integerPart >= (long)Math.Pow(SegmentCapacity, ChineseSegments.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (integerPart != 0)
            {
                long[] segments = new long[ChineseSegments.Length];
                for (int i = 0; i < segments.Length; i++)
                {
                    segments[i] = (long)(integerPart % ((long)Math.Pow(SegmentCapacity, segments.Length - i)) / (long)Math.Pow(SegmentCapacity, segments.Length - i - 1));
                }
                for (int i = 0; i < segments.Length; i++)
                {
                    long segment = segments[i];
                    if (segment != 0)
                    {
                        int[] pieces = new int[ChinesePieces.Length];
                        // Stores units value for thousand, hundred, tens and single.
                        for (int j = 0; j < pieces.Length; j++)
                        {
                            pieces[j] = (int)(segment % ((int)Math.Pow(10, pieces.Length - j)) / (int)Math.Pow(10, pieces.Length - j - 1));
                        }
                        // Stores
                        for (int j = 0; j < pieces.Length; j++)
                        {
                            if (pieces[j] != 0)
                            {
                                builder.Append(ChineseNumber[pieces[j]]);
                                builder.Append(ChinesePieces[j]);
                            }
                            else
                            {
                                switch (j)
                                {
                                    case 0:
                                        if ((i > 0)
                                         && (segments[i - 1] != 0))
                                        {
                                            builder.Append(ChineseZero);
                                        }
                                        break;
                                    case 1:
                                        if ((pieces[j - 1] != 0)
                                         && ((pieces[j + 1] != 0) || (pieces[j + 2] != 0)))
                                        {
                                            builder.Append(ChineseZero);
                                        }
                                        break;
                                    case 2:
                                        if ((pieces[j - 1] != 0)
                                         && (pieces[j + 1] != 0))
                                        {
                                            builder.Append(ChineseZero);
                                        }
                                        break;
                                }
                            }
                        }

                        // segment uint
                        builder.Append(ChineseSegments[i]);
                    }
                    else
                    {
                        if (i > 0 && i < ChineseSegments.Length - 1)
                        {
                            if (segments[i - 1] != 0)
                            {
                                builder.Append(ChineseZero);
                            }
                        }
                    }
                }
            }
            else
            {
                builder.Append(ChineseZero);
            }

            decimal decimalPart = Math.Abs(number) - (long)Math.Abs(Math.Truncate(number));
            if (decimalPart != 0)
            {
                builder.Append(ChinesePoint);
                while (true)
                {
                    decimalPart = decimalPart * 10;
                    if (decimalPart == 0)
                    {
                        break;
                    }

                    int single = (int)Math.Truncate(decimalPart);
                    builder.Append(ChineseNumber[single]);
                    decimalPart = decimalPart - single;
                }
            }

            return builder.ToString();
        }
    }
}
