using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meision.VisualStudio
{
    public static class EPPlusHelper
    {
        public static DataSet ReadExcelToDataSet(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                return ReadExcelToDataSet(stream);
            }
        }

        public static DataSet ReadExcelToDataSet(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            DataSet dataSet = new DataSet("ds");
            using (ExcelPackage package = new ExcelPackage())
            {
                package.Load(stream);
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    if (sheet.Dimension == null)
                    {
                        continue;
                    }

                    var columnCount = sheet.Dimension.End.Column;
                    var rowCount = sheet.Dimension.End.Row;
                    if (rowCount > 0)
                    {
                        DataTable table = new DataTable(sheet.Name);
                        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                        {
                            object objCellValue = sheet.Cells[1, columnIndex + 1].Value;
                            string cellValue = objCellValue == null ? "" : objCellValue.ToString();
                            table.Columns.Add(cellValue, typeof(string));
                        }

                        for (int rowIndex = 2; rowIndex <= rowCount; rowIndex++)
                        {
                            DataRow row = table.NewRow();
                            for (int columnIndex = 1; columnIndex <= columnCount; columnIndex++)
                            {
                                object objCellValue = sheet.Cells[rowIndex, columnIndex].Value;
                                string cellValue = (objCellValue == null) ? "" : objCellValue.ToString();
                                row[columnIndex - 1] = cellValue;
                            }
                            table.Rows.Add(row);
                        }
                        dataSet.Tables.Add(table);
                    }
                }
            }

            return dataSet;
        }

        public static bool ContainsSheet(string filePath, string sheetName, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                return ContainsSheet(stream, sheetName);
            }
        }

        public static bool ContainsSheet(Stream stream, string sheetName, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                package.Load(stream);
                foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
                {
                    if (sheet.Name.Equals(sheetName, comparison))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }


}
