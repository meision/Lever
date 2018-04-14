using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Meision.Resources;

namespace Meision.Coding
{
    public static class Cryptography
    {
        #region QuotedPrintable
        public static string QuotedPrintableEncode(byte[] bytes)
        {
            const int Max_LineLength = 76;

            #region Rule Comments
            /* 
               Rule #1: (General 8-bit representation) Any octet, except those
      indicating a line break according to the newline convention of the
      canonical (standard) form of the data being encoded, may be
      represented by an "=" followed by a two digit hexadecimal
      representation of the octet's value.  The digits of the
      hexadecimal alphabet, for this purpose, are "0123456789ABCDEF".
      Uppercase letters must be used when sending hexadecimal data,
      though a robust implementation may choose to recognize lowercase
      letters on receipt.  Thus, for example, the value 12 (ASCII form
      feed) can be represented by "=0C", and the value 61 (ASCII EQUAL
      SIGN) can be represented by "=3D".  Except when the following
      rules allow an alternative encoding, this rule is mandatory.

      Rule #2: (Literal representation) Octets with decimal values of 33
      through 60 inclusive, and 62 through 126, inclusive, MAY be
      represented as the ASCII characters which correspond to those
      octets (EXCLAMATION POINT through LESS THAN, and GREATER THAN
      through TILDE, respectively).

      Rule #3: (White Space): Octets with values of 9 and 32 MAY be
      represented as ASCII TAB (HT) and SPACE characters, respectively,
      but MUST NOT be so represented at the end of an encoded line. Any
      TAB (HT) or SPACE characters on an encoded line MUST thus be
      followed on that line by a printable character.  In particular, an
      "=" at the end of an encoded line, indicating a soft line break
      (see rule #5) may follow one or more TAB (HT) or SPACE characters.
      It follows that an octet with value 9 or 32 appearing at the end
      of an encoded line must be represented according to Rule #1.  This
      rule is necessary because some MTAs (Message Transport Agents,
      programs which transport messages from one user to another, or
      perform a part of such transfers) are known to pad lines of text
      with SPACEs, and others are known to remove "white space"
      characters from the end of a line.  Therefore, when decoding a
      Quoted-Printable body, any trailing white space on a line must be
      deleted, as it will necessarily have been added by intermediate
      transport agents.

      Rule #4 (Line Breaks): A line break in a text body, independent of
      what its representation is following the canonical representation
      of the data being encoded, must be represented by a (RFC 822) line
      break, which is a CRLF sequence, in the Quoted-Printable encoding.
      Since the canonical representation of types other than text do not
      generally include the representation of line breaks, no hard line
      breaks (i.e.  line breaks that are intended to be meaningful and
      to be displayed to the user) should occur in the quoted-printable
      encoding of such types.  Of course, occurrences of "=0D", "=0A",
      "0A=0D" and "=0D=0A" will eventually be encountered.  In general,
      however, base64 is preferred over quoted-printable for binary
      data.

      Note that many implementations may elect to encode the local
      representation of various content types directly, as described in
      Appendix G.  In particular, this may apply to plain text material
      on systems that use newline conventions other than CRLF
      delimiters. Such an implementation is permissible, but the
      generation of line breaks must be generalized to account for the
      case where alternate representations of newline sequences are
      used.

      Rule #5 (Soft Line Breaks): The Quoted-Printable encoding REQUIRES
      that encoded lines be no more than 76 characters long. If longer
      lines are to be encoded with the Quoted-Printable encoding, 'soft'
      line breaks must be used. An equal sign as the last character on a
      encoded line indicates such a non-significant ('soft') line break
      in the encoded text. Thus if the "raw" form of the line is a
      single unencoded line that says:

          Now's the time for all folk to come to the aid of
          their country.

      This can be represented, in the Quoted-Printable encoding, as

          Now's the time =
          for all folk to come=
           to the aid of their country.

      This provides a mechanism with which long lines are encoded in
      such a way as to be restored by the user agent.  The 76 character
      limit does not count the trailing CRLF, but counts all other
      characters, including any equal signs.
             */
            #endregion Rule Comments

            StringBuilder allBuffer = new StringBuilder();
            StringBuilder lineBuffer = new StringBuilder(Max_LineLength + 2, Max_LineLength + 2); // line max size is 78 (include CRLF)

            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                if ((b == 9) // Tab
                 || (b == 32) // Space
                 || ((33 <= b) && (b <= 60)) // printable character
                 || ((62 <= b) && (b <= 126))) // printable character
                {
                    if (lineBuffer.Length >= Max_LineLength - 1)
                    {
                        lineBuffer.Append("=\r\n");
                        allBuffer.Append(lineBuffer.ToString());
                        lineBuffer.Length = 0;
                    }

                    lineBuffer.Append((char)b);
                }
                else if ((b == 13) && (i < (bytes.Length - 1)) && (bytes[i + 1] == 10))
                {
                    lineBuffer.Append("\r\n");
                    allBuffer.Append(lineBuffer.ToString());
                    lineBuffer.Length = 0;
                    i++;
                }
                else
                {
                    if (lineBuffer.Length >= Max_LineLength - 3)
                    {
                        lineBuffer.Append("=\r\n");
                        allBuffer.Append(lineBuffer.ToString());
                        lineBuffer.Length = 0;
                    }

                    lineBuffer.Append("=");
                    lineBuffer.Append(b.ToString("X2"));
                }
            }

