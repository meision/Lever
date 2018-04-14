using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Meision.Algorithms;
using Meision.Resources;

namespace Meision.Collections
{
    public static class Bytes
    {
        private static readonly byte[] empty = new byte[0];
        public static byte[] Empty
        {
            get
            {
                return empty;
            }
        }

        public static int Compare(byte[] byte1, byte[] byte2)
        {
            if ((byte1 == null) && (byte2 == null))
            {
                return 0;
            }
            else if (byte1 == null)
            {
                return -1;
            }
            else if (byte2 == null)
            {
                return 1;
            }
            else
            {
                if (byte1.Length != byte1.Length)
                {
                    return byte1.Length.CompareTo(byte2.Length);
                }

                for (int i = 0; i < byte1.Length; i++)
                {
                    if (byte1[i] < byte2[i])
                    {
                        return -1;
                    }
                    else if (byte1[i] > byte2[i])
                    {
                        return 1;
                    }
                }

                return 0;
            }
        }

        public static byte[] CreateRandomBytes(int size)
        {
            ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

            Random random = new Random();

            byte[] data = new byte[size];
            random.NextBytes(data);
            return data;
        }

        public static void Copy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
            System.Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }

        public static byte[] Clone(byte[] src)
        {
            if (src == null)
            {
                return null;
            }

            byte[] dst = new byte[src.Length];
            Copy(src, 0, dst, 0, src.Length);
            return dst;
        }

        public static byte[] GetSegment(this byte[] array, int offset)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange(((offset < 0) || (offset > array.Length)), nameof(offset));

