using System;
using Meision.Resources;

namespace Meision.Algorithms
{
    public static class Calculator
    {
        public static byte GetHexValue(char high, char low)
        {
            byte b;
            switch (high)
            {
                case '0':
                    switch (low)
                    {
                        case '0': b = 0x00; break;
                        case '1': b = 0x01; break;
                        case '2': b = 0x02; break;
                        case '3': b = 0x03; break;
                        case '4': b = 0x04; break;
                        case '5': b = 0x05; break;
                        case '6': b = 0x06; break;
                        case '7': b = 0x07; break;
                        case '8': b = 0x08; break;
                        case '9': b = 0x09; break;
                        case 'a': case 'A': b = 0x0A; break;
                        case 'b': case 'B': b = 0x0B; break;
                        case 'c': case 'C': b = 0x0C; break;
                        case 'd': case 'D': b = 0x0D; break;
                        case 'e': case 'E': b = 0x0E; break;
                        case 'f': case 'F': b = 0x0F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '1':
                    switch (low)
                    {
                        case '0': b = 0x10; break;
                        case '1': b = 0x11; break;
                        case '2': b = 0x12; break;
                        case '3': b = 0x13; break;
                        case '4': b = 0x14; break;
                        case '5': b = 0x15; break;
                        case '6': b = 0x16; break;
                        case '7': b = 0x17; break;
                        case '8': b = 0x18; break;
                        case '9': b = 0x19; break;
                        case 'a': case 'A': b = 0x1A; break;
                        case 'b': case 'B': b = 0x1B; break;
                        case 'c': case 'C': b = 0x1C; break;
                        case 'd': case 'D': b = 0x1D; break;
                        case 'e': case 'E': b = 0x1E; break;
                        case 'f': case 'F': b = 0x1F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '2':
                    switch (low)
                    {
                        case '0': b = 0x20; break;
                        case '1': b = 0x21; break;
                        case '2': b = 0x22; break;
                        case '3': b = 0x23; break;
                        case '4': b = 0x24; break;
                        case '5': b = 0x25; break;
                        case '6': b = 0x26; break;
                        case '7': b = 0x27; break;
                        case '8': b = 0x28; break;
                        case '9': b = 0x29; break;
                        case 'a': case 'A': b = 0x2A; break;
                        case 'b': case 'B': b = 0x2B; break;
                        case 'c': case 'C': b = 0x2C; break;
                        case 'd': case 'D': b = 0x2D; break;
                        case 'e': case 'E': b = 0x2E; break;
                        case 'f': case 'F': b = 0x2F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '3':
                    switch (low)
                    {
                        case '0': b = 0x30; break;
                        case '1': b = 0x31; break;
                        case '2': b = 0x32; break;
                        case '3': b = 0x33; break;
                        case '4': b = 0x34; break;
                        case '5': b = 0x35; break;
                        case '6': b = 0x36; break;
                        case '7': b = 0x37; break;
                        case '8': b = 0x38; break;
                        case '9': b = 0x39; break;
                        case 'a': case 'A': b = 0x3A; break;
                        case 'b': case 'B': b = 0x3B; break;
                        case 'c': case 'C': b = 0x3C; break;
                        case 'd': case 'D': b = 0x3D; break;
                        case 'e': case 'E': b = 0x3E; break;
                        case 'f': case 'F': b = 0x3F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '4':
                    switch (low)
                    {
                        case '0': b = 0x40; break;
                        case '1': b = 0x41; break;
                        case '2': b = 0x42; break;
                        case '3': b = 0x43; break;
                        case '4': b = 0x44; break;
                        case '5': b = 0x45; break;
                        case '6': b = 0x46; break;
                        case '7': b = 0x47; break;
                        case '8': b = 0x48; break;
                        case '9': b = 0x49; break;
                        case 'a': case 'A': b = 0x4A; break;
                        case 'b': case 'B': b = 0x4B; break;
                        case 'c': case 'C': b = 0x4C; break;
                        case 'd': case 'D': b = 0x4D; break;
                        case 'e': case 'E': b = 0x4E; break;
                        case 'f': case 'F': b = 0x4F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '5':
                    switch (low)
                    {
                        case '0': b = 0x50; break;
                        case '1': b = 0x51; break;
                        case '2': b = 0x52; break;
                        case '3': b = 0x53; break;
                        case '4': b = 0x54; break;
                        case '5': b = 0x55; break;
                        case '6': b = 0x56; break;
                        case '7': b = 0x57; break;
                        case '8': b = 0x58; break;
                        case '9': b = 0x59; break;
                        case 'a': case 'A': b = 0x5A; break;
                        case 'b': case 'B': b = 0x5B; break;
                        case 'c': case 'C': b = 0x5C; break;
                        case 'd': case 'D': b = 0x5D; break;
                        case 'e': case 'E': b = 0x5E; break;
                        case 'f': case 'F': b = 0x5F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '6':
                    switch (low)
                    {
                        case '0': b = 0x60; break;
                        case '1': b = 0x61; break;
                        case '2': b = 0x62; break;
                        case '3': b = 0x63; break;
                        case '4': b = 0x64; break;
                        case '5': b = 0x65; break;
                        case '6': b = 0x66; break;
                        case '7': b = 0x67; break;
                        case '8': b = 0x68; break;
                        case '9': b = 0x69; break;
                        case 'a': case 'A': b = 0x6A; break;
                        case 'b': case 'B': b = 0x6B; break;
                        case 'c': case 'C': b = 0x6C; break;
                        case 'd': case 'D': b = 0x6D; break;
                        case 'e': case 'E': b = 0x6E; break;
                        case 'f': case 'F': b = 0x6F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '7':
                    switch (low)
                    {
                        case '0': b = 0x70; break;
                        case '1': b = 0x71; break;
                        case '2': b = 0x72; break;
                        case '3': b = 0x73; break;
                        case '4': b = 0x74; break;
                        case '5': b = 0x75; break;
                        case '6': b = 0x76; break;
                        case '7': b = 0x77; break;
                        case '8': b = 0x78; break;
                        case '9': b = 0x79; break;
                        case 'a': case 'A': b = 0x7A; break;
                        case 'b': case 'B': b = 0x7B; break;
                        case 'c': case 'C': b = 0x7C; break;
                        case 'd': case 'D': b = 0x7D; break;
                        case 'e': case 'E': b = 0x7E; break;
                        case 'f': case 'F': b = 0x7F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '8':
                    switch (low)
                    {
                        case '0': b = 0x80; break;
                        case '1': b = 0x81; break;
                        case '2': b = 0x82; break;
                        case '3': b = 0x83; break;
                        case '4': b = 0x84; break;
                        case '5': b = 0x85; break;
                        case '6': b = 0x86; break;
                        case '7': b = 0x87; break;
                        case '8': b = 0x88; break;
                        case '9': b = 0x89; break;
                        case 'a': case 'A': b = 0x8A; break;
                        case 'b': case 'B': b = 0x8B; break;
                        case 'c': case 'C': b = 0x8C; break;
                        case 'd': case 'D': b = 0x8D; break;
                        case 'e': case 'E': b = 0x8E; break;
                        case 'f': case 'F': b = 0x8F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case '9':
                    switch (low)
                    {
                        case '0': b = 0x90; break;
                        case '1': b = 0x91; break;
                        case '2': b = 0x92; break;
                        case '3': b = 0x93; break;
                        case '4': b = 0x94; break;
                        case '5': b = 0x95; break;
                        case '6': b = 0x96; break;
                        case '7': b = 0x97; break;
                        case '8': b = 0x98; break;
                        case '9': b = 0x99; break;
                        case 'a': case 'A': b = 0x9A; break;
                        case 'b': case 'B': b = 0x9B; break;
                        case 'c': case 'C': b = 0x9C; break;
                        case 'd': case 'D': b = 0x9D; break;
                        case 'e': case 'E': b = 0x9E; break;
                        case 'f': case 'F': b = 0x9F; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case 'a':
                case 'A':
                    switch (low)
                    {
                        case '0': b = 0xA0; break;
                        case '1': b = 0xA1; break;
                        case '2': b = 0xA2; break;
                        case '3': b = 0xA3; break;
                        case '4': b = 0xA4; break;
                        case '5': b = 0xA5; break;
                        case '6': b = 0xA6; break;
                        case '7': b = 0xA7; break;
                        case '8': b = 0xA8; break;
                        case '9': b = 0xA9; break;
                        case 'a': case 'A': b = 0xAA; break;
                        case 'b': case 'B': b = 0xAB; break;
                        case 'c': case 'C': b = 0xAC; break;
                        case 'd': case 'D': b = 0xAD; break;
                        case 'e': case 'E': b = 0xAE; break;
                        case 'f': case 'F': b = 0xAF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case 'b':
                case 'B':
                    switch (low)
                    {
                        case '0': b = 0xB0; break;
                        case '1': b = 0xB1; break;
                        case '2': b = 0xB2; break;
                        case '3': b = 0xB3; break;
                        case '4': b = 0xB4; break;
                        case '5': b = 0xB5; break;
                        case '6': b = 0xB6; break;
                        case '7': b = 0xB7; break;
                        case '8': b = 0xB8; break;
                        case '9': b = 0xB9; break;
                        case 'a': case 'A': b = 0xBA; break;
                        case 'b': case 'B': b = 0xBB; break;
                        case 'c': case 'C': b = 0xBC; break;
                        case 'd': case 'D': b = 0xBD; break;
                        case 'e': case 'E': b = 0xBE; break;
                        case 'f': case 'F': b = 0xBF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case 'c':
                case 'C':
                    switch (low)
                    {
                        case '0': b = 0xC0; break;
                        case '1': b = 0xC1; break;
                        case '2': b = 0xC2; break;
                        case '3': b = 0xC3; break;
                        case '4': b = 0xC4; break;
                        case '5': b = 0xC5; break;
                        case '6': b = 0xC6; break;
                        case '7': b = 0xC7; break;
                        case '8': b = 0xC8; break;
                        case '9': b = 0xC9; break;
                        case 'a': case 'A': b = 0xCA; break;
                        case 'b': case 'B': b = 0xCB; break;
                        case 'c': case 'C': b = 0xCC; break;
                        case 'd': case 'D': b = 0xCD; break;
                        case 'e': case 'E': b = 0xCE; break;
                        case 'f': case 'F': b = 0xCF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case 'd':
                case 'D':
                    switch (low)
                    {
                        case '0': b = 0xD0; break;
                        case '1': b = 0xD1; break;
                        case '2': b = 0xD2; break;
                        case '3': b = 0xD3; break;
                        case '4': b = 0xD4; break;
                        case '5': b = 0xD5; break;
                        case '6': b = 0xD6; break;
                        case '7': b = 0xD7; break;
                        case '8': b = 0xD8; break;
                        case '9': b = 0xD9; break;
                        case 'a': case 'A': b = 0xDA; break;
                        case 'b': case 'B': b = 0xDB; break;
                        case 'c': case 'C': b = 0xDC; break;
                        case 'd': case 'D': b = 0xDD; break;
                        case 'e': case 'E': b = 0xDE; break;
                        case 'f': case 'F': b = 0xDF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case 'e':
                case 'E':
                    switch (low)
                    {
                        case '0': b = 0xE0; break;
                        case '1': b = 0xE1; break;
                        case '2': b = 0xE2; break;
                        case '3': b = 0xE3; break;
                        case '4': b = 0xE4; break;
                        case '5': b = 0xE5; break;
                        case '6': b = 0xE6; break;
                        case '7': b = 0xE7; break;
                        case '8': b = 0xE8; break;
                        case '9': b = 0xE9; break;
                        case 'a': case 'A': b = 0xEA; break;
                        case 'b': case 'B': b = 0xEB; break;
                        case 'c': case 'C': b = 0xEC; break;
                        case 'd': case 'D': b = 0xED; break;
                        case 'e': case 'E': b = 0xEE; break;
                        case 'f': case 'F': b = 0xEF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                case 'f':
                case 'F':
                    switch (low)
                    {
                        case '0': b = 0xF0; break;
                        case '1': b = 0xF1; break;
                        case '2': b = 0xF2; break;
                        case '3': b = 0xF3; break;
                        case '4': b = 0xF4; break;
                        case '5': b = 0xF5; break;
                        case '6': b = 0xF6; break;
                        case '7': b = 0xF7; break;
                        case '8': b = 0xF8; break;
                        case '9': b = 0xF9; break;
                        case 'a': case 'A': b = 0xFA; break;
                        case 'b': case 'B': b = 0xFB; break;
                        case 'c': case 'C': b = 0xFC; break;
                        case 'd': case 'D': b = 0xFD; break;
                        case 'e': case 'E': b = 0xFE; break;
                        case 'f': case 'F': b = 0xFF; break;
                        default: throw new ArgumentOutOfRangeException(nameof(low), SR._Exception_ValueOutOfRange.FormatWith(low));
                    }
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(high), SR._Exception_ValueOutOfRange.FormatWith(high));
            }
            return b;
        }
        public static string ToHexString(byte b, bool capitalization = true)
        {
            char[] items = new char[UnitTable.HexTextCountPerByte];
            ToHexStringInner(b, items, 0, capitalization);
            return new string(items);
        }
        public static void ToHexString(byte b, char[] array, int index, bool capitalization = true)
        {
            ThrowHelper.ArgumentNull((array == null), nameof(array));
            ThrowHelper.ArgumentIndexOutOfRange((index < 0), nameof(index));
            ThrowHelper.ArgumentIndexLengthOutOfArray(((index + UnitTable.HexTextCountPerByte) > array.Length));

            ToHexStringInner(b, array, index, capitalization);
        }
        internal static void ToHexStringInner(byte b, char[] array, int index, bool capitalization = true)
        {
            if (capitalization)
            {
                switch (b)
                {
                    case (byte)0: array[index] = '0'; array[index + 1] = '0'; break;
                    case (byte)1: array[index] = '0'; array[index + 1] = '1'; break;
                    case (byte)2: array[index] = '0'; array[index + 1] = '2'; break;
                    case (byte)3: array[index] = '0'; array[index + 1] = '3'; break;
                    case (byte)4: array[index] = '0'; array[index + 1] = '4'; break;
                    case (byte)5: array[index] = '0'; array[index + 1] = '5'; break;
                    case (byte)6: array[index] = '0'; array[index + 1] = '6'; break;
                    case (byte)7: array[index] = '0'; array[index + 1] = '7'; break;
                    case (byte)8: array[index] = '0'; array[index + 1] = '8'; break;
                    case (byte)9: array[index] = '0'; array[index + 1] = '9'; break;
                    case (byte)10: array[index] = '0'; array[index + 1] = 'A'; break;
                    case (byte)11: array[index] = '0'; array[index + 1] = 'B'; break;
                    case (byte)12: array[index] = '0'; array[index + 1] = 'C'; break;
                    case (byte)13: array[index] = '0'; array[index + 1] = 'D'; break;
                    case (byte)14: array[index] = '0'; array[index + 1] = 'E'; break;
                    case (byte)15: array[index] = '0'; array[index + 1] = 'F'; break;
                    case (byte)16: array[index] = '1'; array[index + 1] = '0'; break;
                    case (byte)17: array[index] = '1'; array[index + 1] = '1'; break;
                    case (byte)18: array[index] = '1'; array[index + 1] = '2'; break;
                    case (byte)19: array[index] = '1'; array[index + 1] = '3'; break;
                    case (byte)20: array[index] = '1'; array[index + 1] = '4'; break;
                    case (byte)21: array[index] = '1'; array[index + 1] = '5'; break;
                    case (byte)22: array[index] = '1'; array[index + 1] = '6'; break;
                    case (byte)23: array[index] = '1'; array[index + 1] = '7'; break;
                    case (byte)24: array[index] = '1'; array[index + 1] = '8'; break;
                    case (byte)25: array[index] = '1'; array[index + 1] = '9'; break;
                    case (byte)26: array[index] = '1'; array[index + 1] = 'A'; break;
                    case (byte)27: array[index] = '1'; array[index + 1] = 'B'; break;
                    case (byte)28: array[index] = '1'; array[index + 1] = 'C'; break;
                    case (byte)29: array[index] = '1'; array[index + 1] = 'D'; break;
                    case (byte)30: array[index] = '1'; array[index + 1] = 'E'; break;
                    case (byte)31: array[index] = '1'; array[index + 1] = 'F'; break;
                    case (byte)32: array[index] = '2'; array[index + 1] = '0'; break;
                    case (byte)33: array[index] = '2'; array[index + 1] = '1'; break;
                    case (byte)34: array[index] = '2'; array[index + 1] = '2'; break;
                    case (byte)35: array[index] = '2'; array[index + 1] = '3'; break;
                    case (byte)36: array[index] = '2'; array[index + 1] = '4'; break;
                    case (byte)37: array[index] = '2'; array[index + 1] = '5'; break;
                    case (byte)38: array[index] = '2'; array[index + 1] = '6'; break;
                    case (byte)39: array[index] = '2'; array[index + 1] = '7'; break;
                    case (byte)40: array[index] = '2'; array[index + 1] = '8'; break;
                    case (byte)41: array[index] = '2'; array[index + 1] = '9'; break;
                    case (byte)42: array[index] = '2'; array[index + 1] = 'A'; break;
                    case (byte)43: array[index] = '2'; array[index + 1] = 'B'; break;
                    case (byte)44: array[index] = '2'; array[index + 1] = 'C'; break;
                    case (byte)45: array[index] = '2'; array[index + 1] = 'D'; break;
                    case (byte)46: array[index] = '2'; array[index + 1] = 'E'; break;
                    case (byte)47: array[index] = '2'; array[index + 1] = 'F'; break;
                    case (byte)48: array[index] = '3'; array[index + 1] = '0'; break;
                    case (byte)49: array[index] = '3'; array[index + 1] = '1'; break;
                    case (byte)50: array[index] = '3'; array[index + 1] = '2'; break;
                    case (byte)51: array[index] = '3'; array[index + 1] = '3'; break;
                    case (byte)52: array[index] = '3'; array[index + 1] = '4'; break;
                    case (byte)53: array[index] = '3'; array[index + 1] = '5'; break;
                    case (byte)54: array[index] = '3'; array[index + 1] = '6'; break;
                    case (byte)55: array[index] = '3'; array[index + 1] = '7'; break;
                    case (byte)56: array[index] = '3'; array[index + 1] = '8'; break;
                    case (byte)57: array[index] = '3'; array[index + 1] = '9'; break;
                    case (byte)58: array[index] = '3'; array[index + 1] = 'A'; break;
                    case (byte)59: array[index] = '3'; array[index + 1] = 'B'; break;
                    case (byte)60: array[index] = '3'; array[index + 1] = 'C'; break;
                    case (byte)61: array[index] = '3'; array[index + 1] = 'D'; break;
                    case (byte)62: array[index] = '3'; array[index + 1] = 'E'; break;
                    case (byte)63: array[index] = '3'; array[index + 1] = 'F'; break;
                    case (byte)64: array[index] = '4'; array[index + 1] = '0'; break;
                    case (byte)65: array[index] = '4'; array[index + 1] = '1'; break;
                    case (byte)66: array[index] = '4'; array[index + 1] = '2'; break;
                    case (byte)67: array[index] = '4'; array[index + 1] = '3'; break;
                    case (byte)68: array[index] = '4'; array[index + 1] = '4'; break;
                    case (byte)69: array[index] = '4'; array[index + 1] = '5'; break;
                    case (byte)70: array[index] = '4'; array[index + 1] = '6'; break;
                    case (byte)71: array[index] = '4'; array[index + 1] = '7'; break;
                    case (byte)72: array[index] = '4'; array[index + 1] = '8'; break;
                    case (byte)73: array[index] = '4'; array[index + 1] = '9'; break;
                    case (byte)74: array[index] = '4'; array[index + 1] = 'A'; break;
                    case (byte)75: array[index] = '4'; array[index + 1] = 'B'; break;
                    case (byte)76: array[index] = '4'; array[index + 1] = 'C'; break;
                    case (byte)77: array[index] = '4'; array[index + 1] = 'D'; break;
                    case (byte)78: array[index] = '4'; array[index + 1] = 'E'; break;
                    case (byte)79: array[index] = '4'; array[index + 1] = 'F'; break;
                    case (byte)80: array[index] = '5'; array[index + 1] = '0'; break;
                    case (byte)81: array[index] = '5'; array[index + 1] = '1'; break;
                    case (byte)82: array[index] = '5'; array[index + 1] = '2'; break;
                    case (byte)83: array[index] = '5'; array[index + 1] = '3'; break;
                    case (byte)84: array[index] = '5'; array[index + 1] = '4'; break;
                    case (byte)85: array[index] = '5'; array[index + 1] = '5'; break;
                    case (byte)86: array[index] = '5'; array[index + 1] = '6'; break;
                    case (byte)87: array[index] = '5'; array[index + 1] = '7'; break;
                    case (byte)88: array[index] = '5'; array[index + 1] = '8'; break;
                    case (byte)89: array[index] = '5'; array[index + 1] = '9'; break;
                    case (byte)90: array[index] = '5'; array[index + 1] = 'A'; break;
                    case (byte)91: array[index] = '5'; array[index + 1] = 'B'; break;
                    case (byte)92: array[index] = '5'; array[index + 1] = 'C'; break;
                    case (byte)93: array[index] = '5'; array[index + 1] = 'D'; break;
                    case (byte)94: array[index] = '5'; array[index + 1] = 'E'; break;
                    case (byte)95: array[index] = '5'; array[index + 1] = 'F'; break;
                    case (byte)96: array[index] = '6'; array[index + 1] = '0'; break;
                    case (byte)97: array[index] = '6'; array[index + 1] = '1'; break;
                    case (byte)98: array[index] = '6'; array[index + 1] = '2'; break;
                    case (byte)99: array[index] = '6'; array[index + 1] = '3'; break;
                    case (byte)100: array[index] = '6'; array[index + 1] = '4'; break;
                    case (byte)101: array[index] = '6'; array[index + 1] = '5'; break;
                    case (byte)102: array[index] = '6'; array[index + 1] = '6'; break;
                    case (byte)103: array[index] = '6'; array[index + 1] = '7'; break;
                    case (byte)104: array[index] = '6'; array[index + 1] = '8'; break;
                    case (byte)105: array[index] = '6'; array[index + 1] = '9'; break;
                    case (byte)106: array[index] = '6'; array[index + 1] = 'A'; break;
                    case (byte)107: array[index] = '6'; array[index + 1] = 'B'; break;
                    case (byte)108: array[index] = '6'; array[index + 1] = 'C'; break;
                    case (byte)109: array[index] = '6'; array[index + 1] = 'D'; break;
                    case (byte)110: array[index] = '6'; array[index + 1] = 'E'; break;
                    case (byte)111: array[index] = '6'; array[index + 1] = 'F'; break;
                    case (byte)112: array[index] = '7'; array[index + 1] = '0'; break;
                    case (byte)113: array[index] = '7'; array[index + 1] = '1'; break;
                    case (byte)114: array[index] = '7'; array[index + 1] = '2'; break;
                    case (byte)115: array[index] = '7'; array[index + 1] = '3'; break;
                    case (byte)116: array[index] = '7'; array[index + 1] = '4'; break;
                    case (byte)117: array[index] = '7'; array[index + 1] = '5'; break;
                    case (byte)118: array[index] = '7'; array[index + 1] = '6'; break;
                    case (byte)119: array[index] = '7'; array[index + 1] = '7'; break;
                    case (byte)120: array[index] = '7'; array[index + 1] = '8'; break;
                    case (byte)121: array[index] = '7'; array[index + 1] = '9'; break;
                    case (byte)122: array[index] = '7'; array[index + 1] = 'A'; break;
                    case (byte)123: array[index] = '7'; array[index + 1] = 'B'; break;
                    case (byte)124: array[index] = '7'; array[index + 1] = 'C'; break;
                    case (byte)125: array[index] = '7'; array[index + 1] = 'D'; break;
                    case (byte)126: array[index] = '7'; array[index + 1] = 'E'; break;
                    case (byte)127: array[index] = '7'; array[index + 1] = 'F'; break;
                    case (byte)128: array[index] = '8'; array[index + 1] = '0'; break;
                    case (byte)129: array[index] = '8'; array[index + 1] = '1'; break;
                    case (byte)130: array[index] = '8'; array[index + 1] = '2'; break;
                    case (byte)131: array[index] = '8'; array[index + 1] = '3'; break;
                    case (byte)132: array[index] = '8'; array[index + 1] = '4'; break;
                    case (byte)133: array[index] = '8'; array[index + 1] = '5'; break;
                    case (byte)134: array[index] = '8'; array[index + 1] = '6'; break;
                    case (byte)135: array[index] = '8'; array[index + 1] = '7'; break;
                    case (byte)136: array[index] = '8'; array[index + 1] = '8'; break;
                    case (byte)137: array[index] = '8'; array[index + 1] = '9'; break;
                    case (byte)138: array[index] = '8'; array[index + 1] = 'A'; break;
                    case (byte)139: array[index] = '8'; array[index + 1] = 'B'; break;
                    case (byte)140: array[index] = '8'; array[index + 1] = 'C'; break;
                    case (byte)141: array[index] = '8'; array[index + 1] = 'D'; break;
                    case (byte)142: array[index] = '8'; array[index + 1] = 'E'; break;
                    case (byte)143: array[index] = '8'; array[index + 1] = 'F'; break;
                    case (byte)144: array[index] = '9'; array[index + 1] = '0'; break;
                    case (byte)145: array[index] = '9'; array[index + 1] = '1'; break;
                    case (byte)146: array[index] = '9'; array[index + 1] = '2'; break;
                    case (byte)147: array[index] = '9'; array[index + 1] = '3'; break;
                    case (byte)148: array[index] = '9'; array[index + 1] = '4'; break;
                    case (byte)149: array[index] = '9'; array[index + 1] = '5'; break;
                    case (byte)150: array[index] = '9'; array[index + 1] = '6'; break;
                    case (byte)151: array[index] = '9'; array[index + 1] = '7'; break;
                    case (byte)152: array[index] = '9'; array[index + 1] = '8'; break;
                    case (byte)153: array[index] = '9'; array[index + 1] = '9'; break;
                    case (byte)154: array[index] = '9'; array[index + 1] = 'A'; break;
                    case (byte)155: array[index] = '9'; array[index + 1] = 'B'; break;
                    case (byte)156: array[index] = '9'; array[index + 1] = 'C'; break;
                    case (byte)157: array[index] = '9'; array[index + 1] = 'D'; break;
                    case (byte)158: array[index] = '9'; array[index + 1] = 'E'; break;
                    case (byte)159: array[index] = '9'; array[index + 1] = 'F'; break;
                    case (byte)160: array[index] = 'A'; array[index + 1] = '0'; break;
                    case (byte)161: array[index] = 'A'; array[index + 1] = '1'; break;
                    case (byte)162: array[index] = 'A'; array[index + 1] = '2'; break;
                    case (byte)163: array[index] = 'A'; array[index + 1] = '3'; break;
                    case (byte)164: array[index] = 'A'; array[index + 1] = '4'; break;
                    case (byte)165: array[index] = 'A'; array[index + 1] = '5'; break;
                    case (byte)166: array[index] = 'A'; array[index + 1] = '6'; break;
                    case (byte)167: array[index] = 'A'; array[index + 1] = '7'; break;
                    case (byte)168: array[index] = 'A'; array[index + 1] = '8'; break;
                    case (byte)169: array[index] = 'A'; array[index + 1] = '9'; break;
                    case (byte)170: array[index] = 'A'; array[index + 1] = 'A'; break;
                    case (byte)171: array[index] = 'A'; array[index + 1] = 'B'; break;
                    case (byte)172: array[index] = 'A'; array[index + 1] = 'C'; break;
                    case (byte)173: array[index] = 'A'; array[index + 1] = 'D'; break;
                    case (byte)174: array[index] = 'A'; array[index + 1] = 'E'; break;
                    case (byte)175: array[index] = 'A'; array[index + 1] = 'F'; break;
                    case (byte)176: array[index] = 'B'; array[index + 1] = '0'; break;
                    case (byte)177: array[index] = 'B'; array[index + 1] = '1'; break;
                    case (byte)178: array[index] = 'B'; array[index + 1] = '2'; break;
                    case (byte)179: array[index] = 'B'; array[index + 1] = '3'; break;
                    case (byte)180: array[index] = 'B'; array[index + 1] = '4'; break;
                    case (byte)181: array[index] = 'B'; array[index + 1] = '5'; break;
                    case (byte)182: array[index] = 'B'; array[index + 1] = '6'; break;
                    case (byte)183: array[index] = 'B'; array[index + 1] = '7'; break;
                    case (byte)184: array[index] = 'B'; array[index + 1] = '8'; break;
                    case (byte)185: array[index] = 'B'; array[index + 1] = '9'; break;
                    case (byte)186: array[index] = 'B'; array[index + 1] = 'A'; break;
                    case (byte)187: array[index] = 'B'; array[index + 1] = 'B'; break;
                    case (byte)188: array[index] = 'B'; array[index + 1] = 'C'; break;
                    case (byte)189: array[index] = 'B'; array[index + 1] = 'D'; break;
                    case (byte)190: array[index] = 'B'; array[index + 1] = 'E'; break;
                    case (byte)191: array[index] = 'B'; array[index + 1] = 'F'; break;
                    case (byte)192: array[index] = 'C'; array[index + 1] = '0'; break;
                    case (byte)193: array[index] = 'C'; array[index + 1] = '1'; break;
                    case (byte)194: array[index] = 'C'; array[index + 1] = '2'; break;
                    case (byte)195: array[index] = 'C'; array[index + 1] = '3'; break;
                    case (byte)196: array[index] = 'C'; array[index + 1] = '4'; break;
                    case (byte)197: array[index] = 'C'; array[index + 1] = '5'; break;
                    case (byte)198: array[index] = 'C'; array[index + 1] = '6'; break;
                    case (byte)199: array[index] = 'C'; array[index + 1] = '7'; break;
                    case (byte)200: array[index] = 'C'; array[index + 1] = '8'; break;
                    case (byte)201: array[index] = 'C'; array[index + 1] = '9'; break;
                    case (byte)202: array[index] = 'C'; array[index + 1] = 'A'; break;
                    case (byte)203: array[index] = 'C'; array[index + 1] = 'B'; break;
                    case (byte)204: array[index] = 'C'; array[index + 1] = 'C'; break;
                    case (byte)205: array[index] = 'C'; array[index + 1] = 'D'; break;
                    case (byte)206: array[index] = 'C'; array[index + 1] = 'E'; break;
                    case (byte)207: array[index] = 'C'; array[index + 1] = 'F'; break;
                    case (byte)208: array[index] = 'D'; array[index + 1] = '0'; break;
                    case (byte)209: array[index] = 'D'; array[index + 1] = '1'; break;
                    case (byte)210: array[index] = 'D'; array[index + 1] = '2'; break;
                    case (byte)211: array[index] = 'D'; array[index + 1] = '3'; break;
                    case (byte)212: array[index] = 'D'; array[index + 1] = '4'; break;
                    case (byte)213: array[index] = 'D'; array[index + 1] = '5'; break;
                    case (byte)214: array[index] = 'D'; array[index + 1] = '6'; break;
                    case (byte)215: array[index] = 'D'; array[index + 1] = '7'; break;
                    case (byte)216: array[index] = 'D'; array[index + 1] = '8'; break;
                    case (byte)217: array[index] = 'D'; array[index + 1] = '9'; break;
                    case (byte)218: array[index] = 'D'; array[index + 1] = 'A'; break;
                    case (byte)219: array[index] = 'D'; array[index + 1] = 'B'; break;
                    case (byte)220: array[index] = 'D'; array[index + 1] = 'C'; break;
                    case (byte)221: array[index] = 'D'; array[index + 1] = 'D'; break;
                    case (byte)222: array[index] = 'D'; array[index + 1] = 'E'; break;
                    case (byte)223: array[index] = 'D'; array[index + 1] = 'F'; break;
                    case (byte)224: array[index] = 'E'; array[index + 1] = '0'; break;
                    case (byte)225: array[index] = 'E'; array[index + 1] = '1'; break;
                    case (byte)226: array[index] = 'E'; array[index + 1] = '2'; break;
                    case (byte)227: array[index] = 'E'; array[index + 1] = '3'; break;
                    case (byte)228: array[index] = 'E'; array[index + 1] = '4'; break;
                    case (byte)229: array[index] = 'E'; array[index + 1] = '5'; break;
                    case (byte)230: array[index] = 'E'; array[index + 1] = '6'; break;
                    case (byte)231: array[index] = 'E'; array[index + 1] = '7'; break;
                    case (byte)232: array[index] = 'E'; array[index + 1] = '8'; break;
                    case (byte)233: array[index] = 'E'; array[index + 1] = '9'; break;
                    case (byte)234: array[index] = 'E'; array[index + 1] = 'A'; break;
                    case (byte)235: array[index] = 'E'; array[index + 1] = 'B'; break;
                    case (byte)236: array[index] = 'E'; array[index + 1] = 'C'; break;
                    case (byte)237: array[index] = 'E'; array[index + 1] = 'D'; break;
                    case (byte)238: array[index] = 'E'; array[index + 1] = 'E'; break;
                    case (byte)239: array[index] = 'E'; array[index + 1] = 'F'; break;
                    case (byte)240: array[index] = 'F'; array[index + 1] = '0'; break;
                    case (byte)241: array[index] = 'F'; array[index + 1] = '1'; break;
                    case (byte)242: array[index] = 'F'; array[index + 1] = '2'; break;
                    case (byte)243: array[index] = 'F'; array[index + 1] = '3'; break;
                    case (byte)244: array[index] = 'F'; array[index + 1] = '4'; break;
                    case (byte)245: array[index] = 'F'; array[index + 1] = '5'; break;
                    case (byte)246: array[index] = 'F'; array[index + 1] = '6'; break;
                    case (byte)247: array[index] = 'F'; array[index + 1] = '7'; break;
                    case (byte)248: array[index] = 'F'; array[index + 1] = '8'; break;
                    case (byte)249: array[index] = 'F'; array[index + 1] = '9'; break;
                    case (byte)250: array[index] = 'F'; array[index + 1] = 'A'; break;
                    case (byte)251: array[index] = 'F'; array[index + 1] = 'B'; break;
                    case (byte)252: array[index] = 'F'; array[index + 1] = 'C'; break;
                    case (byte)253: array[index] = 'F'; array[index + 1] = 'D'; break;
                    case (byte)254: array[index] = 'F'; array[index + 1] = 'E'; break;
                    case (byte)255: array[index] = 'F'; array[index + 1] = 'F'; break;
                }
            }
            else
            {
                switch (b)
                {
                    case (byte)0: array[index] = '0'; array[index + 1] = '0'; break;
                    case (byte)1: array[index] = '0'; array[index + 1] = '1'; break;
                    case (byte)2: array[index] = '0'; array[index + 1] = '2'; break;
                    case (byte)3: array[index] = '0'; array[index + 1] = '3'; break;
                    case (byte)4: array[index] = '0'; array[index + 1] = '4'; break;
                    case (byte)5: array[index] = '0'; array[index + 1] = '5'; break;
                    case (byte)6: array[index] = '0'; array[index + 1] = '6'; break;
                    case (byte)7: array[index] = '0'; array[index + 1] = '7'; break;
                    case (byte)8: array[index] = '0'; array[index + 1] = '8'; break;
                    case (byte)9: array[index] = '0'; array[index + 1] = '9'; break;
                    case (byte)10: array[index] = '0'; array[index + 1] = 'a'; break;
                    case (byte)11: array[index] = '0'; array[index + 1] = 'b'; break;
                    case (byte)12: array[index] = '0'; array[index + 1] = 'c'; break;
                    case (byte)13: array[index] = '0'; array[index + 1] = 'd'; break;
                    case (byte)14: array[index] = '0'; array[index + 1] = 'e'; break;
                    case (byte)15: array[index] = '0'; array[index + 1] = 'f'; break;
                    case (byte)16: array[index] = '1'; array[index + 1] = '0'; break;
                    case (byte)17: array[index] = '1'; array[index + 1] = '1'; break;
                    case (byte)18: array[index] = '1'; array[index + 1] = '2'; break;
                    case (byte)19: array[index] = '1'; array[index + 1] = '3'; break;
                    case (byte)20: array[index] = '1'; array[index + 1] = '4'; break;
                    case (byte)21: array[index] = '1'; array[index + 1] = '5'; break;
                    case (byte)22: array[index] = '1'; array[index + 1] = '6'; break;
                    case (byte)23: array[index] = '1'; array[index + 1] = '7'; break;
                    case (byte)24: array[index] = '1'; array[index + 1] = '8'; break;
                    case (byte)25: array[index] = '1'; array[index + 1] = '9'; break;
                    case (byte)26: array[index] = '1'; array[index + 1] = 'a'; break;
                    case (byte)27: array[index] = '1'; array[index + 1] = 'b'; break;
                    case (byte)28: array[index] = '1'; array[index + 1] = 'c'; break;
                    case (byte)29: array[index] = '1'; array[index + 1] = 'd'; break;
                    case (byte)30: array[index] = '1'; array[index + 1] = 'e'; break;
                    case (byte)31: array[index] = '1'; array[index + 1] = 'f'; break;
                    case (byte)32: array[index] = '2'; array[index + 1] = '0'; break;
                    case (byte)33: array[index] = '2'; array[index + 1] = '1'; break;
                    case (byte)34: array[index] = '2'; array[index + 1] = '2'; break;
                    case (byte)35: array[index] = '2'; array[index + 1] = '3'; break;
                    case (byte)36: array[index] = '2'; array[index + 1] = '4'; break;
                    case (byte)37: array[index] = '2'; array[index + 1] = '5'; break;
                    case (byte)38: array[index] = '2'; array[index + 1] = '6'; break;
                    case (byte)39: array[index] = '2'; array[index + 1] = '7'; break;
                    case (byte)40: array[index] = '2'; array[index + 1] = '8'; break;
                    case (byte)41: array[index] = '2'; array[index + 1] = '9'; break;
                    case (byte)42: array[index] = '2'; array[index + 1] = 'a'; break;
                    case (byte)43: array[index] = '2'; array[index + 1] = 'b'; break;
                    case (byte)44: array[index] = '2'; array[index + 1] = 'c'; break;
                    case (byte)45: array[index] = '2'; array[index + 1] = 'd'; break;
                    case (byte)46: array[index] = '2'; array[index + 1] = 'e'; break;
                    case (byte)47: array[index] = '2'; array[index + 1] = 'f'; break;
                    case (byte)48: array[index] = '3'; array[index + 1] = '0'; break;
                    case (byte)49: array[index] = '3'; array[index + 1] = '1'; break;
                    case (byte)50: array[index] = '3'; array[index + 1] = '2'; break;
                    case (byte)51: array[index] = '3'; array[index + 1] = '3'; break;
                    case (byte)52: array[index] = '3'; array[index + 1] = '4'; break;
                    case (byte)53: array[index] = '3'; array[index + 1] = '5'; break;
                    case (byte)54: array[index] = '3'; array[index + 1] = '6'; break;
                    case (byte)55: array[index] = '3'; array[index + 1] = '7'; break;
                    case (byte)56: array[index] = '3'; array[index + 1] = '8'; break;
                    case (byte)57: array[index] = '3'; array[index + 1] = '9'; break;
                    case (byte)58: array[index] = '3'; array[index + 1] = 'a'; break;
                    case (byte)59: array[index] = '3'; array[index + 1] = 'b'; break;
                    case (byte)60: array[index] = '3'; array[index + 1] = 'c'; break;
                    case (byte)61: array[index] = '3'; array[index + 1] = 'd'; break;
                    case (byte)62: array[index] = '3'; array[index + 1] = 'e'; break;
                    case (byte)63: array[index] = '3'; array[index + 1] = 'f'; break;
                    case (byte)64: array[index] = '4'; array[index + 1] = '0'; break;
                    case (byte)65: array[index] = '4'; array[index + 1] = '1'; break;
                    case (byte)66: array[index] = '4'; array[index + 1] = '2'; break;
                    case (byte)67: array[index] = '4'; array[index + 1] = '3'; break;
                    case (byte)68: array[index] = '4'; array[index + 1] = '4'; break;
                    case (byte)69: array[index] = '4'; array[index + 1] = '5'; break;
                    case (byte)70: array[index] = '4'; array[index + 1] = '6'; break;
                    case (byte)71: array[index] = '4'; array[index + 1] = '7'; break;
                    case (byte)72: array[index] = '4'; array[index + 1] = '8'; break;
                    case (byte)73: array[index] = '4'; array[index + 1] = '9'; break;
                    case (byte)74: array[index] = '4'; array[index + 1] = 'a'; break;
                    case (byte)75: array[index] = '4'; array[index + 1] = 'b'; break;
                    case (byte)76: array[index] = '4'; array[index + 1] = 'c'; break;
                    case (byte)77: array[index] = '4'; array[index + 1] = 'd'; break;
                    case (byte)78: array[index] = '4'; array[index + 1] = 'e'; break;
                    case (byte)79: array[index] = '4'; array[index + 1] = 'f'; break;
                    case (byte)80: array[index] = '5'; array[index + 1] = '0'; break;
                    case (byte)81: array[index] = '5'; array[index + 1] = '1'; break;
                    case (byte)82: array[index] = '5'; array[index + 1] = '2'; break;
                    case (byte)83: array[index] = '5'; array[index + 1] = '3'; break;
                    case (byte)84: array[index] = '5'; array[index + 1] = '4'; break;
                    case (byte)85: array[index] = '5'; array[index + 1] = '5'; break;
                    case (byte)86: array[index] = '5'; array[index + 1] = '6'; break;
                    case (byte)87: array[index] = '5'; array[index + 1] = '7'; break;
                    case (byte)88: array[index] = '5'; array[index + 1] = '8'; break;
                    case (byte)89: array[index] = '5'; array[index + 1] = '9'; break;
                    case (byte)90: array[index] = '5'; array[index + 1] = 'a'; break;
                    case (byte)91: array[index] = '5'; array[index + 1] = 'b'; break;
                    case (byte)92: array[index] = '5'; array[index + 1] = 'c'; break;
                    case (byte)93: array[index] = '5'; array[index + 1] = 'd'; break;
                    case (byte)94: array[index] = '5'; array[index + 1] = 'e'; break;
                    case (byte)95: array[index] = '5'; array[index + 1] = 'f'; break;
                    case (byte)96: array[index] = '6'; array[index + 1] = '0'; break;
                    case (byte)97: array[index] = '6'; array[index + 1] = '1'; break;
                    case (byte)98: array[index] = '6'; array[index + 1] = '2'; break;
                    case (byte)99: array[index] = '6'; array[index + 1] = '3'; break;
                    case (byte)100: array[index] = '6'; array[index + 1] = '4'; break;
                    case (byte)101: array[index] = '6'; array[index + 1] = '5'; break;
                    case (byte)102: array[index] = '6'; array[index + 1] = '6'; break;
                    case (byte)103: array[index] = '6'; array[index + 1] = '7'; break;
                    case (byte)104: array[index] = '6'; array[index + 1] = '8'; break;
                    case (byte)105: array[index] = '6'; array[index + 1] = '9'; break;
                    case (byte)106: array[index] = '6'; array[index + 1] = 'a'; break;
                    case (byte)107: array[index] = '6'; array[index + 1] = 'b'; break;
                    case (byte)108: array[index] = '6'; array[index + 1] = 'c'; break;
                    case (byte)109: array[index] = '6'; array[index + 1] = 'd'; break;
                    case (byte)110: array[index] = '6'; array[index + 1] = 'e'; break;
                    case (byte)111: array[index] = '6'; array[index + 1] = 'f'; break;
                    case (byte)112: array[index] = '7'; array[index + 1] = '0'; break;
                    case (byte)113: array[index] = '7'; array[index + 1] = '1'; break;
                    case (byte)114: array[index] = '7'; array[index + 1] = '2'; break;
                    case (byte)115: array[index] = '7'; array[index + 1] = '3'; break;
                    case (byte)116: array[index] = '7'; array[index + 1] = '4'; break;
                    case (byte)117: array[index] = '7'; array[index + 1] = '5'; break;
                    case (byte)118: array[index] = '7'; array[index + 1] = '6'; break;
                    case (byte)119: array[index] = '7'; array[index + 1] = '7'; break;
                    case (byte)120: array[index] = '7'; array[index + 1] = '8'; break;
                    case (byte)121: array[index] = '7'; array[index + 1] = '9'; break;
                    case (byte)122: array[index] = '7'; array[index + 1] = 'a'; break;
                    case (byte)123: array[index] = '7'; array[index + 1] = 'b'; break;
                    case (byte)124: array[index] = '7'; array[index + 1] = 'c'; break;
                    case (byte)125: array[index] = '7'; array[index + 1] = 'd'; break;
                    case (byte)126: array[index] = '7'; array[index + 1] = 'e'; break;
                    case (byte)127: array[index] = '7'; array[index + 1] = 'f'; break;
                    case (byte)128: array[index] = '8'; array[index + 1] = '0'; break;
                    case (byte)129: array[index] = '8'; array[index + 1] = '1'; break;
                    case (byte)130: array[index] = '8'; array[index + 1] = '2'; break;
                    case (byte)131: array[index] = '8'; array[index + 1] = '3'; break;
                    case (byte)132: array[index] = '8'; array[index + 1] = '4'; break;
                    case (byte)133: array[index] = '8'; array[index + 1] = '5'; break;
                    case (byte)134: array[index] = '8'; array[index + 1] = '6'; break;
                    case (byte)135: array[index] = '8'; array[index + 1] = '7'; break;
                    case (byte)136: array[index] = '8'; array[index + 1] = '8'; break;
                    case (byte)137: array[index] = '8'; array[index + 1] = '9'; break;
                    case (byte)138: array[index] = '8'; array[index + 1] = 'a'; break;
                    case (byte)139: array[index] = '8'; array[index + 1] = 'b'; break;
                    case (byte)140: array[index] = '8'; array[index + 1] = 'c'; break;
                    case (byte)141: array[index] = '8'; array[index + 1] = 'd'; break;
                    case (byte)142: array[index] = '8'; array[index + 1] = 'e'; break;
                    case (byte)143: array[index] = '8'; array[index + 1] = 'f'; break;
                    case (byte)144: array[index] = '9'; array[index + 1] = '0'; break;
                    case (byte)145: array[index] = '9'; array[index + 1] = '1'; break;
                    case (byte)146: array[index] = '9'; array[index + 1] = '2'; break;
                    case (byte)147: array[index] = '9'; array[index + 1] = '3'; break;
                    case (byte)148: array[index] = '9'; array[index + 1] = '4'; break;
                    case (byte)149: array[index] = '9'; array[index + 1] = '5'; break;
                    case (byte)150: array[index] = '9'; array[index + 1] = '6'; break;
                    case (byte)151: array[index] = '9'; array[index + 1] = '7'; break;
                    case (byte)152: array[index] = '9'; array[index + 1] = '8'; break;
                    case (byte)153: array[index] = '9'; array[index + 1] = '9'; break;
                    case (byte)154: array[index] = '9'; array[index + 1] = 'a'; break;
                    case (byte)155: array[index] = '9'; array[index + 1] = 'b'; break;
                    case (byte)156: array[index] = '9'; array[index + 1] = 'c'; break;
                    case (byte)157: array[index] = '9'; array[index + 1] = 'd'; break;
                    case (byte)158: array[index] = '9'; array[index + 1] = 'e'; break;
                    case (byte)159: array[index] = '9'; array[index + 1] = 'f'; break;
                    case (byte)160: array[index] = 'a'; array[index + 1] = '0'; break;
                    case (byte)161: array[index] = 'a'; array[index + 1] = '1'; break;
                    case (byte)162: array[index] = 'a'; array[index + 1] = '2'; break;
                    case (byte)163: array[index] = 'a'; array[index + 1] = '3'; break;
                    case (byte)164: array[index] = 'a'; array[index + 1] = '4'; break;
                    case (byte)165: array[index] = 'a'; array[index + 1] = '5'; break;
                    case (byte)166: array[index] = 'a'; array[index + 1] = '6'; break;
                    case (byte)167: array[index] = 'a'; array[index + 1] = '7'; break;
                    case (byte)168: array[index] = 'a'; array[index + 1] = '8'; break;
                    case (byte)169: array[index] = 'a'; array[index + 1] = '9'; break;
                    case (byte)170: array[index] = 'a'; array[index + 1] = 'a'; break;
                    case (byte)171: array[index] = 'a'; array[index + 1] = 'b'; break;
                    case (byte)172: array[index] = 'a'; array[index + 1] = 'c'; break;
                    case (byte)173: array[index] = 'a'; array[index + 1] = 'd'; break;
                    case (byte)174: array[index] = 'a'; array[index + 1] = 'e'; break;
                    case (byte)175: array[index] = 'a'; array[index + 1] = 'f'; break;
                    case (byte)176: array[index] = 'b'; array[index + 1] = '0'; break;
                    case (byte)177: array[index] = 'b'; array[index + 1] = '1'; break;
                    case (byte)178: array[index] = 'b'; array[index + 1] = '2'; break;
                    case (byte)179: array[index] = 'b'; array[index + 1] = '3'; break;
                    case (byte)180: array[index] = 'b'; array[index + 1] = '4'; break;
                    case (byte)181: array[index] = 'b'; array[index + 1] = '5'; break;
                    case (byte)182: array[index] = 'b'; array[index + 1] = '6'; break;
                    case (byte)183: array[index] = 'b'; array[index + 1] = '7'; break;
                    case (byte)184: array[index] = 'b'; array[index + 1] = '8'; break;
                    case (byte)185: array[index] = 'b'; array[index + 1] = '9'; break;
                    case (byte)186: array[index] = 'b'; array[index + 1] = 'a'; break;
                    case (byte)187: array[index] = 'b'; array[index + 1] = 'b'; break;
                    case (byte)188: array[index] = 'b'; array[index + 1] = 'c'; break;
                    case (byte)189: array[index] = 'b'; array[index + 1] = 'd'; break;
                    case (byte)190: array[index] = 'b'; array[index + 1] = 'e'; break;
                    case (byte)191: array[index] = 'b'; array[index + 1] = 'f'; break;
                    case (byte)192: array[index] = 'c'; array[index + 1] = '0'; break;
                    case (byte)193: array[index] = 'c'; array[index + 1] = '1'; break;
                    case (byte)194: array[index] = 'c'; array[index + 1] = '2'; break;
                    case (byte)195: array[index] = 'c'; array[index + 1] = '3'; break;
                    case (byte)196: array[index] = 'c'; array[index + 1] = '4'; break;
                    case (byte)197: array[index] = 'c'; array[index + 1] = '5'; break;
                    case (byte)198: array[index] = 'c'; array[index + 1] = '6'; break;
                    case (byte)199: array[index] = 'c'; array[index + 1] = '7'; break;
                    case (byte)200: array[index] = 'c'; array[index + 1] = '8'; break;
                    case (byte)201: array[index] = 'c'; array[index + 1] = '9'; break;
                    case (byte)202: array[index] = 'c'; array[index + 1] = 'a'; break;
                    case (byte)203: array[index] = 'c'; array[index + 1] = 'b'; break;
                    case (byte)204: array[index] = 'c'; array[index + 1] = 'c'; break;
                    case (byte)205: array[index] = 'c'; array[index + 1] = 'd'; break;
                    case (byte)206: array[index] = 'c'; array[index + 1] = 'e'; break;
                    case (byte)207: array[index] = 'c'; array[index + 1] = 'f'; break;
                    case (byte)208: array[index] = 'd'; array[index + 1] = '0'; break;
                    case (byte)209: array[index] = 'd'; array[index + 1] = '1'; break;
                    case (byte)210: array[index] = 'd'; array[index + 1] = '2'; break;
                    case (byte)211: array[index] = 'd'; array[index + 1] = '3'; break;
                    case (byte)212: array[index] = 'd'; array[index + 1] = '4'; break;
                    case (byte)213: array[index] = 'd'; array[index + 1] = '5'; break;
                    case (byte)214: array[index] = 'd'; array[index + 1] = '6'; break;
                    case (byte)215: array[index] = 'd'; array[index + 1] = '7'; break;
                    case (byte)216: array[index] = 'd'; array[index + 1] = '8'; break;
                    case (byte)217: array[index] = 'd'; array[index + 1] = '9'; break;
                    case (byte)218: array[index] = 'd'; array[index + 1] = 'a'; break;
                    case (byte)219: array[index] = 'd'; array[index + 1] = 'b'; break;
                    case (byte)220: array[index] = 'd'; array[index + 1] = 'c'; break;
                    case (byte)221: array[index] = 'd'; array[index + 1] = 'd'; break;
                    case (byte)222: array[index] = 'd'; array[index + 1] = 'e'; break;
                    case (byte)223: array[index] = 'd'; array[index + 1] = 'f'; break;
                    case (byte)224: array[index] = 'e'; array[index + 1] = '0'; break;
                    case (byte)225: array[index] = 'e'; array[index + 1] = '1'; break;
                    case (byte)226: array[index] = 'e'; array[index + 1] = '2'; break;
                    case (byte)227: array[index] = 'e'; array[index + 1] = '3'; break;
                    case (byte)228: array[index] = 'e'; array[index + 1] = '4'; break;
                    case (byte)229: array[index] = 'e'; array[index + 1] = '5'; break;
                    case (byte)230: array[index] = 'e'; array[index + 1] = '6'; break;
                    case (byte)231: array[index] = 'e'; array[index + 1] = '7'; break;
                    case (byte)232: array[index] = 'e'; array[index + 1] = '8'; break;
                    case (byte)233: array[index] = 'e'; array[index + 1] = '9'; break;
                    case (byte)234: array[index] = 'e'; array[index + 1] = 'a'; break;
                    case (byte)235: array[index] = 'e'; array[index + 1] = 'b'; break;
                    case (byte)236: array[index] = 'e'; array[index + 1] = 'c'; break;
                    case (byte)237: array[index] = 'e'; array[index + 1] = 'd'; break;
                    case (byte)238: array[index] = 'e'; array[index + 1] = 'e'; break;
                    case (byte)239: array[index] = 'e'; array[index + 1] = 'f'; break;
                    case (byte)240: array[index] = 'f'; array[index + 1] = '0'; break;
                    case (byte)241: array[index] = 'f'; array[index + 1] = '1'; break;
                    case (byte)242: array[index] = 'f'; array[index + 1] = '2'; break;
                    case (byte)243: array[index] = 'f'; array[index + 1] = '3'; break;
                    case (byte)244: array[index] = 'f'; array[index + 1] = '4'; break;
                    case (byte)245: array[index] = 'f'; array[index + 1] = '5'; break;
                    case (byte)246: array[index] = 'f'; array[index + 1] = '6'; break;
                    case (byte)247: array[index] = 'f'; array[index + 1] = '7'; break;
                    case (byte)248: array[index] = 'f'; array[index + 1] = '8'; break;
                    case (byte)249: array[index] = 'f'; array[index + 1] = '9'; break;
                    case (byte)250: array[index] = 'f'; array[index + 1] = 'a'; break;
                    case (byte)251: array[index] = 'f'; array[index + 1] = 'b'; break;
                    case (byte)252: array[index] = 'f'; array[index + 1] = 'c'; break;
                    case (byte)253: array[index] = 'f'; array[index + 1] = 'd'; break;
                    case (byte)254: array[index] = 'f'; array[index + 1] = 'e'; break;
                    case (byte)255: array[index] = 'f'; array[index + 1] = 'f'; break;
                }
            }
        }


        /// <summary>
        /// This function calculator the result: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many divisor it need.
        /// </summary>
        /// <param name="dividend">Number of box.</param>
        /// <param name="divisor">Capacity of box</param>
        /// <returns>The number of box</returns>
        public static int CeilingDivision(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (dividend == 0)
            {
                return 0;
            }

            int count = dividend / divisor;
            if (dividend % divisor != 0)
            {
                count++;
            }
            return count;
        }


        public static long CeilingDivision(long dividend, long divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (dividend == 0)
            {
                return 0;
            }

            long count = dividend / divisor;
            if (dividend % divisor != 0)
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// This function calculator the count: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many ball is filled in last box 
        /// </summary>
        /// <param name="divident"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static int CeilingDivisionLastNumber(int divident, int divisor)
        {
            ThrowHelper.ArgumentMustNotNegative((divident < 0), nameof(divident));
            ThrowHelper.ArgumentMustNotNegative((divisor < 0), nameof(divident));
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (divident == 0)
            {
                return 0;
            }

            int remainder = divident % divisor;
            if (remainder != 0)
            {
                return remainder;
            }
            else
            {
                return divisor;
            }
        }

        /// <summary>
        /// This function calculator the count: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many ball is filled in last box 
        /// </summary>
        /// <param name="divident"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static long CeilingDivisionLastNumber(long divident, long divisor)
        {
            ThrowHelper.ArgumentMustNotNegative((divident < 0), nameof(divident));
            ThrowHelper.ArgumentMustNotNegative((divisor < 0), nameof(divident));
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (divident == 0)
            {
                return 0;
            }

            long remainder = divident % divisor;
            if (remainder != 0)
            {
                return remainder;
            }
            else
            {
                return divisor;
            }
        }


        /// <summary>
        /// This function calculator the result: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many balls fill up all boxes.
        /// </summary>
        /// <param name="dividend">Number of box.</param>
        /// <param name="divisor">Capacity of box</param>
        /// <returns>The number of all box filled</returns>
        public static int CeilingDivisionTotal(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (dividend == 0)
            {
                return 0;
            }

            int count = dividend / divisor;
            if (dividend % divisor != 0)
            {
                count++;
            }
            return count * divisor;
        }

        /// <summary>
        /// This function calculator the result: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many balls fill up all boxes.
        /// </summary>
        /// <param name="dividend">Number of box.</param>
        /// <param name="divisor">Capacity of box</param>
        /// <returns>The number of all box filled</returns>
        public static long CeilingDivisionTotal(long dividend, long divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (dividend == 0)
            {
                return 0;
            }

            long count = dividend / divisor;
            if (dividend % divisor != 0)
            {
                count++;
            }
            return count * divisor;
        }

        /// <summary>
        /// This function calculator the result: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many balls need to padding padding all boxes.
        /// </summary>
        /// <param name="dividend">Number of box.</param>
        /// <param name="divisor">Capacity of box</param>
        /// <returns>The number for padding</returns>
        public static int CeilingDivisionPadding(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (dividend == 0)
            {
                return 0;
            }

            int count = dividend / divisor;
            if (dividend % divisor != 0)
            {
                count++;
            }
            return count * divisor - dividend;
        }

        /// <summary>
        /// This function calculator the result: when want to box dividend balls into box, and each
        /// box's capacity is divisor, how many balls need to padding padding all boxes.
        /// </summary>
        /// <param name="dividend">Number of box.</param>
        /// <param name="divisor">Capacity of box</param>
        /// <returns>The number for padding</returns>
        public static long CeilingDivisionPadding(long dividend, long divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            if (dividend == 0)
            {
                return 0;
            }

            long count = dividend / divisor;
            if (dividend % divisor != 0)
            {
                count++;
            }
            return count * divisor - dividend;
        }

        /// <summary>
        /// Checks whether the value(enum type) contains one or more flag(s).
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool EnumContainsFlags(Enum value, params Enum[] flags)
        {
            ThrowHelper.ArgumentArrayMustNotEmpty((flags.Length == 0), nameof(flags));

            int check = 0;
            foreach (object flag in flags)
            {
                check |= System.Convert.ToInt32(flag, System.Globalization.CultureInfo.InvariantCulture);
            }

            if ((System.Convert.ToInt32(value, System.Globalization.CultureInfo.InvariantCulture) & check) == check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int Sum(params int[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));

            int value = 0;
            foreach (int item in items)
            {
                value += item;
            }
            return value;
        }

        public static long Sum(params long[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));

            long value = 0;
            foreach (long item in items)
            {
                value += item;
            }
            return value;
        }

        public static double Sum(params double[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));

            double value = 0;
            foreach (double item in items)
            {
                value += item;
            }
            return value;
        }

        public static int Min(params int[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            int min = items[0];
            for (int index = 1; index < items.Length; index++)
            {
                if (items[index] < min)
                {
                    min = items[index];
                }
            }
            return min;
        }

        public static long Min(params long[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            long min = items[0];
            for (int index = 1; index < items.Length; index++)
            {
                if (items[index] < min)
                {
                    min = items[index];
                }
            }
            return min;
        }

        public static double Min(params double[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            double min = items[0];
            for (int index = 1; index < items.Length; index++)
            {
                if (items[index] < min)
                {
                    min = items[index];
                }
            }
            return min;
        }


        public static int Max(params int[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            int max = items[0];
            for (int index = 1; index < items.Length; index++)
            {
                if (items[index] > max)
                {
                    max = items[index];
                }
            }
            return max;
        }


        public static long Max(params long[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            long max = items[0];
            for (int index = 1; index < items.Length; index++)
            {
                if (items[index] > max)
                {
                    max = items[index];
                }
            }
            return max;
        }


        public static double Max(params double[] items)
        {
            ThrowHelper.ArgumentNull((items == null), nameof(items));
            ThrowHelper.ArgumentArrayMustNotEmpty((items.Length == 0), nameof(items));

            double max = items[0];
            for (int index = 1; index < items.Length; index++)
            {
                if (items[index] > max)
                {
                    max = items[index];
                }
            }
            return max;
        }

        public static int[] Allocate(int total, double[] percentages)
        {
            ThrowHelper.ArgumentMustNotNegative((total < 0), nameof(total));
            ThrowHelper.ArgumentNull((percentages == null), nameof(percentages));
            ThrowHelper.ArgumentException((Min(percentages) < 0), SR.Calculator_Allocate_PercentagesContainsNegative, nameof(percentages));
            ThrowHelper.ArgumentException((Sum(percentages) != 1), SR.Calculator_Allocate_PercentagesTotalNotEqualsOne, nameof(percentages));

            int[] targets = new int[percentages.Length];

            int index = -1;
            double max = 0;
            for (int i = 0; i < percentages.Length; i++)
            {
                if (percentages[i] > max)
                {
                    max = percentages[i];
                    index = i;
                }
            }

            for (int i = 0; i < percentages.Length; i++)
            {
                if (i != index)
                {
                    targets[i] = System.Convert.ToInt32(Math.Round(total * percentages[i]));
                }
            }
            targets[index] = total - Sum(targets);
            return targets;

        }


        #region Bits Operation
        private static readonly ulong[] BitTemplate = GetBitTemplate();
        private static ulong[] GetBitTemplate()
        {
            ulong[] templates = new ulong[sizeof(ulong) * UnitTable.ByteToBit + 1];
            ulong value = 0u;
            for (int i = 0; i < templates.Length; i++)
            {
                templates[i] = value;

                value <<= 1;
                value |= 1;
            }

            return templates;
        }

        #region Use Bits
        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(SByte value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(Byte value)
        {
            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(Int16 value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(UInt16 value)
        {
            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(Int32 value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(UInt32 value)
        {
            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(Int64 value)
        {
            ThrowHelper.ArgumentMustNotNegative((value < 0), nameof(value));

            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculate how many bits should be use for store specified value.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>number of bits</returns>
        public static int GetUsedBits(UInt64 value)
        {
            ulong item = (ulong)value;
            for (int i = 0; i < Calculator.BitTemplate.Length; i++)
            {
                if (Calculator.BitTemplate[i] >= item)
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion Use Bits

        #region Max Value
        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static SByte GetMaxSByte(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(SByte) * UnitTable.ByteToBit - 1))), nameof(bits));

            return (SByte)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static Byte GetMaxByte(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(Byte) * UnitTable.ByteToBit - 0))), nameof(bits));

            return (Byte)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static Int16 GetMaxInt16(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(Int16) * UnitTable.ByteToBit - 1))), nameof(bits));

            return (Int16)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static UInt16 GetMaxUInt16(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(UInt16) * UnitTable.ByteToBit - 0))), nameof(bits));

            return (UInt16)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static Int32 GetMaxInt32(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(Int32) * UnitTable.ByteToBit - 1))), nameof(bits));

            return (Int32)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static UInt32 GetMaxUInt32(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(UInt32) * UnitTable.ByteToBit - 0))), nameof(bits));

            return (UInt32)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static Int64 GetMaxInt64(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(Int64) * UnitTable.ByteToBit - 1))), nameof(bits));

            return (Int64)Calculator.BitTemplate[bits];
        }

        /// <summary>
        /// Calculate max value that specified bits represents.
        /// </summary>
        /// <param name="bits">bits</param>
        /// <returns>max value for bits</returns>
        public static UInt64 GetMaxUInt64(int bits)
        {
            ThrowHelper.ArgumentOutOfRange(((bits < 0) || (bits > (sizeof(UInt64) * UnitTable.ByteToBit - 0))), nameof(bits));

            return (UInt64)Calculator.BitTemplate[bits];
        }
        #endregion Max Value
        #endregion Bits Operation


    }
}
