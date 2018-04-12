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

namespace Meision.VisualStudio.CustomTools
{
    [ComVisible(true)]
    [Guid("B1CED36B-722E-4339-A211-6FD31BFA5102")]
    [CodeGeneratorRegistration(typeof(ExcelTestDataAttributeGenerator), nameof(ExcelTestDataAttributeGenerator), Parameters.guidCSharpProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ExcelTestDataAttributeGenerator), nameof(ExcelTestDataAttributeGenerator), Parameters.guidDotNetCoreProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(ExcelTestDataAttributeGenerator))]
    public class ExcelTestDataAttributeGenerator : BaseCodeGeneratorWithSite
    {
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
            DataSet dataSet;
            using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                dataSet = EPPlusHelper.ReadExcelToDataSet(stream);
            }
            ProjectItems collection = projectItem.Collection;

            // Generate code
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, Parameters.DO_NOT_MODIFY, this.GetType().Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            builder.AppendLine($"using System;");
            builder.AppendLine($"using System.Collections.Generic;");
            builder.AppendLine($"using System.Globalization;");
            builder.AppendLine($"using System.Reflection;");
            builder.AppendLine($"using Xunit.Sdk;");
            builder.AppendLine($"");
            builder.AppendLine($"namespace {this.CodeNamespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"    public class ExcelTestDataAttribute : DataAttribute");
            builder.AppendLine($"    {{");
            builder.AppendLine($"        public override IEnumerable<object[]> GetData(MethodInfo testMethod)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            ParameterInfo[] parameters = testMethod.GetParameters();");
            builder.AppendLine($"            switch ($\"{{testMethod.DeclaringType.Name}}\")");
            builder.AppendLine($"            {{");
            foreach (DataTable table in dataSet.Tables)
            {
                builder.AppendLine($"                case \"{table.TableName}\":");

                ProjectItem referenceItem = null;
                foreach (ProjectItem item in collection)
                {
                    if (item.Name.Equals(table.TableName + ".cs", StringComparison.OrdinalIgnoreCase))
                    {
                        referenceItem = item;
                        break;
                    }
                }
                if (referenceItem != null)
                {
                    FileCodeModel model = referenceItem.FileCodeModel;
                    builder.AppendLine($"                    switch ($\"{{testMethod.Name}}\")");
                    builder.AppendLine($"                    {{");
                    foreach (var group in table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr["Method"].ToString()))
                    {
                        string method = group.Key;
                        CodeFunction eFunction = this.FindMethod(model, table.TableName, method);
                        if (eFunction == null)
                        {
                            continue;
                        }
                        string[] types = eFunction.Parameters.Cast<CodeParameter>().Select(p => p.Type.AsFullName).ToArray();
                        if (!types.All(p => ConvertFuncs.ContainsKey(p)))
                        {
                            continue;
                        }

                        builder.AppendLine($"                        case \"{method}\":");
                        foreach (DataRow row in group)
                        {
                            builder.AppendLine($"                            yield return new object[]");
                            builder.AppendLine($"                            {{");
                            for (int i = 0; i < types.Length; i++)
                            {
                                string text = (string)row[i + 1];
                                builder.AppendLine($"                                {ConvertFuncs[types[i]](text)},");
                            }
                            builder.AppendLine($"                            }};");
                        }
                        builder.AppendLine($"                            yield break;");
                    }
                    builder.AppendLine($"                        default:");
                    builder.AppendLine($"                            yield break;");
                    builder.AppendLine($"                    }}");
                }
                else
                {
                    builder.AppendLine($"                    yield break;");
                }
            }
            builder.AppendLine($"                default:");
            builder.AppendLine($"                    yield break;");
            builder.AppendLine($"            }}");
            builder.AppendLine($"        }}");
            // End Method.

            builder.AppendLine($"    }}");
            builder.AppendLine($"}}");
            builder.AppendLine($"");

            string code = builder.ToString();
            return Encoding.UTF8.GetBytes(code);
        }

        private CodeFunction FindMethod(FileCodeModel model, string className, string methodName)
        {
            foreach (CodeNamespace eNamespace in model.CodeElements.Cast<CodeElement>().OfType<CodeNamespace>())
            {
                foreach (CodeClass eClass in eNamespace.Children.Cast<CodeElement>().OfType<CodeClass>().Where(p => p.Name == className))
                {
                    CodeFunction eFunction = eClass.Children.Cast<CodeElement>().OfType<CodeFunction>().FirstOrDefault(p => p.Name == methodName);
                    if (eFunction != null)
                    {
                        return eFunction;
                    }
                }
            }

            return null;
        }


        private Dictionary<string, Func<string, string>> _convertFuncs;
        private Dictionary<string, Func<string, string>> ConvertFuncs
        {
            get
            {
                if (this._convertFuncs == null)
                {
                    this._convertFuncs = new Dictionary<string, Func<string, string>>
                    {
                        {typeof(Byte[]).FullName, ToBytes },
                        {typeof(Byte).FullName, ToByte },
                        {typeof(Char).FullName, ToChar},
                        {typeof(DateTime).FullName, ToDateTime},
                        {typeof(Decimal).FullName, ToDecimal},
                        {typeof(Double).FullName, ToDouble},
                        {typeof(Int16).FullName, ToInt16},
                        {typeof(Int32).FullName, ToInt32},
                        {typeof(Int64).FullName, ToInt64},
                        {typeof(SByte).FullName, ToSByte},
                        {typeof(Single).FullName, ToSingle},
                        {typeof(String).FullName, ToString},
                        {typeof(UInt16).FullName, ToUInt16},
                        {typeof(UInt32).FullName, ToUInt32},
                        {typeof(UInt64).FullName, ToUInt64},
                    };
                }

                return this._convertFuncs;
            }
        }

        #region ConvertMethods
        private string ToBytes(string text)
        {
            if ((text.Length % 2) != 0)
            {
                throw new ArgumentException("Invalid hex string.", nameof(text));
            }

            // [0xAA, ] = 6 = Lenght * 3
            StringBuilder builder = new StringBuilder("new byte[] {".Length + text.Length * 3 + "}".Length);
            builder.Append("new byte[] {");
            if (text.Length > 0)
            {
                for (int i = 0; i < text.Length; i += 2)
                {
                    builder.Append("0x");
                    builder.Append(text.Substring(i, 2));
                    builder.Append(", ");
                }
                builder.Remove(builder.Length - 2, 2);
            }
            builder.Append("}");
            return builder.ToString();
        }
        private string ToByte(string text)
        {
            return $"(byte){text}";
        }
        private string ToChar(string text)
        {
            return $"(char){text}";
        }
        private string ToDateTime(string text)
        {
            return $"DateTime.Parse(\"{text}\")";
        }
        private string ToDecimal(string text)
        {
            return $"(decimal){text}";
        }
        private string ToDouble(string text)
        {
            return $"(double){text}";
        }
        private string ToInt16(string text)
        {
            return $"(short){text}";
        }
        private string ToInt32(string text)
        {
            return $"(int){text}";
        }
        private string ToInt64(string text)
        {
            return $"(long){text}";
        }
        private string ToSByte(string text)
        {
            return $"(sbyte){text}";
        }
        private string ToSingle(string text)
        {
            return $"(float){text}";
        }
        private string ToString(string text)
        {
            return $"@\"{text.Replace("\"", "\"\"")}\"";
        }
        private string ToUInt16(string text)
        {
            return $"(ushort){text}";
        }
        private string ToUInt32(string text)
        {
            return $"(uint){text}";
        }
        private string ToUInt64(string text)
        {
            return $"(ulong){text}";
        }
        #endregion ConvertMethods

    }
}
