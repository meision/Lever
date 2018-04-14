using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Meision
{
    public static class Machine
    {

        public static PhysicalAddress[] GetMacAddresses()
        {
            List<PhysicalAddress> addresses = new List<PhysicalAddress>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    PhysicalAddress address = nic.GetPhysicalAddress();
                    addresses.Add(address);
                }
            }
            return addresses.ToArray();
        }
    }
}
