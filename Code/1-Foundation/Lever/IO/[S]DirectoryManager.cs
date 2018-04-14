using System;
using System.IO;

namespace Meision.IO
{
    public static class DirectoryManager
    {
        public const string CurrentFolderName = ".";
        public const string ParentFolderName = "..";

        // File system.
        public const string DiskExpression = @"^[a-zA-Z]:";
        public const string FileSystemExpression = @"^[a-zA-Z]\:[\\\/]([^\\\/\:\*\?\""\<\>\|\r\n\t]+[\\\/])*([^\\\/\:\*\?\""\<\>\|\r\n\t]+[\\\/]?)?$";
        
        public static string GetApplicationDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static long GetTotalSize(string directory, SearchOption option)
        {
            ThrowHelper.ArgumentNull((directory == null), nameof(directory));

            string[] files = System.IO.Directory.GetFiles(directory, FileManager.Wildcard_Multiple.ToString(), option);
            if (files.Length == 0)
            {
                return 0;
            }

            long size = 0;
            foreach (string file in files)
            {
                size += new FileInfo(file).Length;
            }
            return size;
        }
        
        public static void CopyDirectory(string source, string destincation, bool copySubDirectories)
        {
            DirectoryInfo src = new DirectoryInfo(source);
            DirectoryInfo dst = new DirectoryInfo(destincation);
            CopyDirectory(src, dst, copySubDirectories);
        }

        /// <summary>
        /// Copy Directory
        /// </summary>
        /// <param name="source">source directory</param>
        /// <param name="destination">destination directory</param>
        /// <param name="copySubDirectories">indicator whether copy sub directories or not.</param>
        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination, bool copySubDirectories)
        {
            // Create destination directory.
            if (!destination.Exists)
            {
                destination.Create();
            }
            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
            }
            // Copy sub directories
            if (copySubDirectories)
            {
                DirectoryInfo[] dirs = source.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    string destinationDir = Path.Combine(destination.FullName, dir.Name);
                    CopyDirectory(dir, new DirectoryInfo(destinationDir), true);
                }
            }
        }


        /// <summary>
        /// Get the sub Directory which exists in speified folder.
        /// </summary>
        /// <param name="directory">Directory instance</param>
        /// <param name="subdirectoryName">Sub directory name</param>
        /// <returns>The sub folder directory instance. if not exist, return null.</returns>
        public static DirectoryInfo GetSubdirectory(DirectoryInfo directory, string subdirectoryName)
        {
            // Validate argument.
            ThrowHelper.ArgumentNull((directory == null), nameof(directory));
            ThrowHelper.ArgumentNull((subdirectoryName == null), nameof(subdirectoryName));

            // Get Sub Directories.
            DirectoryInfo[] directories = directory.GetDirectories(subdirectoryName, SearchOption.TopDirectoryOnly);
            if (directories.Length > 0)
            {
                return directories[0];
            }
            else
            {
                string path = Path.Combine(directory.FullName, subdirectoryName);
                return new DirectoryInfo(path);
            }
        }        
    }

}