            return Bytes.GetSegment(array, offset, array.Length - offset);
        }

        public static byte[] GetSegment(this byte[] array, int offset, int length)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((offset + length) > array.Length));

            byte[] buffer = new byte[length];
            System.Buffer.BlockCopy(array, offset, buffer, 0, length);
            return buffer;
        }

        /// <summary>
        /// Get string till '\0'
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string GetString(this byte[] array)
        {
            return GetString(array, 0);
        }
        public static string GetString(this byte[] array, int offset)
        {
            return GetString(array, offset, Encoding.Default);
        }
        public static string GetString(this byte[] array, int offset, Encoding encoding)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentMustNotNegative((offset < 0), nameof(offset));

            int end = Array.IndexOf<byte>(array, 0, offset);
            if (end < 0)
            {
                end = array.Length;
            }
            string text = encoding.GetString(array, offset, end - offset);
            return text;
        }

        public static byte[] TrimStart(this byte[] array)
        {
            return TrimStart(array, 0);
        }
        public static byte[] TrimStart(this byte[] array, params byte[] trimBytes)
        {
            return Trim(array, trimBytes, null);
        }
        public static byte[] TrimEnd(this byte[] array)
        {
            return TrimEnd(array, 0);
        }
        public static byte[] TrimEnd(this byte[] array, params byte[] trimBytes)
        {
            return Trim(array, null, trimBytes);
        }
        public static byte[] Trim(this byte[] array)
        {
            return Trim(array, 0);
        }
        public static byte[] Trim(this byte[] array, params byte[] trimBytes)
        {
            return Trim(array, trimBytes, trimBytes);
        }
        public static byte[] Trim(this byte[] array, byte[] leftTrimBytes, byte[] rightTrimBytes)
        {
            int end = array.Length - 1;
            int start = 0;
            if (leftTrimBytes != null)
            {
                while (start < array.Length)
                {
                    if (leftTrimBytes.GetIndex(p => p == array[start]) < 0)
                    {
                        break;
                    }
                    start++;
                }
            }
            if (rightTrimBytes != null)
            {
                while (end >= start)
                {
                    if (rightTrimBytes.GetIndex(p => p == array[end]) < 0)
                    {
                        break;
                    }
                    end--;
                }
            }

            int length = (end - start) + 1;
            if (length == array.Length)
            {
                return array;
            }
            if (length == 0)
            {
                return Bytes.Empty;
            }
            return CollectionManager.GetSegment(array, start, length);

        }

        public static byte[] Align(this byte[] array, int alignment)
        {
            byte[] buffer = new byte[Meision.Algorithms.Calculator.CeilingDivisionTotal(array.Length, alignment)];
            Bytes.Copy(array, 0, buffer, 0, array.Length);
            return buffer;
        }

        /// <summary>
        /// concatenate zero one more bytes array to one
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>new bytes</returns>
        public static byte[] Concatenate(params byte[][] parameters)
        {
            if (parameters.Length == 0)
            {
                return Bytes.Empty;
            }

            int size = 0;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    continue;
                }

                size += parameters[i].Length;
            }

            byte[] buffer = new byte[size];
            int total = 0;
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].CopyTo(buffer, total);
                total += parameters[i].Length;
            }

            return buffer;
        }

        public static byte[][] Split(this byte[] array, byte separator = 0)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            if (array.Length == 0)
            {
                return new byte[0][];
            }

            List<byte[]> items = new List<byte[]>();
            for (int beginIndex = 0, endIndex = 0; endIndex <= array.Length; endIndex++)
            {
                if ((endIndex == array.Length)
                 || (array[endIndex] == separator))
                {
                    items.Add(array.GetSegment(beginIndex, endIndex - beginIndex));
                    beginIndex = endIndex + 1;
                }
            }
            return items.ToArray();
        }

        #region Search
        /// <summary>
        /// Find the index of pattern in origin array. BMH algorithm search
        /// </summary>
        /// <param name="origin">origin bytes.</param>
        /// <param name="pattern">Find bytes.</param>
        /// <returns>Index position.</returns>
        public static int Search(byte[] origin, byte[] pattern)
        {
            return Search(origin, 0, origin.Length, pattern);
        }

        /// <summary>
        /// Find the index of pattern in origin array. BMH algorithm search
        /// </summary>
        /// <param name="origin">origin bytes.</param> 
        /// <param name="index">index.</param>
        /// <param name="length">searching length</param>
        /// <param name="pattern">Find bytes.</param>
        /// <returns>Index position.</returns>
        public static int Search(byte[] origin, int index, byte[] pattern)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((origin == null), nameof(origin));
            ThrowHelper.ArgumentIndexOutOfRange(((index < 0) || (index > origin.Length)), nameof(index));
            ThrowHelper.ArgumentNull((pattern == null), nameof(pattern));

            return Bytes.Search(origin, index, origin.Length - index, pattern);
        }



        /// <summary>
        /// Find the index of pattern in origin array. BMH algorithm search
        /// </summary>
        /// <param name="origin">origin bytes.</param> 
        /// <param name="index">start index.</param>
        /// <param name="length">searching length</param>
        /// <param name="pattern">Find bytes.</param>
        /// <returns>Index position.</returns>
        public static int Search(byte[] origin, int index, int length, byte[] pattern)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((origin == null), nameof(origin));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + length) > origin.Length));
            ThrowHelper.ArgumentNull((pattern == null), nameof(pattern));

            if ((pattern.Length == 0)
             || (length < pattern.Length))
            {
                return -1;
            }

            if (pattern.Length > 1)
            {
                #region Binary Search
                unsafe
                {
                    fixed (byte* pOrigin = origin)
                    fixed (byte* pPattern = pattern)
                    {
                        byte* result = BMHSearch(pOrigin + index, length, pPattern, pattern.Length);
                        if (result == null)
                        {
                            return -1;
                        }
                        return (int)(result - pOrigin);
                    }
                }
                #endregion Binary Search
            }
            else
            {
                #region Normal Search
                byte b = pattern[0];
                for (int i = index; i < index + length; i++)
                {
                    if (origin[i] == b)
                    {
                        return i;
                    }
                }
                return -1;
                #endregion Normal Search
            }
        }

        public static unsafe byte* BMHSearch(byte* haystack, int hlen, byte* needle, int nlen)
        {
            int[] bad_char_skip = new int[byte.MaxValue + 1];

            // Initialize the table to default value
            // When a character is encountered that does not occur
            // in the needle, we can safely skip ahead for the whole
            // length of the needle.
            for (int scan = 0; scan < bad_char_skip.Length; scan++)
            {
                bad_char_skip[scan] = nlen;
            }

            // C arrays have the first byte at [0], therefore:
            // [nlen - 1] is the last byte of the array.
            int last = nlen - 1;
            // Then populate it with the analysis of the needle
            for (int scan = 0; scan < last; scan++)
            {
                bad_char_skip[needle[scan]] = last - scan;
            }

            // Search the haystack, while the needle can still be within it.
            while (hlen >= nlen)
            {
                // scan from the end of the needle
                for (int scan = last; haystack[scan] == needle[scan]; scan--)
                {
                    // If the first byte matches, we've found it. 
                    if (scan == 0)
                    {
                        return haystack;
                    }
                }

                // otherwise, we need to skip some bytes and start again.
                // Note that here we are getting the skip value based on the last byte
                // of needle, no matter where we didn't match. So if needle is: "abcd"
                // then we are skipping based on 'd' and that value will be 4, and
                // for "abcdd" we again skip on 'd' but the value will be only 1.
                // The alternative of pretending that the mismatched character was
                // the last character is slower in the normal case (E.g. finding
                // "abcd" in "...azcd..." gives 4 by using 'd' but only
                // 4-2==2 using 'z'.
                hlen -= bad_char_skip[haystack[last]];
                haystack += bad_char_skip[haystack[last]];
            }

            return null;
        }

        /// <summary>
        /// Returns a new byte array in which all occurrences of a specified byte array 
        /// in this instance are replaced with another specified byte array.
        /// </summary>
        /// <param name="origin">The origin byte array.</param>
        /// <param name="pattern">The byte array to be replaced.</param>
        /// <param name="replacement">The byte array to replace all occurrences of pattern.</param>
        /// <returns></returns>
        public static byte[] Replace(this byte[] origin, byte[] pattern, byte[] replacement)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((origin == null), nameof(origin));
            ThrowHelper.ArgumentNull((pattern == null), nameof(pattern));
            ThrowHelper.ArgumentNull((replacement == null), nameof(replacement));


            List<int> indices = new List<int>();
            int index = 0;
            while (true)
            {
                index = Meision.Collections.Bytes.Search(origin, index, pattern);
                if (index < 0)
                {
                    break;
                }

                indices.Add(index);
                index += pattern.Length;
            }

            byte[] buffer = new byte[origin.Length + (replacement.Length - pattern.Length) * indices.Count];
            int position = 0;
            int length = (indices.Count > 0) ? indices[0] - position : replacement.Length;
            Bytes.Copy(origin, position, buffer, position, length);
            position += length;
            for (int i = 0; i < indices.Count; i++)
            {
                length = replacement.Length;
                Bytes.Copy(replacement, 0, buffer, position, length);
                position += length;

                length = ((i < indices.Count - 1) ? indices[i + 1] : origin.Length) - indices[i] - pattern.Length;
                Bytes.Copy(origin, indices[i] + pattern.Length, buffer, position, length);
                position += length;
            }

            return buffer;
        }

        #endregion Search

        public static int GetHashCode(byte[] array)
        {
            return GetHashCode(array, 0, array.Length);
        }

        public static unsafe int GetHashCode(byte[] array, int offset, int length)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((offset + length) > array.Length));

            fixed (byte* pArray = &array[offset])
            {
                int hash = 0;
                int times = length / sizeof(int);
                int mod = length % sizeof(int);

                int* pointer = (int*)pArray;

                // Run times
                for (int i = 0; i < times; i++)
                {
                    hash ^= *pointer;
                    pointer++;
                }
                // compute last bytes.
                switch (mod)
                {
                    case 1:
                        hash ^= (array[offset + length - 1]);
                        break;
                    case 2:
                        hash ^= ((array[offset + length - 2]) | (array[offset + length - 1] << 8));
                        break;
                    case 3:
                        hash ^= ((array[offset + length - 3]) | (array[offset + length - 2] << 8) | (array[offset + length - 1] << 16));
                        break;
                    default:
                        break;
                }

                return hash;
            }
        }

        public static void Reverse(byte[] data)
        {
            ThrowHelper.ArgumentNull((data == null), nameof(data));

            Reverse(data, 0, data.Length);
        }

        public static void Reverse(byte[] data, int index, int count)
        {
            ThrowHelper.ArgumentNull((data == null), nameof(data));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + count) > data.Length));

            int middle = index + count / 2;
            int upper = index + count - 1;
            for (int i = index; i < middle; i++)
            {
                // Locate destination.
                int j = upper - (i - index);
                // Swap i, j
                byte temp = data[i];
                data[i] = data[j];
                data[j] = temp;
            }
        }

        public static byte[] ConvertFromStringArray(string[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            if (items.Length == 0)
            {
                return Empty;
            }

            // Calculate size of string.
            int count = 0;
            foreach (string item in items)
            {
                count += (item.Length * sizeof(char));
            }
            count += (items.Length) * sizeof(char);
            byte[] buffer = new byte[count];
            // Copy string array.
            int index = 0;
            foreach (string item in items)
            {
                unsafe
                {
                    fixed (char* pointer = item)
                    {
                        Marshal.Copy(new IntPtr(pointer), buffer, index, item.Length * sizeof(char));
                    }
                }
                index += (item.Length + 1) * sizeof(char);
            }

            return buffer;
        }

        public static string[] ConvertToStringArray(byte[] buffer)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentException((((buffer.Length % 2) != 0) || (buffer[buffer.Length - 2] != 0) || (buffer[buffer.Length - 1] != 0)), SR.Bytes_ConvertToStringArray_Exception_InvalidByteArray);

            List<string> items = new List<string>();
            unsafe
            {
                fixed (byte* pByte = buffer)
                {
                    for (int index = 0; index < buffer.Length;)
                    {
                        string item = new string((char*)(pByte + index));
                        index += ((item.Length + 1) * sizeof(char));
                        items.Add(item);
                    }
                }
            }
            return items.ToArray();
        }

        public static string ToHexString(this byte[] buffer, string delimiter = null)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            if (buffer.Length == 0)
            {
                return string.Empty;
            }

            int length = buffer.Length * 2;
            if (delimiter != null)
            {
                length += ((buffer.Length - 1) * delimiter.Length);
            }

            StringBuilder builder = new StringBuilder(length, length);
            builder.AppendFormat(buffer[0].ToString("X2"));
            for (int i = 1; i < buffer.Length; i++)
            {
                builder.Append(delimiter);
                builder.AppendFormat(buffer[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static byte[] FromHexString(string text, bool ignoreSpace = false)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));
            string value = ignoreSpace ? Regex.Replace(text, @"\s", "") : text;
            ThrowHelper.ArgumentException(((value.Length % 2) != 0), SR.Bytes_Exception_InvalidHexText, nameof(value));

            byte[] buffer = new byte[value.Length / 2];
            for (int i = 0; i < value.Length; i += 2)
            {
                byte b = Calculator.GetHexValue(value[i], value[i + 1]);

                buffer[i / 2] = b;
            }
            return buffer;
        }

        /// <summary>
        /// Generate code expression.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="countPerLine"></param>
        /// <param name="capitalization"></param>
        /// <returns></returns>
        public static string ToExpression(this byte[] buffer, int countPerLine = 16, bool capitalization = true)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentOutOfRange((countPerLine < 1), nameof(countPerLine));

            string format = capitalization ? "0x{0:X2}, " : "0x{0:x2}, ";

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.AppendFormat(format, buffer[i]);
                if (i % countPerLine == (countPerLine - 1))
                {
                    builder.Append("\r\n");
                }
            }
            return builder.ToString();
        }
    }
}
