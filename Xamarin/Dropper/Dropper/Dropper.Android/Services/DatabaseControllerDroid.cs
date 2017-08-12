using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dropper.Models;
using Dropper.Services;
using Dropper.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseControllerDroid))]
namespace Dropper.Droid.Services
{
    public class DatabaseControllerDroid : IDatabaseController
    {
        public Task Add(FileData data)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string docId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<FileData>> GetAllDocs()
        {
            throw new NotImplementedException();
        }

        public Task<FileData> GetDoc(string docId)
        {
            throw new NotImplementedException();
        }

        public Task Init(string databaseName)
        {
            throw new NotImplementedException();
        }
    }
}