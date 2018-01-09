using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Meision.Net.Sockets
{
    public static class TcpClientExtensions
    {
        /// <summary>
        /// Receive data by size from server
        /// </summary>
        /// <param name="this">instance</param>
        /// <param name="data">data array to receive</param>
        /// <param name="offset">offset in data.</param>
        /// <param name="size">ensure receiving size.</param>
        public static void ReceiveBySize(this TcpClient @this, byte[] data, int offset, int size)
        {
            if (@this ==null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), Strings._Array_Index_OutOfRange);
            }
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), Strings._Array_Length_OutOfRange);
            }
            if ((offset + size) > data.Length)
            {
                throw new ArgumentException(Strings._Array_InvalidIndexLength);
            }

            int left = size;
            while (left > 0)
            {
                int received = @this.ReceiveCore(data, offset + (size - left), left, SocketFlags.None);
                if (received == 0)
                {
                    throw new InvalidOperationException(Strings.TcpClient_Exception_ReceiveEmtpy);
                }
                left -= received;
            }
        }
    }
}
