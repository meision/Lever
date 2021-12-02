using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meision.VisualStudio.CustomCommands
{
    internal enum GenerateLanguagesAction
    {
        GenerateStrings,
        GenerateInternalStrings,
        GenerateJsons
    }

    internal class GenerateLanguagesConfig
    {
        public const string DefaultSheetName = "__Config";

        public static GenerateLanguagesConfig CreateFromExcel(string fullname)
        {
            DataSet dataSet = EPPlusHelper.ReadExcelToDataSet(fullname, (sheetName) => DefaultSheetName.Equals(sheetName, StringComparison.OrdinalIgnoreCase));
            return CreateFromExcel(fullname, dataSet);
        }
        public static GenerateLanguagesConfig CreateFromExcel(string excelFilePath, DataSet dataSet)
        {
            if (excelFilePath == null)
            {
                throw new ArgumentNullException(nameof(excelFilePath));
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException(nameof(dataSet));
            }
            DataTable table = dataSet.Tables[DefaultSheetName];
            if (table == null)
            {
                return null;
            }

            GenerateLanguagesConfig config = new GenerateLanguagesConfig();
            // Action
            config.Action = (GenerateLanguagesAction)Enum.Parse(typeof(GenerateLanguagesAction), Convert.ToString(table.Rows[0]["Action"]));        
            return config;
        }

        public GenerateLanguagesAction Action { get; set; }
    }
}
