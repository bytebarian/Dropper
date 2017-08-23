using Dropper.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Dropper.Services
{
    public class MediaService : IFileService
    {
        public MediaService()
        {
            CrossMedia.Current?.Initialize();
        }

        public async Task<FileModel> PickFileAsync()
        {
            MediaFile mediaFile = null;
            try
            {
                if (CrossMedia.Current != null)
                {
                    mediaFile = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            catch (TaskCanceledException)
            {
                // PickPhotoAsync canceled
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("MediaService.PickPhotoAsync Exception: " + ex);
#endif
            }
            return new FileModel
            {
                Name = mediaFile.Path,
                Stream = mediaFile.GetStream()
            };
        }

        public Task SaveFileAsync(FileModel file)
        {
            throw new NotImplementedException();
        }
    }
}
