using EnvDTE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Meision.VisualStudio
{
    public static class Extensions
    {
        public static void AddDependentItems(this ProjectItem instance, params ProjectItem[] items)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach(ProjectItem item in items)
            {
                item.Properties.Item("DesignTime").Value = "True";
                item.Properties.Item("AutoGen").Value = "True";
                item.Properties.Item("DependentUpon").Value = instance.Name;
            }
        }

        //public static void AddDependentFromFiles(this ProjectItem instance, params string[] files)
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException(nameof(instance));
        //    }
        //    if (files == null)
        //    {
        //        throw new ArgumentNullException(nameof(files));
        //    }

        //    //if (instance.ContainingProject.Kind.Equals(Parameters.guidDotNetCoreProject, StringComparison.OrdinalIgnoreCase))
        //    string targetFrameworkMoniker = instance.ContainingProject.Properties.Item("TargetFrameworkMoniker")?.Value;
        //    if ((targetFrameworkMoniker != null) && (targetFrameworkMoniker.IndexOf(".NETCore") >= 0))
        //    {
        //        ProjectItem projectItem = instance;
        //        string projectItemIdentity = (string)projectItem.Properties.Item("Identity").Value;
        //        string projectItemName = (string)projectItem.Properties.Item("FileName").Value;
        //        Project project = projectItem.ContainingProject;
        //        string projectPath = System.IO.Path.Combine((string)project.Properties.Item("LocalPath").Value, (string)project.Properties.Item("FileName").Value);
        //        // Filter files in same directory.
        //        string directory = Path.GetDirectoryName((string)projectItem.Properties.Item("LocalPath").Value);
        //        IEnumerable<string> validFiles = files.Where(p => directory.Equals(Path.GetDirectoryName(p), StringComparison.OrdinalIgnoreCase));

        //        XDocument document = XDocument.Load(projectPath);
        //        // Item Group
        //        XElement eItemGroup = document.XPathSelectElements($"//ItemGroup[@Label='DependentUpon:{projectItemIdentity}']").FirstOrDefault();
        //        if (eItemGroup == null)
        //        {
        //            eItemGroup = XElement.Parse($"<ItemGroup Label='DependentUpon:{projectItemIdentity}'></ItemGroup>");
        //            document.Root.Add(eItemGroup);
        //        }

        //        // Merge new updates
        //        string baseIdentity = Path.GetDirectoryName(projectItemIdentity);
        //        if (!string.IsNullOrEmpty(baseIdentity))
        //        {
        //            baseIdentity += "\\";
        //        }
        //        // Parse XML from project file
        //        foreach (string validFile in validFiles)
        //        {
        //            string name = Path.GetFileName(validFile);
        //            string itemIdentity = $"{baseIdentity}{name}";
        //            //ProjectItem pFile = projectItem.Collection.Item(name);
        //            //if (pFile == null)
        //            //{
        //            //    continue;
        //            //}
        //            //string itemType = pFile.Properties.Item("ItemType").Value;
        //            string itemType = null;
        //            string extension = System.IO.Path.GetExtension(validFile);
        //            if (extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
        //            {
        //                itemType = "Compile";
        //            }
        //            else
        //            {
        //                itemType = "None";
        //            }

        //            // Remove all exists update element
        //            foreach (XElement element in eItemGroup.XPathSelectElements($"*[@Update='{itemIdentity}']"))
        //            {
        //                element.Remove();
        //            }

        //            XElement eItem = XElement.Parse($"<{itemType} Update=\"{itemIdentity}\"><DesignTime>True</DesignTime><AutoGen>True</AutoGen><DependentUpon>{projectItemName}</DependentUpon></{itemType}>");
        //            eItemGroup.Add(eItem);
        //        }

        //        //project.Save();
        //        //project.IsDirty = false;
        //        document.Save(projectPath);
        //        //project.DTE.ExecuteCommand("Project.ReloadProject", "");
        //    }
        //    else
        //    {
        //        foreach (string file in files)
        //        {
        //            instance.ProjectItems.AddFromFile(file);
        //        }
        //    }
        //}

        public static string[] GetDependents(this ProjectItem instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            // .net core project same with no
            string[] items = new string[instance.ProjectItems.Count];
            for (int i = 1; i <= items.Length; i++)
            {
                items[i - 1] = (string)instance.ProjectItems.Item(i).Properties.Item("LocalPath").Value;
            }
            return items;
        }

        public static void DeleteDependentFiles(this ProjectItem instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            //if (instance.ContainingProject.Kind.Equals(Parameters.guidDotNetCoreProject, StringComparison.OrdinalIgnoreCase))
            string targetFrameworkMoniker = instance.ContainingProject.Properties.Item("TargetFrameworkMoniker")?.Value;
            if ((targetFrameworkMoniker != null) && (targetFrameworkMoniker.IndexOf(".NETCore") >= 0))
            {
                for (int i = instance.ProjectItems.Count; i >= 1; i--)
                {
                    System.IO.File.Delete(instance.ProjectItems.Item(i).GetFullPath());
                }

                ProjectItem projectItem = instance;
                string projectItemIdentity = (string)projectItem.Properties.Item("Identity").Value;
                string projectItemName = (string)projectItem.Properties.Item("FileName").Value;
                Project project = projectItem.ContainingProject;
                string projectPath = System.IO.Path.Combine((string)project.Properties.Item("LocalPath").Value, (string)project.Properties.Item("FileName").Value);

                XDocument document = XDocument.Load(projectPath);
                // Item Group
                XElement eItemGroup = document.XPathSelectElements($"//ItemGroup[@Label='DependentUpon:{projectItemIdentity}']").FirstOrDefault();
                if (eItemGroup != null)
                {
                    eItemGroup.Remove();
                    document.Save(projectPath);
                }
            }
            else
            {
                for (int i = instance.ProjectItems.Count; i >= 1; i--)
                {
                    instance.ProjectItems.Item(i).Delete();
                }
            }
        }

        public static string GetFullPath(this ProjectItem instance)
        {
            string fullPath = (string)instance.Properties.Item("FullPath").Value;
            return fullPath;
        }

        public static bool IsCSharpProject(this ProjectItem instance)
        {
            bool result =
                instance.ContainingProject.Kind.Equals(Parameters.guidCSharpProject, StringComparison.OrdinalIgnoreCase)
             || instance.ContainingProject.Kind.Equals(Parameters.guidCSharpProject2, StringComparison.OrdinalIgnoreCase)
             || instance.ContainingProject.Kind.Equals(Parameters.guidDotNetCoreProject, StringComparison.OrdinalIgnoreCase);
            return result;
        }
    }
}