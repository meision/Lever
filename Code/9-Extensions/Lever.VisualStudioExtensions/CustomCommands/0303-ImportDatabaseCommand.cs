using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using EnvDTE;
using System.Linq;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj;
using System.Text.RegularExpressions;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class ImportDatabaseCommand : CustomCommand
    {
        public ImportDatabaseCommand()
        {
            this.CommandId = 0x0303;
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = false;

            if (this.DTE.SelectedItems.Count != 1)
            {
                return;
            }
            if (this.DTE.SelectedItems.Item(1).ProjectItem.Kind != (string)EnvDTE.Constants.vsProjectItemKindPhysicalFile)
            {
                return;
            }
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            if (!fullPath.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (!EPPlusHelper.ContainsSheet(fullPath, "__Config"))
            {
                return;
            }

            menuItem.Visible = true;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            DataSet dataSet;
            using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                dataSet = EPPlusHelper.ReadExcelToDataSet(stream);
            }
            DataTable configTable = dataSet.Tables["__Config"];
            string connectionStringSource = Convert.ToString(configTable.Rows[0]["ConnectionStringSource"]);
            string connectionStringExpression = Convert.ToString(configTable.Rows[0]["ConnectionStringExpression"]);
            bool clearBeforeImport = Convert.ToBoolean(configTable.Rows[0]["ClearBeforeImport"]);

            List<string> connectionStrings = new List<string>();
            if (connectionStringSource.Equals("Static"))
            {
                connectionStrings.AddRange(connectionStringExpression.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            }
            else if (connectionStringSource.StartsWith("File:"))
            {
                string path = connectionStringSource.Substring("File:".Length);
                string connectionStringFile = Path.GetFullPath(Path.Combine(fullPath, path));
                Match match = Regex.Match(File.ReadAllText(connectionStringFile), connectionStringExpression);
                if (match.Success)
                {
                    connectionStrings.Add(match.Groups["ConnectionString"].Value);
                }
            }
            if (connectionStringExpression.Length < 1)
            {
                return;
            }

        }
    }
}
