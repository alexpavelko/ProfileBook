using Acr.UserDialogs;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProfileBook.Services.Dialogs
{
    public class CameraDialogService : ICameraDialogService
    {
        #region -- ICameraDialog implementation --

        public async Task<string> GetPhotoFullPathAsync()
        {
            string fullPath = "";

            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                 fullPath = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message);
            }

            return fullPath;
        }

        public async Task<string> TakePhotoFullPathAsync()
        {
            string fullPath = "";

            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

                using (var stream = await photo.OpenReadAsync())
                {
                    using (var newStream = File.OpenWrite(newFile))
                    {
                        await stream.CopyToAsync(newStream);
                    }
                }

                fullPath = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message);
            }

            return fullPath;
        }

        #endregion
    }
}