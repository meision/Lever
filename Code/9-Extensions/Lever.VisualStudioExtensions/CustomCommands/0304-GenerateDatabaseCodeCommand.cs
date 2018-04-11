using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Meision.Database;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class GenerateDatabaseCodeCommand : CustomCommand
    {
        public GenerateDatabaseCodeCommand()
        {
            this.CommandId = 0x0304;
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
            if ((string)this.DTE.SelectedItems.Item(1).ProjectItem.Properties.Item("CustomTool").Value != "DatabaseCodeGenerator")
            {
                return;
            }

            menuItem.Visible = true;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string xmlPath = (string)projectItem.Properties.Item("FullPath").Value;
            
            DatabaseCodeGenerator generator = DatabaseCodeGenerator.Create(xmlPath);
            if (generator == null)
            {
                return;
            }

            // Delete all cs files
            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(xmlPath), "*.cs", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }

            generator.GenerateMain();
            generator.GenerateEntites();

            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(xmlPath), "*.cs", SearchOption.TopDirectoryOnly))
            {
                projectItem.Collection.AddFromFile(file);
            }
        }
    }




}
