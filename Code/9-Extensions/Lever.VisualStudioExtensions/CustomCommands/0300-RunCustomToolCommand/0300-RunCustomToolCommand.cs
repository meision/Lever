﻿ using System;
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
using Meision.VisualStudio.CustomTools;

namespace Meision.VisualStudio.CustomCommands
{
    internal abstract class RunCustomToolCommand<TCustomTool> : CustomCommand where TCustomTool : BaseCodeGeneratorWithSite, new()
    {
        public RunCustomToolCommand()
        {
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = IsSelectedSingleFileWithCustomTool(typeof(TCustomTool).Name);            
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string fullPath = (string)projectItem.Properties.Item("FullPath").Value;

            TCustomTool generator = new TCustomTool();
            generator.CodeNamespace = Helper.GetNamespace(projectItem);
            generator.InputFilePath = fullPath;
            byte[] data = generator.GenerateDataFromProjectItem(projectItem);
            
            string csPath = Path.Combine(Path.GetDirectoryName(fullPath), Path.GetFileNameWithoutExtension(fullPath) + ".cs");
            File.WriteAllBytes(csPath, data);
        }
    }
}
