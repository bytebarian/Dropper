using System;
using System.Collections.Generic;

using Couchbase.Lite.Listener;
using Couchbase.Lite.Listener.Tcp;
using Couchbase.Lite;
using Dropper.Services;
using Dropper.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Receiver))]
namespace Dropper.Droid.Services
{
    public class Receiver : IReceiver
    {
        CouchbaseLiteServiceListener _listener;

        public Credentials Credentials { get; private set; }

        public void Initialize(Credentials cerdentials)
        {
            Credentials = cerdentials;

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