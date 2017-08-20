using Dropper.Services;
using System.Net;
using Dropper.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(IPAddressService))]
namespace Dropper.Droid.Services
{
    public class IPAddressService : IIPAddressService
    {
        public string GetIPAddress()
        {
            IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());

            if (adresses != null && adresses[0] != null)
            {
                return adresses[0].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}