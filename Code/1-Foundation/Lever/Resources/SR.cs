// Generate by Lever visual studio extension - ExcelLanguagesGenerator. Please do not modify this file manully.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;

namespace Meision.Resources
{
    static partial class SR
    {
        private static readonly ReadOnlyCollection<CultureInfo> __locales = new ReadOnlyCollection<CultureInfo>(new CultureInfo[]
        {
            new CultureInfo(""),
            new CultureInfo("en"),
            new CultureInfo("zh-Hans"),
            new CultureInfo("zh-Hant"),
        });

        /// <summary>
        /// Get all support locales.
        /// </summary>
        /// <returns>A ReadOnlyCollection instance.</returns>
        public static ReadOnlyCollection<CultureInfo> GetSupportLocales()
        {
            return SR.__locales;
        }

        private static readonly Dictionary<int, int> __columnMappings = GetColumnMappings();
        private static Dictionary<int, int> GetColumnMappings()
        {
            Dictionary<int, int> columnMappings = new Dictionary<int, int>();
            columnMappings.Add(127, 0);
            columnMappings.Add(9, 1);
            columnMappings.Add(4, 2);
            columnMappings.Add(31748, 3);
            return columnMappings;
        }

        private static readonly Dictionary<string, string[]> __languages = GetLanguages();
        private static Dictionary<string, string[]> GetLanguages()
        {
            string key;
            string[] value;

            Dictionary<string, string[]> languages = new Dictionary<string, string[]>(3);
            key = @"_Exception_IndexOutOfRange";
            value = new string[4];
            value[0] = @"Index(Offset) must not be negative or not larger than array size.";
            value[1] = @"";
            value[2] = @"";
            value[3] = @"";
            languages.Add(key, value);
            key = @"_Exception_InvalidIndexLength";
            value = new string[4];
            value[0] = @"Index(Offset) and length(Count) must refer to a location within the array.";
            value[1] = @"";
            value[2] = @"";
            value[3] = @"";
            languages.Add(key, value);
            key = @"_Exception_LengthOutOfRange";
            value = new string[4];
            value[0] = @"Length(Count) must not be negative or not larger than array size.";
            value[1] = @"";
            value[2] = @"";
            value[3] = @"";
            languages.Add(key, value);
            return languages;
        }

        public static CultureInfo GetAvailableCulture(CultureInfo culture)
        {
            CultureInfo current = culture;
            while (true)
            {
                if (SR.__columnMappings.ContainsKey(current.LCID))
                {
                    return current;
                }

                current = current.Parent;
            }
        }

        public static string GetString(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return SR.GetStringInternal(name, Thread.CurrentThread.CurrentUICulture);
        }

        public static string GetString(string name, CultureInfo culture)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            return SR.GetStringInternal(name, culture);
        }

        private static string GetStringInternal(string name, CultureInfo culture)
        {
            if (!SR.__languages.ContainsKey(name))
            {
                return null;
            }
            if (!SR.__columnMappings.ContainsKey(culture.LCID))
            {
                culture = SR.GetAvailableCulture(culture);
            }

            int index = SR.__columnMappings[culture.LCID];
            string text = SR.__languages[name][index];
            if (text == null)
            {
                text = SR.__languages[name][0];
            }

            return text;
        }

        /// <summary>
        /// Index(Offset) must not be negative or not larger than array size.
        /// </summary>
        public static string _Exception_IndexOutOfRange
        {
            get
            {
                return SR.GetStringInternal("_Exception_IndexOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Index(Offset) and length(Count) must refer to a location within the array.
        /// </summary>
        public static string _Exception_InvalidIndexLength
        {
            get
            {
                return SR.GetStringInternal("_Exception_InvalidIndexLength", Thread.CurrentThread.CurrentUICulture);
            }
        }

        /// <summary>
        /// Length(Count) must not be negative or not larger than array size.
        /// </summary>
        public static string _Exception_LengthOutOfRange
        {
            get
            {
                return SR.GetStringInternal("_Exception_LengthOutOfRange", Thread.CurrentThread.CurrentUICulture);
            }
        }

    }
}
