using System.IO;

public static partial class Extensions
{
    public static byte[] Read(this Stream stream, int size)
    {
        ThrowHelper.ArgumentNull((stream == null), nameof(stream));
        byte[] buffer = new byte[size];
        int left = size;
        do
        {
            int received = stream.Read(buffer, size - left, left);
            if (received == 0)
            {
                throw new EndOfStreamException();
            }
            left -= received;
        }
        while (left > 0);
        return buffer;
    }

    public static void Write(this Stream stream, byte[] buffer)
    {
        ThrowHelper.ArgumentNull((stream == null), nameof(stream));
        ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));

        stream.Write(buffer, 0, buffer.Length);
    }
}