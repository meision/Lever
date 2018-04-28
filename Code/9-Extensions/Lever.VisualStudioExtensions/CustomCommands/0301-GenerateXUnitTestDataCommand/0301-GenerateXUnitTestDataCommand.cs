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
            menuItem.Visible = this.IsSelectedSingleFileWithCustomTool("XUnitTestData");
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            GenerateXUnitTestDataLauncher generator = new GenerateXUnitTestDataLauncher(this.DTE.SelectedItems.Item(1).ProjectItem);
            generator.Launch();
        }


    }
}
