using System.Collections.Generic;
using System.Threading.Tasks;
using Dropper.Models;
using Dropper.Services;
using Dropper.Droid.Services;
using Couchbase.Lite;
using System.Linq;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseController))]
namespace Dropper.Droid.Services
{
    public class DatabaseController : IDatabaseController
    {
        private Database db;

        public event EventHandler<DatabaseChangedEventArgs> DatabaseChanged;

        public Task Add(FileModel data)
        {
            return Task.Run(() =>
            {
                var doc = db.CreateDocument();
                var newRev = doc.CreateRevision();
                newRev.SetAttachment(data.Name, data.ContentType, data.Stream);
                newRev.SetProperties(data.ToDictionary());
                newRev.Save();
            });
        }

        public Task Delete(string docId)
        {
            return Task.Run(() => db.DeleteLocalDocument(docId));
        }

        public void Dispose()
        {
            if (db == null) return;
            db.Dispose();
            db = null;
        }

        public async Task<IEnumerable<FileModel>> GetAllDocs()
        {
            var query = db.CreateAllDocumentsQuery();
            query.AllDocsMode = AllDocsMode.AllDocs;
            var rows = await query.RunAsync();
            return rows.Select(x => new FileModel(x.AsJSONDictionary()));
        }

        public Task<FileModel> GetDoc(string docId)
        {
            return Task.Run(() =>
            {
                var doc = db.GetDocument(docId);
                return new FileModel(doc.CurrentRevision.Properties);
            });
        }

        public async Task Init(string databaseName)
        {
            if (db != null)
            {
                await db.Close();
                db.Dispose();
            }
            db = Manager.SharedInstance.GetDatabase(databaseName);
            db.Changed += Db_Changed;
        }

        private void Db_Changed(object sender, DatabaseChangeEventArgs e)
        {
            foreach (var change in e.Changes)
            {
                DatabaseChanged(this, new DatabaseChangedEventArgs(change.DocumentId));
            }
        }
    }
}