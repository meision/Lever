using System;
using System.IO;
using System.Text.RegularExpressions;
using Meision.Resources;
using Meision.Text;

namespace Meision.IO
{
    public static class PathManager
    {
        public const string RootPath = "\\";
        public const char DirectorySeparator = '\\';

        private const string NewFileFormat = "{0} ({1}){2}";

        public static readonly string RegularExpression =
            @"^(?<Drive>[a-zA-Z]:)((\\)|((\\[^\\\/\:\*\?\""\<\>\|\s]([^\\\/\:\*\?\""\<\>\|\t]*[^\\\/\:\*\?\""\<\>\|\s])?)*\\(?<Name>[^\\\/\:\*\?\""\<\>\|\s]([^\\\/\:\*\?\""\<\>\|\t]*[^\\\/\:\*\?\""\<\>\|\s])?)))$";

        public static bool IsAbsoluteLocalPhysicalPath(string path)
        {
            if (path == null)
            {
                return false;
            }
            if (path.Length < 3)
            {
                return false;
            }

            if ((((path[0] >= 'a') && (path[0] <= 'z')) || ((path[0] >= 'A') && (path[0] <= 'Z')))
             && (path[1] == ':')
             && (path[2] == PathManager.DirectorySeparator))
            {
                return true;
            }

            return false;
        }

        public static bool IsFileSystem(string value)
        {
            ThrowHelper.ArgumentNull((value == null), nameof(value));

            return Regex.IsMatch(value, RegexConstant.GetFullRegularExpression(RegularExpression));
        }



        ///// <summary>
        ///// Get path related to the assembly where type location.
        ///// </summary>
        ///// <param name="type">Type name</param>
        ///// <param name="filename">Filename</param>
        ///// <returns></returns>
        //public static string GetPathInAssemblyFolder(Type type, string filename)
        //{
        //    if (type == null)
        //    {
        //        throw new ArgumentNullException(nameof(type));
        //    }
        //    if (filename == null)
        //    {
        //        throw new ArgumentNullException(nameof(filename));
        //    }

        //    return Path.GetPath(type.Assembly, filename);
        //}

        ///// <summary>
        ///// Get path related to the assembly
        ///// </summary>
        ///// <param name="assembly">Assembly</param>
        ///// <param name="filename">Filename</param>
        ///// <returns></returns>
        //public static string GetPath(Assembly assembly, string filename)
        //{
        //    if (assembly == null)
        //    {
        //        throw new ArgumentNullException(nameof(assembly));
        //    }
        //    if (filename == null)
        //    {
        //        throw new ArgumentNullException(nameof(filename));
        //    }

        //    string folder = System.IO.Path.GetDirectoryName(assembly.Location);
        //    return Path.Combine(folder, filename);
        //}

        public static string GetUsableFilename(DirectoryInfo directory, string filename)
        {
            return GetUsableFilename(directory.ToString(), filename);
        }

        public static string GetUsableFilename(string directory, string filename)
        {
            ThrowHelper.ArgumentNull((directory == null), nameof(directory));
            if (!System.IO.Directory.Exists(directory))
            {
                throw new System.IO.DirectoryNotFoundException();
            }
            ThrowHelper.ArgumentNull((filename == null), nameof(filename));

            string path = Path.Combine(directory, filename);
            if (!System.IO.File.Exists(path))
            {
                return filename;
            }

            int slashPosition = path.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
            int extensionPosition = path.IndexOf(Meision.IO.FileManager.FileExtension, slashPosition);

            if (extensionPosition != -1)
            {
                string left = path.Substring(0, extensionPosition);
                string right = path.Substring(extensionPosition);

                for (int i = 1; i < int.MaxValue; i++)
                {
                    string newPath = string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        NewFileFormat,
                        left,
                        i.ToString(),
                        right);

                    if (!System.IO.File.Exists(newPath))
                    {
                        return System.IO.Path.GetFileName(newPath);
                    }
                }
            }
            else
            {
                for (int i = 1; i < int.MaxValue; i++)
                {
                    string newPath = string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        NewFileFormat,
                        path,
                        i.ToString(),
                        string.Empty);

                    if (!System.IO.File.Exists(newPath))
                    {
                        return System.IO.Path.GetFileName(newPath);
                    }
                }
            }

            return null;
        }

        public static long GetSize(string path)
        {
            return GetSize(path, SearchOption.AllDirectories);
        }

        public static long GetSize(string path, SearchOption option)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((path == null), nameof(path));
            ThrowHelper.ArgumentException((!PathManager.IsFileSystem(path)), SR.Path_Invalid);

            if (System.IO.File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                return file.Length;
            }
            else if (System.IO.Directory.Exists(path))
            {
                return DirectoryManager.GetTotalSize(path, option);
            }
            else
            {
                throw new ArgumentException(SR.Path_Invalid);
            }
        }
    }
}
