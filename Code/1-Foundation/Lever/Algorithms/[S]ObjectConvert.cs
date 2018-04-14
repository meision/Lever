using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Meision.Algorithms
{
    public static class ObjectConvert
    {
        //#region Size
        //private const string NumberKey = "Number";
        //private const string UnitKey = "Unit";
        //public static readonly string SizeRegularExpression = string.Format(
        //    System.Globalization.CultureInfo.InvariantCulture,
        //    @"^(?<{0}>\d+[\,\d\.]*)\ ?(?<{1}>B|KB|MB|GB|TB)$",
        //    NumberKey,
        //    UnitKey);

        //public static string GetSizeText(long size)
        //{
        //    return GetSizeText(size, 2);
        //}

        //public static string GetSizeText(long size, int digits)
        //{
        //    if (size < 0)
        //    {
        //        return Meision.Text.StringConstant.Infinite.ToString();
        //    }
        //    else if ((size >= 0) && (size < 1024))
        //    {
        //        return string.Format(CultureInfo.InvariantCulture, "{0}B", size);
        //    }
        //    else if ((size >= 1024) && (size < 1048576)) // KB
        //    {
        //        double value = (double)size / 1024;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f" + digits + "}KB", value);
        //    }
        //    else if ((size >= 1048576) && (size < 1073741824)) // MB
        //    {
        //        double value = (double)size / 1048576;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f" + digits + "}MB", value);
        //    }
        //    else if ((size >= 1073741824) && (size < 1099511627776))// GB
        //    {
        //        double value = (double)size / 1073741824;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f" + digits + "}GB", value);
        //    }
        //    else // TB
        //    {
        //        double value = (double)size / 1099511627776;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f" + digits + "}TB", value);
        //    }
        //}

        ///// <summary>
        ///// Get size from size text.
        ///// </summary>
        ///// <param name="value">The size text</param>
        ///// <returns>Size</returns>
        //public static long ToSize(string value)
        //{
        //    if (value == null)
        //    {
        //        throw new ArgumentNullException(nameof(value));
        //    }

        //    Match match = Regex.Match(value, RegexConstant.GetFullRegularExpression(SizeRegularExpression));
        //    if (!match.Success)
        //    {
        //        throw new ArgumentException(SR.Convert_SizeText_Invalid, nameof(value));
        //    }

        //    double number = double.Parse(match.Groups[NumberKey].Value);
        //    long unit = 0;
        //    switch (match.Groups[UnitKey].Value.ToUpperInvariant())
        //    {
        //        case "B":
        //            unit = 1;
        //            break;
        //        case "KB":
        //            unit = 1024;
        //            break;
        //        case "MB":
        //            unit = 1048576;
        //            break;
        //        case "GB":
        //            unit = 1073741824;
        //            break;
        //        case "TB":
        //            unit = 1099511627776;
        //            break;
        //        default:
        //            break;
        //    }

        //    number *= unit;
        //    return (long)number;
        //}
        //#endregion Size

        //#region Speed
        //public static string GetSpeedText(double speed)
        //{
        //    if (speed < 0)
        //    {
        //        throw new ArgumentOutOfRangeException(SR.Convert_Speed_Negative, "speed");
        //    }

        //    if ((speed >= 0) && (speed < 1024))
        //    {
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f1}B/s", speed);
        //    }
        //    else if ((speed >= 1024) && (speed < 1048576)) // KB
        //    {
        //        double value = (double)speed / 1024;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f1}KB/s", value);
        //    }
        //    else if ((speed >= 1048576) && (speed < 1073741824)) // MB
        //    {
        //        double value = (double)speed / 1048576;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f1}MB/s", value);
        //    }
        //    else if ((speed >= 1073741824) && (speed < 1099511627776))// GB
        //    {
        //        double value = (double)speed / 1073741824;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f1}GB/s", value);
        //    }
        //    else // TB
        //    {
        //        double value = (double)speed / 1099511627776;
        //        return string.Format(CultureInfo.InvariantCulture, "{0:f1}TB/s", value);
        //    }
        //}
        //#endregion Speed

        public static T DBNullToDefault<T>(object value)
        {
            if (System.Convert.IsDBNull(value))
            {
                return default(T);
            }

            return (T)value;
        }

        public static object DBNullToNull(object value)
        {
            if (System.Convert.IsDBNull(value))
            {
                return null;
            }

            return value;
        }

        public static string ToString(byte[] bytes)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));

            return ToString(bytes, 0, bytes.Length, false);
        }

        public static string ToString(byte[] bytes, int index, int length)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));

            return ToString(bytes, index, length, false);
        }

        public static string ToString(byte[] bytes, int index, int length, bool wrap)
        {
            ThrowHelper.ArgumentNull((bytes == null), nameof(bytes));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentLengthOutOfRange((length < 0), nameof(length));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + length) > bytes.Length));

            string[] buffer = new string[length];
            for (int i = index; i < index + length; i++)
            {
                buffer[i - index] = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    "{0:X2}",
                    bytes[i]);
            }

            if (wrap)
            {
                string[][] temp = Meision.Collections.CollectionManager.Divide<string>(buffer, 16);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < temp.Length; i++)
                {
                    builder.AppendLine(string.Join(" ", temp[i]));
                }
                return builder.ToString();
            }
            else
            {
                return string.Join(" ", buffer);
            }
        }

        public static byte[] ToBytes<T>(T value) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            ToBytes(value, buffer);
            return buffer;
        }

        //public static void ToBytes<T>(T value, byte[] buffer, int index = 0) where T : struct
        //{
        //    ToBytes((object)value, buffer, index);
        //}

        public static void ToBytes(object value, byte[] buffer, int index = 0)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            int size = (!value.GetType().IsEnum) ? Marshal.SizeOf(value) : Marshal.SizeOf(Enum.GetUnderlyingType(value.GetType()));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + size) > buffer.Length));

            ObjectConvert.ToBytes(value, size, buffer, index);
        }

        internal static void ToBytes(object value, int size, byte[] buffer, int index = 0)
        {
            IntPtr pointer = Marshal.AllocHGlobal(size);
            try
            {
                if (value.GetType().IsEnum)
                {
                    Marshal.StructureToPtr(System.Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType())), pointer, false);
                    Marshal.Copy(pointer, buffer, index, size);
                }
                else
                {
                    Marshal.StructureToPtr(value, pointer, false);
                    Marshal.Copy(pointer, buffer, index, size);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }


        public static T FromBytes<T>(byte[] buffer, int index = 0) where T : struct
        {
            return (T)FromBytes(typeof(T), buffer, index);
        }

        public static object FromBytes(Type type, byte[] buffer, int index = 0)
        {
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            int size = (!type.IsEnum) ? Marshal.SizeOf(type) : Marshal.SizeOf(Enum.GetUnderlyingType(type));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + size) > buffer.Length));

            return FromBytes(type, size, buffer, index);
        }

        internal static object FromBytes(Type type, int size, byte[] buffer, int index = 0)
        {
            IntPtr pointer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(buffer, index, pointer, size);
                if (type.IsEnum)
                {
                    object value = Marshal.PtrToStructure(pointer, Enum.GetUnderlyingType(type));
                    return value;
                }
                else
                {
                    object value = Marshal.PtrToStructure(pointer, type);
                    return value;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }


        public static void FromBytes(object value, byte[] buffer, int index = 0)
        {
            ThrowHelper.ArgumentNull((value == null), nameof(value));
            ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
            int size = Marshal.SizeOf(value);
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + size) > buffer.Length));

            IntPtr pointer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(buffer, index, pointer, size);
                Marshal.PtrToStructure(pointer, value);
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

    }
}
