using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomTools
{
    [ComVisible(true)]
    [Guid("B1CED36B-722E-4339-A211-6FD31BFA5101")]
    [CodeGeneratorRegistration(typeof(ExcelLanguagesGenerator), nameof(ExcelLanguagesGenerator), Parameters.guidCSharpProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ExcelLanguagesGenerator), nameof(ExcelLanguagesGenerator), Parameters.guidDotNetCoreProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(ExcelLanguagesGenerator))]
    public class ExcelLanguagesGenerator : BaseCodeGeneratorWithSite
    {
        private const string TABLENAME_LANGUAGES = "Languages";
        
        protected override byte[] GenerateData(string inputFileContent)
        {
            if (!this.InputFilePath.EndsWith(".xlsx"))
            {
                this.GeneratorError(1, "Input file should be excel file.", 0, 0);
                return null;
            }

            DTE dte = (DTE)this.GetService(typeof(DTE));
            ProjectItem projectItem = this.GetProjectItem();
            byte[] data = this.GenerateDataFromProjectItem(projectItem);
            return data;
        }

        internal protected override byte[] GenerateDataFromProjectItem(ProjectItem projectItem)
        {
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            DataTable table;
            using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                DataSet dataSet = EPPlusHelper.ReadExcelToDataSet(stream);
                if (!dataSet.Tables.Contains(TABLENAME_LANGUAGES))
                {
                    this.GeneratorError(1, "Excel sheet \"Languages\" could not be found.", 0, 0);
                    return null;
                }

                table = dataSet.Tables[TABLENAME_LANGUAGES];
            }
            // update locale info.
            string className = Path.GetFileNameWithoutExtension(this.InputFilePath);
            List<string> locales = new List<string>();
            table.Columns[0].ColumnName = "Identifier";
            table.Columns[1].ColumnName = CultureInfo.InvariantCulture.LCID.ToString();
            locales.Add(CultureInfo.InvariantCulture.Name);
            for (int columnIndex = 2; columnIndex < table.Columns.Count; columnIndex++)
            {
                string nativeName = table.Columns[columnIndex].ColumnName;
                CultureInfo culture = LanguageManager.Default.GetCultureByNativeName(nativeName);
                table.Columns[columnIndex].ColumnName = culture.LCID.ToString();
                locales.Add(culture.Name);
            }
            // Generate code
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, Parameters.DO_NOT_MODIFY, this.GetType().Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            builder.AppendLine($"using System;");
            builder.AppendLine($"using System.Collections.Generic;");
            builder.AppendLine($"using System.Collections.ObjectModel;");
            builder.AppendLine($"using System.Globalization;");
            builder.AppendLine($"using System.Threading;");
            builder.AppendLine($"");
            builder.AppendLine($"namespace {this.CodeNamespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    static partial class {className}");
            builder.AppendLine($"    {{");
            builder.AppendLine($"        private static readonly ReadOnlyCollection<CultureInfo> __locales = new ReadOnlyCollection<CultureInfo>(new CultureInfo[]");
            builder.AppendLine($"        {{");
            for (int i = 0; i < locales.Count; i++)
            {
                builder.AppendLine($"            new CultureInfo(\"{locales[i]}\"),");
            }
            builder.AppendLine($"        }});");
            builder.AppendLine($"");
            builder.AppendLine($"        /// <summary>");
            builder.AppendLine($"        /// Get all support locales.");
            builder.AppendLine($"        /// </summary>");
            builder.AppendLine($"        /// <returns>A ReadOnlyCollection instance.</returns>");
            builder.AppendLine($"        public static ReadOnlyCollection<CultureInfo> GetSupportLocales()");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            return {className}.__locales;");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        private static readonly Dictionary<int, int> __columnMappings = GetColumnMappings();");
            builder.AppendLine($"        private static Dictionary<int, int> GetColumnMappings()");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            Dictionary<int, int> columnMappings = new Dictionary<int, int>();");

            for (int i = 1; i < table.Columns.Count; i++)
            {
                builder.AppendLine($"            columnMappings.Add({table.Columns[i].ColumnName}, {i - 1});");
            }
            builder.AppendLine($"            return columnMappings;");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        private static readonly Dictionary<string, string[]> __languages = GetLanguages();");
            builder.AppendLine($"        private static Dictionary<string, string[]> GetLanguages()");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            string key;");
            builder.AppendLine($"            string[] value;");
            builder.AppendLine($"");
            builder.AppendLine($"            Dictionary<string, string[]> languages = new Dictionary<string, string[]>({table.Rows.Count});");
            foreach (DataRow row in table.Rows)
            {
                builder.AppendLine($"            key = @\"{((string)row[0]).Replace("\"", "\"\"")}\";");
                builder.AppendLine($"            value = new string[{table.Columns.Count - 1}];");
                for (int i = 1; i < table.Columns.Count; i++)
                {
                    if (Convert.IsDBNull(row[i]))
                    {
                        builder.AppendLine($"            value[{i - 1}] = null;");
                    }
                    else
                    {
                        builder.AppendLine($"            value[{i - 1}] = @\"{((string)row[i]).Replace("\"", "\"\"")}\";");
                    }
                }
                builder.AppendLine($"            languages.Add(key, value);");
            }
            builder.AppendLine($"            return languages;");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        public static CultureInfo GetAvailableCulture(CultureInfo culture)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            CultureInfo current = culture;");
            builder.AppendLine($"            while (true)");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                if ({className}.__columnMappings.ContainsKey(current.LCID))");
            builder.AppendLine($"                {{");
            builder.AppendLine($"                    return current;");
            builder.AppendLine($"                }}");
            builder.AppendLine($"");
            builder.AppendLine($"                current = current.Parent;");
            builder.AppendLine($"            }}");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        public static string GetString(string name)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            if (name == null)");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                throw new ArgumentNullException(nameof(name));");
            builder.AppendLine($"            }}");
            builder.AppendLine($"");
            builder.AppendLine($"            return {className}.GetStringInternal(name, Thread.CurrentThread.CurrentUICulture);");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        public static string GetString(string name, CultureInfo culture)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            if (name == null)");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                throw new ArgumentNullException(nameof(name));");
            builder.AppendLine($"            }}");
            builder.AppendLine($"            if (culture == null)");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                throw new ArgumentNullException(nameof(culture));");
            builder.AppendLine($"            }}");
            builder.AppendLine($"");
            builder.AppendLine($"            return {className}.GetStringInternal(name, culture);");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            builder.AppendLine($"        private static string GetStringInternal(string name, CultureInfo culture)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            if (!{className}.__languages.ContainsKey(name))");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                return null;");
            builder.AppendLine($"            }}");
            builder.AppendLine($"            if (!{className}.__columnMappings.ContainsKey(culture.LCID))");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                culture = {className}.GetAvailableCulture(culture);");
            builder.AppendLine($"            }}");
            builder.AppendLine($"");
            builder.AppendLine($"            int index = {className}.__columnMappings[culture.LCID];");
            builder.AppendLine($"            string text = {className}.__languages[name][index];");
            builder.AppendLine($"            if (text == null)");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                text = {className}.__languages[name][0];");
            builder.AppendLine($"            }}");
            builder.AppendLine($"");
            builder.AppendLine($"            return text;");
            builder.AppendLine($"        }}");
            builder.AppendLine($"");
            foreach (DataRow row in table.Rows)
            {
                builder.AppendLine($"        /// <summary>");
                foreach (string line in ((string)row[1]).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    builder.AppendLine($"        /// {line}");
                }
                builder.AppendLine($"        /// </summary>");
                builder.AppendLine($"        public static string {row[0]}");
                builder.AppendLine($"        {{");
                builder.AppendLine($"            get");
                builder.AppendLine($"            {{");
                builder.AppendLine($"                return {className}.GetStringInternal(\"{row[0]}\", Thread.CurrentThread.CurrentUICulture);");
                builder.AppendLine($"            }}");
                builder.AppendLine($"        }}");
                builder.AppendLine($"");
            }
            builder.AppendLine($"    }}");
            builder.AppendLine($"}}");

            string code = builder.ToString();
            return Encoding.UTF8.GetBytes(code);
        }

    }
}
