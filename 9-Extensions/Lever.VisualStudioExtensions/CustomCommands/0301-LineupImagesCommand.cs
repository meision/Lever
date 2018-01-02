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
using System.Drawing;

namespace Meision.VisualStudio.CustomCommands
{
    internal sealed class LineupImagesCommand : CustomCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenContainingFolderCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public LineupImagesCommand()
        {
            this.CommandId = 0x0301;
        }

        protected override void PerformMenuItemInvoke(OleMenuCommand menuItem)
        {
            ProjectItem projectItem = this.DTE.SelectedItems.Item(1).ProjectItem;
            string directory = ((string)projectItem.Properties.Item("FullPath").Value).TrimEnd('\\');
            byte[] data = this.LineupImages(directory);
            if (data == null)
            {
                VsShellUtilities.ShowMessageBox(
                    this.ServiceProvider,
                    "No PNG file found under selected folder.",
                    null,
                    OLEMSGICON.OLEMSGICON_WARNING,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            
            string pngPath = Path.Combine(Path.GetDirectoryName(directory), Path.GetFileNameWithoutExtension(directory) + ".png");
            File.WriteAllBytes(pngPath, data);
            projectItem.Collection.AddFromFile(pngPath);
        }

        protected override void PerformMenuItemBeforeQueryStatus(OleMenuCommand menuItem)
        {
            menuItem.Visible = false;

            if (this.DTE.SelectedItems.Count != 1)
            {
                return;
            }

            menuItem.Visible = true;
        }

        private byte[] LineupImages(string directory)
        {
            var files = Directory.GetFiles(directory, "*.png", SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
            {
                return null;
            }

            int width = 0;
            int height = 0;
            Bitmap[] bitmaps = new Bitmap[files.Length];
            for (int i = 0; i < bitmaps.Length; i++)
            {
                string file = files[i];
                bitmaps[i] = new Bitmap(file);
                width += bitmaps[i].Width;
                height = Math.Max(height, bitmaps[i].Height);
            }

            using (Bitmap image = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(image))
            using (MemoryStream stream = new MemoryStream())
            {
                int x = 0;
                foreach (Bitmap b in bitmaps)
                {
                    g.DrawImage(b, x, 0);
                    x += b.Width;
                }
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
