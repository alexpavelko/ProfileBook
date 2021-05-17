 using Acr.UserDialogs;
using System;
using System.IO;
using Xamarin.Essentials;

namespace ProfileBook.Services.Dialogs
{
    public class CameraDialogService : ICameraDialogService
    {
        public async void GetPhotoAsync(Models.Profile CurrentProfile)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                if (photo.FullPath != null)
                {
                    CurrentProfile.ProfileImage = photo.FullPath;
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
        }
        public async void TakePhotoAsync(Models.Profile CurrentProfile)
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
                    CurrentProfile.ProfileImage = photo.FullPath;
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
        }
    }
}
