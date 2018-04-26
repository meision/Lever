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
        public static void AddDependentFromFiles(this ProjectItems instance, params string[] files)
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
                ProjectItem projectItem = instance.Parent as ProjectItem;
                string projectItemIdentity = (string)projectItem.Properties.Item("Identity").Value;
                //projectItem.Properties.Print();
                Project project = projectItem.ContainingProject;
                string projectPath = System.IO.Path.Combine((string)project.Properties.Item("LocalPath").Value, (string)project.Properties.Item("FileName").Value);
                // Filter files in same directory.
                string directory = Path.GetDirectoryName((string)projectItem.Properties.Item("LocalPath").Value);
                IEnumerable<string> validFiles = files.Where(p => directory.Equals(Path.GetDirectoryName(p), StringComparison.OrdinalIgnoreCase));

                // Parse XML from project file
                XDocument document = XDocument.Load(projectPath);
                // Element
                XElement element = document.XPathSelectElements($"//ItemGroup/Compile/DependentUpon[text()='{projectItemIdentity}']").FirstOrDefault();
                if (element == null)
                {
                    XElement eItemGroup = XElement.Parse($"<ItemGroup><Compile Update=\"\"><DesignTime>True</DesignTime><AutoGen>True</AutoGen><DependentUpon>{projectItemIdentity}</DependentUpon></Compile></ItemGroup>");
                    document.Root.Add(eItemGroup);
                    element = document.XPathSelectElements($"//ItemGroup/Compile/DependentUpon[text()='{projectItemIdentity}']").FirstOrDefault();
                }
                XElement eOwner = element.Parent;

                List<string> updates = new List<string>();
                // Add old updates.
                string value = eOwner.Attribute("Update")?.Value;
                if (value != null)
                {
                    updates.AddRange(value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()));
                }
                // Merge new updates
                string basePath = Path.GetDirectoryName(projectItemIdentity);
                foreach (string file in validFiles)
                {
                    string item = $"{basePath}\\{Path.GetFileName(file)}";
                    if (!updates.Any(p => item.Equals(p, StringComparison.OrdinalIgnoreCase)))
                    {
                        updates.Add(item);
                    }
                }
                if (eOwner.Attribute("Update") == null)
                {
                    eOwner.Add(new XAttribute("Update", ""));
                }
                eOwner.Attribute("Update").Value = string.Join(";", updates);

                document.Save(projectPath);
            }
            else
            {
                foreach (string file in files)
                {
                    instance.AddFromFile(file);
                }
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
   
