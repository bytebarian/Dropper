﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Dropper.Services;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Dropper.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(IPAddressService))]
namespace Dropper.iOS.Services
{
    public class IPAddressService : IIPAddressService
    {
        public string GetIPAddress()
        {
            String ipAddress = "";

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addrInfo.Address.ToString();

                        }
                    }
                }
            }

            return ipAddress;
        }
    }
}