using EnvDTE;
using Meision.VisualStudio.CustomTools;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class GenerateLanguagesCommand : CustomCommand
    {
        public GenerateLanguagesCommand()
        {
            this.CommandId = 0x0302;
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = this.IsSelectedSingleFileWithCustomTool("GenerateLanguages");
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            this.EnsureNotDirty(projectItem.ContainingProject);
            GenerateLanguagesLauncher generator = new GenerateLanguagesLauncher(projectItem);
            generator.Launch();
            projectItem.ContainingProject.Save();
        }
    }
}
