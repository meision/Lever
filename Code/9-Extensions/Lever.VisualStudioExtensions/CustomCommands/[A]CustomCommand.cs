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
using Meision.VisualStudio.CustomTools;
using Microsoft.VisualStudio;

namespace Meision.VisualStudio.CustomCommands
{
    internal abstract class CustomCommand
    {
        public static readonly Guid GuidLeverCmdSet = new Guid(Parameters.guidLeverCmdSet);

        public Guid CommandGuid
        {
            get
            {
                return CustomCommand.GuidLeverCmdSet;
            }
        }

        private int _commandId;
        public int CommandId
        {
            get
            {
                return this._commandId;
            }
            internal set
            {
                this._commandId = value;
            }
        }

        private Package _package;
        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        protected IServiceProvider ServiceProvider
        {
            get
            {
                return this._package;
            }
        }
        private DTE _dte;
        protected DTE DTE
        {
            get
            {
                return this._dte;
            }
        }

        public CustomCommand()
        {
        }

        internal void Initialize(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }
            this._package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                CommandID menuCommandID = new CommandID(this.CommandGuid, this.CommandId);
                OleMenuCommand menuItem = new OleMenuCommand(this.MenuItem_Invoke, this.MenuItem_Change, this.MenuItem_BeforeQueryStatus, menuCommandID);
                commandService.AddCommand(menuItem);
            }

            this._dte = (DTE)this.ServiceProvider.GetService(typeof(DTE));
        }

        private void MenuItem_Invoke(object sender, EventArgs e)
        {
            OleMenuCommand menuItem = (OleMenuCommand)sender;
            this.PerformMenuItemInvoke(menuItem);
        }
        protected abstract void PerformMenuItemInvoke(OleMenuCommand menuItem);

        private void MenuItem_Change(object sender, EventArgs e)
        {
            OleMenuCommand menuItem = (OleMenuCommand)sender;
            this.PerformMenuItemChange(menuItem);
        }
        protected virtual void PerformMenuItemChange(OleMenuCommand menuItem)
        {
        }

        private void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand menuItem = (OleMenuCommand)sender;
            this.PerformMenuItemBeforeQueryStatus(menuItem);
        }
        protected virtual void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
        }

        protected void ShowMessage(string title, string message)
        {
            IVsUIShell shell = (IVsUIShell)this.ServiceProvider.GetService(typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result = VSConstants.S_OK;
            int hr = shell.ShowMessageBox(0,
                ref clsid,
                title,
                message,
                null,
                0,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                OLEMSGICON.OLEMSGICON_INFO,
                0,
                out result);
        }

    }
}
