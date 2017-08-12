using Dropper.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dropper.Services
{
    public interface IDatabaseController : IDisposable
    {
        Task Init(string databaseName);
        Task Add(FileData data);
        Task Delete(string docId);
        Task<FileData> GetDoc(string docId);
        Task<List<FileData>> GetAllDocs(); 
    }
}
