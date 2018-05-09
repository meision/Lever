using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class SyncDatabaseCommand : CustomCommand
    {

        public SyncDatabaseCommand()
        {
            this.CommandId = 0x0303;
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            if (this.DTE.SelectedItems.Count != 1)
            {
                menuItem.Visible = false;
                return;
            }
            if (this.DTE.SelectedItems.Item(1).ProjectItem.ContainingProject.Kind.Equals(Parameters.guidSQLServerDatabaseProject, System.StringComparison.OrdinalIgnoreCase))
            {
                menuItem.Visible = true;
                return;
            }

            menuItem.Visible = this.IsSelectedSingleFileWithCustomTool("SyncDatabase");            
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            this.EnsureNotDirty(projectItem.ContainingProject);
            SyncDatabaseLauncher generator = new SyncDatabaseLauncher(projectItem);
            generator.Launch();
            projectItem.ContainingProject.Save();
        }
    }
}
