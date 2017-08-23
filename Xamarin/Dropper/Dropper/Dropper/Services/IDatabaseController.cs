using Dropper.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dropper.Services
{
    public interface IDatabaseController : IDisposable
    {
        Task Init(string databaseName);
        Task Add(FileModel data);
        Task Delete(string docId);
        Task<FileModel> GetDoc(string docId);
        Task<IEnumerable<FileModel>> GetAllDocs();
        event EventHandler<DatabaseChangedEventArgs> DatabaseChanged;
    }

    public class DatabaseChangedEventArgs : EventArgs
    {
        public DatabaseChangedEventArgs(string docId)
        {
            DocumentId = docId;
        }
        public string DocumentId { get; private set; }
    }
}
