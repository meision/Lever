using System;
using System.Net.Sockets;
using Meision.Resources;

public static partial class Extensions
{
    /// <summary>
    /// Receive data by size from server.
    /// </summary>
    /// <param name="instance">socket instance</param>
    /// <param name="size">ensure receiving size</param>
    /// <returns>received data</returns>
    public static byte[] ReceiveBySize(this Socket instance, int size)
    {
        ThrowHelper.ArgumentMustNotNegative((size < 0), nameof(size));

        byte[] buffer = new byte[size];
        instance.ReceiveBySize(buffer, 0, size);
        return buffer;
    }

    /// <summary>
    /// Receive data by size from server.
    /// </summary>
    /// <param name="instance">socket instance</param>
    /// <param name="buffer">data array to receive</param>
    /// <param name="offset">offset in data</param>
    /// <param name="size">ensure receiving size</param>
    public static void ReceiveBySize(this Socket instance, byte[] buffer, int offset, int size, SocketFlags socketFlags = SocketFlags.None)
    {
        ThrowHelper.ArgumentNull((buffer == null), nameof(buffer));
        ThrowHelper.ArgumentIndexOutOfRange((offset < 0), nameof(offset));
        ThrowHelper.ArgumentLengthOutOfRange((size < 0), nameof(size));
        ThrowHelper.ArgumentIndexLengthOutOfArray(((offset + size) > buffer.Length));

        int remaining = size;
        while (remaining > 0)
        {
            int received = instance.Receive(buffer, offset + (size - remaining), remaining, socketFlags);
            if (received == 0)
            {
                throw new InvalidOperationException(SR.Socket_Exception_ReceiveEmtpy);
            }
            remaining -= received;
        }
    }

}

