using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace Meision.VisualStudio.CustomCommands
{
    public abstract class Launcher
    {
        public ProjectItem ProjectItem { get; }
        public string InputFilePath { get; }
        public string CodeNamespace { get; }

        public Launcher(ProjectItem projectItem)
        {
            if (projectItem == null)
            {
                throw new ArgumentNullException(nameof(projectItem));
            }

            this.ProjectItem = projectItem;
            this.InputFilePath = (string)projectItem.Properties.Item("FullPath").Value;
            this.CodeNamespace = Helper.GetNamespace(this.ProjectItem);
        }

        public abstract void Launch();

        protected string GetOutputFilePathByExtension(string extensions)
        {
            string directory = Path.GetDirectoryName(this.InputFilePath);
            string mainName = Path.GetFileNameWithoutExtension(this.InputFilePath);
            return Path.Combine(directory, $"{mainName}.{extensions.TrimStart('.')}");
        }

        protected string GetOutputFilePathByFileName(string fileName)
        {
            string directory = Path.GetDirectoryName(this.InputFilePath);
            return Path.Combine(directory, fileName);
        }
    }
}
