using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using Meision.Resources;

namespace Meision.Net.Sockets
{
    public static class SocketExtensions
    {
        /// <summary>
        /// Receive data by size from server
        /// </summary>
        /// <param name="this">instance</param>
        /// <param name="buffer">data array to receive</param>
        /// <param name="offset">offset in data.</param>
        /// <param name="size">ensure receiving size.</param>
        public static void ReceiveBySize(this Socket @this, byte[] buffer, int offset, int size, SocketFlags socketFlags = SocketFlags.None)
        {
            if (@this ==null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), SR._Exception_IndexOutOfRange);
            }
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), SR._Exception_LengthOutOfRange);
            }
            if ((offset + size) > buffer.Length)
            {
                throw new ArgumentException(SR._Exception_InvalidIndexLength);
            }

            int left = size;
            while (left > 0)
            {
                int received = @this.Receive(buffer, offset + (size - left), left, SocketFlags.None);
                if (received == 0)
                {
                    throw new InvalidOperationException(SR.Socket_Exception_ReceiveEmtpy);
                }
                left -= received;
            }
        }
    }
}
