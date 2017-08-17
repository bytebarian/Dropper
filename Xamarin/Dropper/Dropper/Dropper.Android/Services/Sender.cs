using Couchbase.Lite;
using Dropper.Droid.Services;
using Dropper.Services;
using Android.Util;

[assembly: Xamarin.Forms.Dependency(typeof(Sender))]
namespace Dropper.Droid.Services
{
    public class Sender : ISender
    {
        static readonly string Tag = "Dropper";

        Database Database { get; set; }
        Replication Push { get; set; }

        public void PushSync(Credentials credentials)
        {
            if (credentials == null)
                return;
            var opts = new DatabaseOptions();

            // To use this feature, add the Couchbase.Lite.Storage.ForestDB nuget package
            opts.StorageType = StorageEngineTypes.ForestDB;
            Database = Manager.SharedInstance.OpenDatabase(credentials.DatabaseName, opts);

            if (Database == null)
                return;

            ForgetSync();

            if (!string.IsNullOrEmpty(credentials.SyncGatewayUrl))
            {
                try
                {
                    var uri = new System.Uri(credentials.SyncGatewayUrl);

                    Push = Database.CreatePushReplication(uri);
                    Push.Continuous = true;
                    Push.Changed += ReplicationChanged;

                    Push.Start();
                }
                catch (Java.Lang.Throwable th)
                {
                    Log.Debug(Tag, th, "UpdateSync Error");
                }
            }
        }

        private void ForgetSync()
        {
            if (Push != null)
            {
                Push.Changed -= ReplicationChanged;
                Push.Stop();
                Push = null;
            }
        }

        public void ReplicationChanged(object sender, ReplicationChangeEventArgs args)
        {
            Couchbase.Lite.Util.Log.D(Tag, "Replication Changed: {0}", args);

            var replicator = args.Source;

            var totalCount = replicator.ChangesCount;
            var completedCount = replicator.CompletedChangesCount;

            if (totalCount > 0 && completedCount < totalCount)
            {
                // update progress
            }
            else
            {
                // hide progress
            }
        }
    }
}