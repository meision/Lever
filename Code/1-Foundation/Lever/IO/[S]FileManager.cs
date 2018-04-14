using System;
using System.IO;
using System.Reflection;
using System.Text;
using Meision.Text;

namespace Meision.IO
{
    public static class FileManager
    {
        public const string FileExtension = StringConstant.FileExtension;
        public const string Wildcard_Single = StringConstant.Wildcard_Single;
        public const string Wildcard_Multiple = StringConstant.Wildcard_Multiple;
        
        /// <summary>
        /// Get the specified file from the directory.
        /// </summary>
        /// <param name="directory">Directory instance</param>
        /// <param name="fileName">File name</param>
        /// <returns>If file exist, return file, or else return null.</returns>
        public static FileInfo GetFile(DirectoryInfo directory, string fileName)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((directory == null), nameof(directory));

            FileInfo[] files = directory.GetFiles(fileName);
            if (files.Length != 0)
            {
                return files[0];
            }
            else
            {
                return null;
            }
        }
        
        public static string ReadTextFile(string path)
        {
            ThrowHelper.ArgumentNull((path == null), nameof(path));

            return ReadTextFile(new FileInfo(path));
        }

        public static string ReadTextFile(FileInfo file)
        {
            return ReadTextFile(file, Encoding.Default);
        }

        public static string ReadTextFile(FileInfo file, Encoding encoding)
        {
            // Validate parameter(s).
            ThrowHelper.ArgumentNull((file == null), nameof(file));
            ThrowHelper.ArgumentNull((encoding == null), nameof(encoding));

            System.IO.FileStream stream = file.Open(FileMode.Open, FileAccess.Read);
            int length = Convert.ToInt32(file.Length);
            byte[] buffer = new byte[length];
            stream.Read(buffer, 0, length);
            stream.Close();

            // Convert to text
            string text = encoding.GetString(buffer);
            return text;
        }

        public static string ReadTextFromAssembly(Assembly assembly, string path)
        {
            return FileManager.ReadTextFromAssembly(assembly, path, Encoding.Default);
        }

        public static string ReadTextFromAssembly(Assembly assembly, string path, Encoding encoding)
        {
            using (Stream stream = assembly.GetManifestResourceStream(path))
            {
                int length = Convert.ToInt32(stream.Length);
                byte[] buffer = new byte[length];
                stream.Read(buffer, 0, length);

                // Convert to text
                string text = encoding.GetString(buffer);
                return text;
            }
        }

        public static void CopyToDirectory(string path, string directory)
        {
            ThrowHelper.ArgumentNull((path == null), nameof(path));
            ThrowHelper.ArgumentNull((directory == null), nameof(directory));

            string file = System.IO.Path.GetFileName(path);
            System.IO.File.Copy(path, System.IO.Path.Combine(directory, file));
        }

        public static void MoveToDirectory(string path, string directory)
        {
            ThrowHelper.ArgumentNull((path == null), nameof(path));
            ThrowHelper.ArgumentNull((directory == null), nameof(directory));

            string file = System.IO.Path.GetFileName(path);
            System.IO.File.Move(path, System.IO.Path.Combine(directory, file));
        }

        public static void ReplaceContent(string path, string oldValue, string newValue)
        {
            FileManager.ReplaceContent(path, oldValue, newValue, Encoding.UTF8);
        }

        public static void ReplaceContent(string path, string oldValue, string newValue, Encoding encoding)
        {
            ThrowHelper.ArgumentNull((path == null), nameof(path));
            ThrowHelper.ArgumentNull((oldValue == null), nameof(oldValue));
            ThrowHelper.ArgumentNull((newValue == null), nameof(newValue));

            string text = System.IO.File.ReadAllText(path, encoding);
            text = text.Replace(oldValue, newValue);
            System.IO.File.WriteAllText(path, text, encoding);
        }
        
    }
}
