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

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class OpenContainingFolderCommand : CustomCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenContainingFolderCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public OpenContainingFolderCommand()
        {
            this.CommandId = 0x0101;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;
            string directory = System.IO.Path.GetDirectoryName(fullPath);
            if (Directory.Exists(directory))
            {
                System.Diagnostics.Process.Start(directory);
            }
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = false;

            if (this.DTE.SelectedItems.Count != 1)
            {
                return;
            }
            
            menuItem.Visible = true;
        }

    }
}
