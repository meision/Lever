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
            menuItem.Visible = this.IsSelectedSingleFileWithCustomTool("Test");
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
                ProjectItem item1 = projectItem.Collection.AddFromFile(Path.Combine(directory, "1.cs"));
                projectItem.AddDependentItems(item1);

                System.IO.File.WriteAllBytes(Path.Combine(directory, "2.txt"), new byte[0]);
                ProjectItem item2 = projectItem.Collection.AddFromFile(Path.Combine(directory, "2.txt"));
                projectItem.AddDependentItems(item2);
            }
            projectItem.ContainingProject.Save();

            //projectItem.ProjectItems.AddDependentFromFiles(System.IO.Path.Combine(Path.GetDirectoryName(fullPath), "Class1.cs"));
            //string[] s = projectItem.GetDependents();
            //projectItem.Collection.AddFromFile(System.IO.Path.Combine(Path.GetDirectoryName(fullPath), "2.txt"));

        }
    }
}
