﻿//using NHM.Common;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace NiceHashMinerLegacy.UUID
{
    public static class WindowsMacUtils
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out System.Guid guid);

        private const long RPC_S_OK = 0L;
        private const long RPC_S_UUID_LOCAL_ONLY = 1824L;
        private const long RPC_S_UUID_NO_ADDRESS = 1739L;


        public static string GetMAC_UUID()
        {
            try
            {
                System.Guid guid;
                UuidCreateSequential(out guid);
                // Console.WriteLine(guid);
                // Console.WriteLine(GetMACAddress());
                var splitted = guid.ToString().Split('-');
                var last = splitted.LastOrDefault();
                if (last != null) return last;
            }
            catch (Exception)
            {
                //                Logger.Error("NHM.UUID", $"WindowsMacUtils.GetMAC_UUID: {e.Message}");
            }
            //            Logger.Warn("NHM.UUID", $"WindowsMacUtils.GetMAC_UUID FALLBACK");
            return System.Guid.NewGuid().ToString();
        }
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
    }
}