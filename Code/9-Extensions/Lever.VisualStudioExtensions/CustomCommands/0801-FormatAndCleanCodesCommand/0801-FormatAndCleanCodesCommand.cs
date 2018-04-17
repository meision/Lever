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
    internal sealed class FormatAndCleanCodesCommand : CustomCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatAndCleanCodesCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public FormatAndCleanCodesCommand()
        {
            this.CommandId = 0x0801;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            if (!this.Prompt())
            {
                return;
            }

            SelectedItem selectedItem = this.DTE.SelectedItems.Item(1);
            if (selectedItem.Project != null)
            {
                this.FormatAndCleanCodes(selectedItem.Project.ProjectItems);
            }
            else if (selectedItem.DTE != null)
            {
                foreach (Project project in selectedItem.DTE.Solution.Projects)
                {
                    this.FormatAndCleanCodes(project.ProjectItems);
                }
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

        private bool Prompt()
        {
            IVsUIShell uiShell = this.ServiceProvider.GetService(typeof(SVsUIShell)) as IVsUIShell;
            bool result = VsShellUtilities.PromptYesNo(
                "This operation will \"Format Document\" and \"Remove and Sort Usings\" for .cs files and can not be undone.\r\nBefore click \"yes\", please make sure you have checked in codes and would be able to restore from source control.",
                null,
                OLEMSGICON.OLEMSGICON_QUERY,
                uiShell);
            return result;
        }

        private void FormatAndCleanCodes(ProjectItems projectItems)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                if ((projectItem.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                 && projectItem.Name.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase))
                {
                    // cs code file
                    bool alreadyOpen = projectItem.IsOpen;
                    Window window = projectItem.Open(EnvDTE.Constants.vsViewKindCode);
                    window.Activate();
                    window.DTE.ExecuteCommand("Edit.RemoveAndSort");
                    window.DTE.ExecuteCommand("Edit.FormatDocument");
                    if (!alreadyOpen)
                    {
                        window.Close(vsSaveChanges.vsSaveChangesYes);
                    }
                }
                // sub items
                if (projectItem.ProjectItems != null)
                {
                    this.FormatAndCleanCodes(projectItem.ProjectItems);
                }
                // sub project
                if (projectItem.SubProject != null)
                {
                    this.FormatAndCleanCodes(projectItem.SubProject.ProjectItems);
                }

            }
        }


    }
}