            if (lineBuffer.Length > 0)
            {
                allBuffer.Append(lineBuffer.ToString());
            }

            return allBuffer.ToString();
        }

        public static byte[] QuotedPrintableDecode(string text)
        {
            ThrowHelper.ArgumentNull((text == null), nameof(text));

            List<byte> bytes = new List<byte>();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if ((c == 9) // Tab
                 || (c == 32) // Space
                 || ((33 <= c) && (c <= 60)) // printable character
                 || ((62 <= c) && (c <= 126))) // printable character
                {
                    bytes.Add((byte)c);
                }
                else if (c == 61) // '='
                {
                    i++;
                    System.Diagnostics.Debug.Assert(i < text.Length);
                    char c1 = text[i];
                    i++;
                    System.Diagnostics.Debug.Assert(i < text.Length);
                    char c2 = text[i];

                    if ((c1 == 13) && (c2 == 10)) // \r\n
                    {
                        continue;
                    }

                    byte b;
                    // first character
                    switch (c1)
                    {
                        case '0':
                            b = 0x00;
                            break;
                        case '1':
                            b = 0x10;
                            break;
                        case '2':
                            b = 0x20;
                            break;
                        case '3':
                            b = 0x30;
                            break;
                        case '4':
                            b = 0x40;
                            break;
                        case '5':
                            b = 0x50;
                            break;
                        case '6':
                            b = 0x60;
                            break;
                        case '7':
                            b = 0x70;
                            break;
                        case '8':
                            b = 0x80;
                            break;
                        case '9':
                            b = 0x90;
                            break;
                        case 'a':
                        case 'A':
                            b = 0xA0;
                            break;
                        case 'b':
                        case 'B':
                            b = 0xB0;
                            break;
                        case 'c':
                        case 'C':
                            b = 0xC0;
                            break;
                        case 'd':
                        case 'D':
                            b = 0xD0;
                            break;
                        case 'e':
                        case 'E':
                            b = 0xE0;
                            break;
                        case 'f':
                        case 'F':
                            b = 0xF0;
                            break;
                        default:
                            throw new InvalidOperationException(SR.Cryptography_QuotedPrintable_Exception_InvalidText);
                    }
                    // second character
                    switch (c2)
                    {
                        case '0':
                            b = (byte)(b | 0x00);
                            break;
                        case '1':
                            b = (byte)(b | 0x01);
                            break;
                        case '2':
                            b = (byte)(b | 0x02);
                            break;
                        case '3':
                            b = (byte)(b | 0x03);
                            break;
                        case '4':
                            b = (byte)(b | 0x04);
                            break;
                        case '5':
                            b = (byte)(b | 0x05);
                            break;
                        case '6':
                            b = (byte)(b | 0x06);
                            break;
                        case '7':
                            b = (byte)(b | 0x07);
                            break;
                        case '8':
                            b = (byte)(b | 0x08);
                            break;
                        case '9':
                            b = (byte)(b | 0x09);
                            break;
                        case 'a':
                        case 'A':
                            b = (byte)(b | 0x0A);
                            break;
                        case 'b':
                        case 'B':
                            b = (byte)(b | 0x0B);
                            break;
                        case 'c':
                        case 'C':
                            b = (byte)(b | 0x0C);
                            break;
                        case 'd':
                        case 'D':
                            b = (byte)(b | 0x0D);
                            break;
                        case 'e':
                        case 'E':
                            b = (byte)(b | 0x0E);
                            break;
                        case 'f':
                        case 'F':
                            b = (byte)(b | 0x0F);
                            break;
                        default:
                            throw new InvalidOperationException(SR.Cryptography_QuotedPrintable_Exception_InvalidText);
                    }
                    bytes.Add(b);
                }
                else if (c == 13) // 'r'
                {
                    i++;
                    System.Diagnostics.Debug.Assert(i < text.Length);
                    char c1 = text[i];
                    if (c1 != 10)
                    {
                        throw new InvalidOperationException(SR.Cryptography_QuotedPrintable_Exception_InvalidText);
                    }

                    // remove last blank
                    for (int j = bytes.Count - 1; j >= 0; j--)
                    {
                        if ((bytes[j] != 9) && (bytes[j] != 32)) // neight Tab nor Space
                        {
                            break;
                        }

                        bytes.RemoveAt(j);
                    }
                    // add crlf
                    bytes.Add(13);
                    bytes.Add(10);
                }
                else
                {
                    throw new InvalidOperationException(SR.Cryptography_QuotedPrintable_Exception_InvalidText);
                }
            }

