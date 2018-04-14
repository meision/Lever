using System;
using System.IO;
using System.Reflection;

public static partial class Extensions
{
    public static byte[] ReadResourceFile(this Assembly assembly, Type type, string resource)
    {
        ThrowHelper.ArgumentNull((assembly == null), nameof(assembly));
        ThrowHelper.ArgumentNull((type == null), nameof(type));
        ThrowHelper.ArgumentNull((resource == null), nameof(resource));

        using (Stream stream = assembly.GetManifestResourceStream(type, resource))
        {
            if (stream == null)
            {
                return null;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return buffer;
        }
    }

    public static byte[] ReadResourceFile(this Assembly assembly, string resource)
    {
        ThrowHelper.ArgumentNull((assembly == null), nameof(assembly));
        ThrowHelper.ArgumentNull((resource == null), nameof(resource));

        using (Stream stream = assembly.GetManifestResourceStream(resource))
        {
            if (stream == null)
            {
                return null;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return buffer;
        }
    }

}
