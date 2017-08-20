using Dropper.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Dropper.Services
{
    public class FileService : IFileService
    {
        public async Task<FileModel> PickFileAsync()
        {
            try
            {
                FileData filedata = await CrossFilePicker.Current.PickFile();
                // the dataarray of the file will be found in filedata.DataArray 
                // file name will be found in filedata.FileName;
                //etc etc.
                return new FileModel
                {
                    Name = filedata.FileName,
                    Stream = new MemoryStream(filedata.DataArray)
                };

            }
            catch (Exception ex)
            {
                throw ex;
                //handle exception
            }
        }
    }
}
