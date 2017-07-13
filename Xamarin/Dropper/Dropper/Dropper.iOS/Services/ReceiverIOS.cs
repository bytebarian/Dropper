using Dropper.Services;
using Couchbase.Lite.Listener;
using Couchbase.Lite.Listener.Tcp;
using Couchbase.Lite;
using System.Collections.Generic;
using Dropper.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ReceiverIOS))]
namespace Dropper.iOS.Services
{
    class ReceiverIOS : IReceiver
    {
        CouchbaseLiteServiceListener _listener;

        public Credentials Credentials { get; private set; }

        public void Initialize(Credentials credentials)
        {
            Credentials = credentials;

            _listener = new CouchbaseLiteTcpListener(Manager.SharedInstance, Credentials.Port, CouchbaseLiteTcpOptions.AllowBasicAuth);
            _listener.SetPasswords(new Dictionary<string, string>
            {
                {Credentials.Login, Credentials.Password }
            });
        }

        public void StartListener()
        {
            _listener.Start();
        }

        public void StopListener()
        {
            _listener.Stop();
        }

        public void Dispose()
        {
            if (_listener == null) return;
            _listener.Dispose();
            _listener = null;
        }
    }
}