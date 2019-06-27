using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;

namespace HideHyperV
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="https://brianreiter.org/2011/08/29/hide-vmware-virtual-network-interfaces-from-windows-firewall-and-network-and-sharing-center/"/>
    class Program
    {
        /// <summary>
        /// Registry location where the netowrk adapters are defined.
        /// </summary>
        private static readonly string NetworkAdaptersKey = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}";

        /// <summary>
        /// Name of the Hyper-V adapter that should be hidden.
        /// </summary>
        private static readonly string TargetAdapterName = "Hyper-V Virtual Ethernet Adapter";

        /// <summary>
        /// Main program.  Hide the Hyper-V network adapter from Network and Sharing Center.
        /// </summary>
        static void Main()
        {
            RegistryKey topLevelKey = Registry.LocalMachine.OpenSubKey(NetworkAdaptersKey);

            // Loop through keys representing network adapters installed in the system.
            foreach (string topLevelKeyName in topLevelKey.GetSubKeyNames())
            {
                // Non-numeric keys do not represent network adapters, they will be skipped.
                if (Information.IsNumeric(topLevelKeyName))
                {
                    // Determine the network adapter's name.
                    string adapterNamePath = NetworkAdaptersKey + @"\" + topLevelKeyName;
                    string adapterName = Registry.GetValue(@"HKEY_LOCAL_MACHINE\" + adapterNamePath, "DriverDesc", null)?.ToString();
                    Console.WriteLine("{0} => {1}", topLevelKeyName, adapterName);

                    // If the name matches, add the registry value to hide it.
                    if (adapterName == TargetAdapterName)
                    {
                        Console.WriteLine("  Hiding this network adapter from Network and Sharing Center");
                        Registry.SetValue(@"HKEY_LOCAL_MACHINE\" + adapterNamePath, "*NdisDeviceType", 1, RegistryValueKind.DWord);
                    }
                }
            }
        }
    }
}
