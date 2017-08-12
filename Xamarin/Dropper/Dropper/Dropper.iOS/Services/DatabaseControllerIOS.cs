using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dropper.Models;
using Dropper.Services;
using Dropper.iOS.Services;
using Couchbase.Lite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseControllerIOS))]
namespace Dropper.iOS.Services
{
    public class DatabaseControllerIOS : IDatabaseController
    {
        private Database db;

        public Task Add(FileData data)
        {
            var doc = db.CreateDocument();
            doc.PutProperties(data.ToDictionary());
        }

        public Task Delete(string docId)
        {
            throw new NotImplementedException();
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
            if(db != null)
            {
                await db.Close();
                db.Dispose();
            }
            db = Manager.SharedInstance.GetDatabase(databaseName);
        }
    }
}