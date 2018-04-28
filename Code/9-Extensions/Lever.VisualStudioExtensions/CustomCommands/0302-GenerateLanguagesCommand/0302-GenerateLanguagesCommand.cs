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
            GenerateLanguagesLauncher generator = new GenerateLanguagesLauncher(this.DTE.SelectedItems.Item(1).ProjectItem);
            generator.Launch();
        }
    }
}
