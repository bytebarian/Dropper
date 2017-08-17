using System;
using System.Net;
using System.Net.NetworkInformation;
using Xamarin.Forms;

namespace Dropper.Services
{
    public class ListeningService : IDisposable
    {
        private Credentials _credentials;
        private IReceiver _receiver;

        public void Dispose()
        {
            if (_receiver == null) return;
            _receiver.Dispose();
        }

        public void StartListener()
        {
            _credentials = new Credentials();
            _credentials.Login = Guid.NewGuid().ToString();
            _credentials.Password = Guid.NewGuid().ToString();

            _receiver = DependencyService.Get<IReceiver>();
            _receiver.Initialize(_credentials);
        }

        public void StopReceiver()
        {
            _receiver.StopListener();
        }
    }
}
