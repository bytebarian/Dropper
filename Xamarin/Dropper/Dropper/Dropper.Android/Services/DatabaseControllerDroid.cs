using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dropper.Models;
using Dropper.Services;
using Dropper.Droid.Services;
using Couchbase.Lite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseControllerDroid))]
namespace Dropper.Droid.Services
{
    public class DatabaseControllerDroid : IDatabaseController
    {
        private Database db;

        public Task Add(FileData data)
        {
            return Task.Run(() =>
            {
                var doc = db.CreateDocument();
                doc.PutProperties(data.ToDictionary());
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

        public Task<List<FileData>> GetAllDocs()
        {
            throw new NotImplementedException();
        }

        public Task<FileData> GetDoc(string docId)
        {
            throw new NotImplementedException();
        }

        public async Task Init(string databaseName)
        {
            if (db != null)
            {
                await db.Close();
                db.Dispose();
            }
            db = Manager.SharedInstance.GetDatabase(databaseName);
        }
    }
}