using Acr.UserDialogs;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProfileBook.Services.Dialogs
{
    public class CameraDialogService : ICameraDialogService
    {
        private string result;

        public async Task<string> GetPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
        
                if (photo.FullPath != null)
                {
                    result = photo.FullPath;                    
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }

            return result;
        }
        public async Task<string> TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });


                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                if (photo.FullPath != null)
                {
                    result = photo.FullPath;     
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }

            return result;
        }
    }
}
