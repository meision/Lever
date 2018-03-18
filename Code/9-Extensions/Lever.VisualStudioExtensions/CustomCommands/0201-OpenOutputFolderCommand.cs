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
    internal sealed class OpenOutputFolderCommand : CustomCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenOutputFolderCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public OpenOutputFolderCommand()
        {
            this.CommandId = 0x0201;
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

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            Project project = this.DTE.SelectedItems.Item(1).Project;
            string outputPath = project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
            string directory = Path.Combine(Path.GetDirectoryName(project.FullName), outputPath);
            if (Directory.Exists(directory))
            {
                System.Diagnostics.Process.Start(directory);
            }
        }
    }
}
