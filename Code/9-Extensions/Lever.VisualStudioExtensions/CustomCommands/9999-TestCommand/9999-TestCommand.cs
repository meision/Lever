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
using System.Drawing;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class TestCommand : CustomCommand
    {
        public TestCommand()
        {
            this.CommandId = 0x9999;
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
            if (Path.GetFileNameWithoutExtension(fullPath) != "Test")
            {
                return;
            }

            menuItem.Visible = true;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            this.EnsureNotDirty(projectItem.ContainingProject);
            
            if (string.IsNullOrEmpty((string)projectItem.Properties.Item("CustomToolNamespace").Value))
            {
                projectItem.DeleteDependentFiles();
            }
            else
            {
                string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
                string directory = Path.GetDirectoryName(fullPath);
                System.IO.File.WriteAllBytes(Path.Combine(directory, "1.cs"), new byte[0]);
                projectItem.Collection.AddFromFile(Path.Combine(directory, "1.cs"));
                projectItem.AddDependentFromFiles(Path.Combine(directory, "1.cs"));
    

                System.IO.File.WriteAllBytes(Path.Combine(directory, "2.txt"), new byte[0]);
                projectItem.Collection.AddFromFile(Path.Combine(directory, "2.txt"));
                projectItem.AddDependentFromFiles(Path.Combine(directory, "2.txt"));
            }


            //projectItem.ProjectItems.AddDependentFromFiles(System.IO.Path.Combine(Path.GetDirectoryName(fullPath), "Class1.cs"));
            //string[] s = projectItem.GetDependents();
            //projectItem.Collection.AddFromFile(System.IO.Path.Combine(Path.GetDirectoryName(fullPath), "2.txt"));

        }
    }
}