            return bytes.ToArray();
        }
        #endregion QuotedPrintable

        #region MD5
        public static MD5Code MD5Encode(string clearText)
        {
            byte[] buffer = MD5Encode(Encoding.UTF8.GetBytes(clearText));
            return new MD5Code(buffer);
        }

        public static byte[] MD5Encode(byte[] clear)
        {
            return MD5Encode(clear, 0, clear.Length);
        }

        public static byte[] MD5Encode(byte[] clear, int offset, int count)
        {
            MD5 provider = MD5.Create();
            byte[] hash = provider.ComputeHash(clear, offset, count);
            return hash;
        }
        #endregion

        #region DES
        internal static readonly byte[] DefaultDESKey = GetDefaultDESKey();
        private static byte[] GetDefaultDESKey()
        {
            byte[] buffer = Assembly.GetExecutingAssembly().ReadResourceFile(typeof(SR), "key.dat");
            byte[] array = MD5Encode(buffer);
            byte[] md5 = new byte[8];
            for (int i = 0; i < md5.Length; i++)
            {
                md5[i] = array[i * 2];
            }
            return md5;
        }

        public static byte[] DesEncode(byte[] clear, byte[] key, byte[] iv)
        {
            // Validate parameter
            ThrowHelper.ArgumentNull((clear == null), nameof(clear));
            ThrowHelper.ArgumentNull((key == null), nameof(key));
            ThrowHelper.ArgumentException((key.Length != (64 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidKey, nameof(key));
            ThrowHelper.ArgumentNull((iv == null), nameof(iv));
            ThrowHelper.ArgumentException((iv.Length != (64 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidIV, nameof(iv));


            using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
            using (DES des = DESCryptoServiceProvider.Create())
            using (ICryptoTransform transform = des.CreateEncryptor(key, iv))
            using (CryptoStream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
            {
                stream.Write(clear, 0, clear.Length);
                stream.FlushFinalBlock();
                return memory.ToArray();
            }
        }

        public static byte[] DesDecode(byte[] cipher, byte[] key, byte[] iv)
        {
            // Validate parameter
            ThrowHelper.ArgumentNull((cipher == null), nameof(cipher));
            ThrowHelper.ArgumentNull((key == null), nameof(key));
            ThrowHelper.ArgumentException((key.Length != (64 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidKey, nameof(key));
            ThrowHelper.ArgumentNull((iv == null), nameof(iv));
            ThrowHelper.ArgumentException((iv.Length != (64 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidIV, nameof(iv));

            using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
            using (DES des = DESCryptoServiceProvider.Create())
            using (ICryptoTransform transform = des.CreateDecryptor(key, iv))
            using (CryptoStream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
            {
                stream.Write(cipher, 0, cipher.Length);
                stream.FlushFinalBlock();
                return memory.ToArray();
            }
        }
        #endregion DES

        #region AES
        internal static readonly byte[] DefaultAESKey = GetDefaultAESKey();
        private static byte[] GetDefaultAESKey()
        {
            byte[] buffer = Assembly.GetExecutingAssembly().ReadResourceFile(typeof(SR), "key.dat");
            return MD5Encode(buffer);
        }

        public static byte[] AesEncode(byte[] clear, byte[] iv)
        {
            return Cryptography.AesEncode(clear, Cryptography.DefaultAESKey, iv);
        }

        public static byte[] AesEncode(byte[] clear, byte[] key, byte[] iv)
        {
            // Validate parameter
            ThrowHelper.ArgumentNull((clear == null), nameof(clear));
            ThrowHelper.ArgumentNull((key == null), nameof(key));
            ThrowHelper.ArgumentException((key.Length != (128 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidKey, nameof(key));
            ThrowHelper.ArgumentNull((iv == null), nameof(iv));
            ThrowHelper.ArgumentException((iv.Length != (128 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidIV, nameof(iv));


            using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
            using (RijndaelManaged aes = new RijndaelManaged())
            using (ICryptoTransform transform = aes.CreateEncryptor(key, iv))
            using (CryptoStream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
            {
                stream.Write(clear, 0, clear.Length);
                stream.FlushFinalBlock();
                return memory.ToArray();
            }
        }

        public static byte[] AesDecode(byte[] cipher, byte[] key, byte[] iv)
        {
            // Validate parameter
            ThrowHelper.ArgumentNull((cipher == null), nameof(cipher));
            ThrowHelper.ArgumentNull((key == null), nameof(key));
            ThrowHelper.ArgumentException((key.Length != (128 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidKey, nameof(key));
            ThrowHelper.ArgumentNull((iv == null), nameof(iv));
            ThrowHelper.ArgumentException((iv.Length != (128 / Meision.Algorithms.UnitTable.ByteToBit)), SR.Cryptography_Exceiption_InvalidIV, nameof(iv));

            using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
            using (RijndaelManaged aes = new RijndaelManaged())
            using (ICryptoTransform transform = aes.CreateDecryptor(key, iv))
            using (CryptoStream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
            {
                stream.Write(cipher, 0, cipher.Length);
                stream.FlushFinalBlock();
                return memory.ToArray();
            }
        }
        #endregion AES
    }
}