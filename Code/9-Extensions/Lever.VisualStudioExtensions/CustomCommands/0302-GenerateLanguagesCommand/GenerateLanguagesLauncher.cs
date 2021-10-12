using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomCommands
{
    public class GenerateLanguagesLauncher : Launcher
    {
        private const string TableName_Languages = "Languages";
        private const string TableName_References = "References";
        private const string ColumnName_References_Name = "Name";
        private const string ColumnName_References_NativeName = "NativeName";

        private static int[][] __duplicateLCIDs = new int[][]
        {
            new int[] {4, 2052}, // zh-Hans
            new int[] {1028, 31748},  // zh-Hant
        };

        private GenerateLanguagesConfig _config;
        private DataSet _dataSet;

        public GenerateLanguagesLauncher(ProjectItem projectItem) : base(projectItem)
        {
        }

        public override bool Launch()
        {
            if (!this.InputFilePath.EndsWith(".xlsx"))
            {
                throw new InvalidDataException("Input file should be excel file.");
            }

            using (FileStream stream = new FileStream(this.InputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                this._dataSet = EPPlusHelper.ReadExcelToDataSet(stream);
            }
            this._config = GenerateLanguagesConfig.CreateFromExcel(this.InputFilePath, this._dataSet);

            switch (this._config.Action)
            {
                case GenerateLanguagesAction.GenerateStrings:
                    this.GenerateStrings();
                    return true;
                case GenerateLanguagesAction.GenerateJsons:
                    this.GenerateJsons();
                    return true;
                default:
                    return false;
            }
        }

        private void GenerateStrings()
        {
            string outputFilePath = this.GetOutputFilePathByExtension(".cs");
            DataTable table;
            if (!this._dataSet.Tables.Contains(TableName_Languages))
            {
                throw new InvalidDataException("Excel sheet \"Languages\" could not be found.");
            }
            table = this._dataSet.Tables[TableName_Languages];

            this.ProjectItem.DeleteDependentFiles();
            byte[] data = this.GenerateStringsData(table);
            System.IO.File.WriteAllBytes(outputFilePath, data);
            ProjectItem item = this.ProjectItem.Collection.AddFromFile(outputFilePath);
            this.ProjectItem.AddDependentItems(item);
        }
        private byte[] GenerateStringsData(DataTable table)
        {
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
            builder.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, Parameters.DO_NOT_MODIFY, this.GetType().Name));
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
                int lcid = Convert.ToInt32(table.Columns[i].ColumnName);
                int[] lcids = GenerateLanguagesLauncher.__duplicateLCIDs.FirstOrDefault(p => p.Contains(lcid));
                if (lcids == null)
                {
                    builder.AppendLine($"            columnMappings.Add({table.Columns[i].ColumnName}, {i - 1});");
                }
                else
                {
                    foreach (int l in lcids)
                    {
                        builder.AppendLine($"            columnMappings.Add({l}, {i - 1});");
                    }
                }
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

        private void GenerateJsons()
        {
            DataTable table;
            if (!this._dataSet.Tables.Contains(TableName_Languages))
            {
                throw new InvalidDataException($"Excel sheet \"{TableName_Languages}\" could not be found.");
            }
            table = this._dataSet.Tables[TableName_Languages];

            DataTable referenceTable;
            if (!this._dataSet.Tables.Contains(TableName_References))
            {
                throw new InvalidDataException($"Excel sheet \"{TableName_References}\" could not be found.");
            }
            referenceTable = this._dataSet.Tables[TableName_References];

            string GetLanguageName(string nativeName)
            {
                foreach (DataRow row in referenceTable.Rows)
                {
                    if ((string)row[ColumnName_References_NativeName] == nativeName)
                    {
                        return (string)row[ColumnName_References_Name];
                    }
                }

                return null;
            }

            string directory = Path.GetDirectoryName(this.InputFilePath);
            this.ProjectItem.DeleteDependentFiles();
            for (int columnIndex = 2; columnIndex < table.Columns.Count; columnIndex++)
            {
                string name = GetLanguageName((string)table.Columns[columnIndex].ColumnName);
                List<string> items = new List<string>();
                for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                {
                    //"Key": "Value",
                    items.Add($"\"{(string)table.Rows[rowIndex][0]}\":\"{(string)table.Rows[rowIndex][columnIndex]}\"");
                }
                string json = $"{{\"Culture\":\"en\",\"Texts\":{{{string.Join(",", items)}}}}}";
                string outputFilePath = Path.Combine(directory, $"{name}.json");
                File.WriteAllText(outputFilePath, json);
                ProjectItem projectItem = this.ProjectItem.Collection.AddFromFile(outputFilePath);
                projectItem.Properties.Item("BuildAction").Value = VSLangProj.prjBuildAction.prjBuildActionEmbeddedResource;
                this.ProjectItem.AddDependentItems(projectItem);

                //this.ProjectItem.AddDependentFromFiles(outputFilePath);
            }
        }

    }
}
