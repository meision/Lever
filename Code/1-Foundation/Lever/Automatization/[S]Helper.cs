using System;

namespace Meision.Automatization
{
    public static class Helper
    {
        public static string EscapeDataString(string text)
        {
            return Uri.EscapeDataString(text);
        }

        public static string UnescapeDataString(string text)
        {
            return Uri.UnescapeDataString(text);
        }

        public static int RandomInteger()
        {
            return new Random().Next();
        }

        public static int RandomInteger(int maxValue)
        {
            return new Random().Next(maxValue);
        }

        public static int RandomInteger(int minValue, int maxValue)
        {
            return new Random().Next(minValue, maxValue);
        }

        public static string ApplicationDirectory()
        {
            string directory = System.AppDomain.CurrentDomain.BaseDirectory;
            return directory;
        }

        public static string PathInApplicationDirectory(string filename)
        {
            return System.IO.Path.Combine(Helper.ApplicationDirectory(), filename);
        }

    }
}
