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
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            projectItem.ProjectItems.AddFromFile(System.IO.Path.Combine(Path.GetDirectoryName(fullPath), "1.txt"));
            projectItem.ProjectItems.AddFromFile(System.IO.Path.Combine(Path.GetDirectoryName(fullPath), "2.txt"));
        }
    }
}
