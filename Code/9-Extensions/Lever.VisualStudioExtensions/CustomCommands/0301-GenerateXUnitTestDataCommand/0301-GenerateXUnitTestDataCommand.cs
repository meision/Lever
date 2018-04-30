using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using EnvDTE;
using Meision.VisualStudio.CustomTools;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class GenerateXUnitTestDataCommand : CustomCommand
    {
        public GenerateXUnitTestDataCommand()
        {
            this.CommandId = 0x0301;
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = this.IsSelectedSingleFileWithCustomTool("GenerateXUnitTestData");
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            this.EnsureNotDirty(projectItem.ContainingProject);
            GenerateXUnitTestDataLauncher generator = new GenerateXUnitTestDataLauncher(projectItem);
            generator.Launch();
        }


    }
}
