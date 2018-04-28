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
        public static void AddDependentFromFiles(this ProjectItem instance, params string[] files)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (files == null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            if (instance.ContainingProject.Kind == Parameters.guidDotNetCoreProject)
            {
                ProjectItem projectItem = instance;
                string projectItemIdentity = (string)projectItem.Properties.Item("Identity").Value;
                string projectItemName = (string)projectItem.Properties.Item("FileName").Value;
                projectItem.Properties.Print();
                Project project = projectItem.ContainingProject;
                string projectPath = System.IO.Path.Combine((string)project.Properties.Item("LocalPath").Value, (string)project.Properties.Item("FileName").Value);
                // Filter files in same directory.
                string directory = Path.GetDirectoryName((string)projectItem.Properties.Item("LocalPath").Value);
                IEnumerable<string> validFiles = files.Where(p => directory.Equals(Path.GetDirectoryName(p), StringComparison.OrdinalIgnoreCase));

                XDocument document = XDocument.Load(projectPath);
                // Item Group
                XElement eItemGroup = document.XPathSelectElements($"//ItemGroup[@Label='DependentUpon:{projectItemIdentity}']").FirstOrDefault();
                if (eItemGroup == null)
                {
                    eItemGroup = XElement.Parse($"<ItemGroup Label='DependentUpon:{projectItemIdentity}'></ItemGroup>");
                    document.Root.Add(eItemGroup);
                }

                // Merge new updates
                string baseIdentity = Path.GetDirectoryName(projectItemIdentity);
                if (!string.IsNullOrEmpty(baseIdentity))
                {
                    baseIdentity += "\\";
                }
                // Parse XML from project file
                foreach (string validFile in validFiles)
                {
                    string name = Path.GetFileName(validFile);
                    string itemIdentity = $"{baseIdentity}{name}";
                    ProjectItem pFile = projectItem.Collection.Item(name);
                    if (pFile == null)
                    {
                        continue;
                    }

                    // Remove all exists update element
                    foreach (XElement element in eItemGroup.XPathSelectElements($"*[@Update='{itemIdentity}']"))
                    {
                        element.Remove();
                    }

                    XElement eItem = XElement.Parse($"<{pFile.Properties.Item("ItemType").Value} Update=\"{itemIdentity}\"><DesignTime>True</DesignTime><AutoGen>True</AutoGen><DependentUpon>{projectItemName}</DependentUpon></{pFile.Properties.Item("ItemType").Value}>");
                    eItemGroup.Add(eItem);
                }

                document.Save(projectPath);
            }
            else
            {
                foreach (string file in files)
                {
                    instance.ProjectItems.AddFromFile(file);
                }
            }
        }

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

            // .net core project same with no
            for (int i = instance.ProjectItems.Count; i >= 1; i--)
            {
                instance.ProjectItems.Item(i).Delete();
            }
        }



            internal static void Print(this Properties properties)
        {
            foreach (Property item in properties)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"{item.Name}: {item.Value}");
                }
                catch
                {

                }
            }
        }
    }
}

        /*
          <ItemGroup>
    <Compile Update="NewFolder\Class1.cs;NewFolder\Class2.cs;NewFolder\Class3.cs">
      <DesignTime>True</DesignTime>
      <DependentUpon>Resource1.resx</DependentUpon>
      <AutoGen>True</AutoGen>
    </Compile>
  </ItemGroup>*/
   
