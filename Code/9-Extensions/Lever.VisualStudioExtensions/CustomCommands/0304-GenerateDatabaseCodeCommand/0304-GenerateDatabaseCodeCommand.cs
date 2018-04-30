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
            menuItem.Visible = this.IsSelectedSingleFileWithCustomTool("GenerateDatabaseCode");
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            this.EnsureNotDirty(projectItem.ContainingProject);
            GenerateDatabaseCodeLauncher launcher = GenerateDatabaseCodeLauncher.Create(projectItem);
            if (launcher == null)
            {
                return;
            }
            bool result = launcher.Launch();
            if (result)
            {
                this.ShowMessage("Success", "Operation Successfully.");
            }
        }
    }




}
