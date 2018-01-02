using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.VisualStudio
{
    public static class Helper
    {
        public static void ShowProperties(Properties properties)
        {
            foreach (Property property in properties)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine((string)property.Name + ":" + (string)property.Value);
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine((string)property.Name + ":?");
                }
            }
        }

        public static void ShowReferences(Project project)
        {
            VSLangProj.VSProject vsProject = project.Object as VSLangProj.VSProject;
            foreach (VSLangProj.Reference p in vsProject.References)
            {
                System.Diagnostics.Debug.WriteLine(p.Name);
            }
        }

        public static string GetNamespace(ProjectItem item)
        {
            string @namespace = (string)item.Properties.Item("CustomToolNamespace").Value;
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                @namespace = (string)item.Properties.Item("DefaultNamespace").Value;
            }
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                Project project = item.ContainingProject;
                string projectRootNamespace = (string)project.Properties.Item("RootNamespace").Value;
                string projectFolder = (string)project.Properties.Item("FullPath").Value;
                string itemFolder = Path.GetDirectoryName((string)item.Properties.Item("FullPath").Value);
                string subNamespace = itemFolder.Substring(projectFolder.Length).Trim('\\', '/').Replace('\\', '.').Replace('/', '.');
                @namespace = $"{projectRootNamespace}.{subNamespace}";
            }
            return @namespace;
        }
    }
}
