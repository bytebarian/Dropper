using Dropper.Models;
using System.Threading.Tasks;

namespace Dropper.Services
{
    public interface IFileService
    {
        Task<FileModel> PickFileAsync();
        Task SaveFileAsync(FileModel file);
    }
}
