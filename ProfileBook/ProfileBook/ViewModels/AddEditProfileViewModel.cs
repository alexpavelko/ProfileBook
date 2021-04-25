using System;
using Xamarin.Forms;
using Prism.Mvvm;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using ProfileBook.Validators;
using Acr.UserDialogs;
using Xamarin.Essentials;
using System.IO;
using ProfileBook.Services.Profile;
using ProfileBook.Models;

namespace ProfileBook.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel
    {
        private Profile CurrentProfile { get; set; }
        private IProfileManager _profileManager;

        public AddEditProfileViewModel(INavigationService navigationService,
            IProfileManager profileManager) : base(navigationService)
        {
            this._profileManager = profileManager;
        }

        #region --- Properties---
      
        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        #endregion

        #region --- Comands ---

        public ICommand PreviousPageCommand => new Command(async () => {
            await NavigationService.NavigateAsync(nameof(MainListView));
        });

        public ICommand SaveCommand => new Command(async () => {
            string errorMessage = ProfileValidator.IsProfileValid(CurrentProfile);
            bool isValid = string.IsNullOrEmpty(errorMessage);

            if(!isValid)
            UserDialogs.Instance.Alert(errorMessage, "Error", "Ok");

            await _profileManager.SaveProfile(CurrentProfile);
        });

        public ICommand ProfileImageClickCommand => new Command(() => {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                                .SetTitle("Choose Action")
                                .Add("Pick at Gallery", () => GetPhotoAsync(), "gallery_icon.png")
                                .Add("Take photo with camera", () => TakePhotoAsync(), "camera_icon.png")
                            );
        });

        #endregion

        #region --- Private Helpers ---
        private async void GetPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                ProfileImage = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
        }

        private async void TakePhotoAsync()
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


                ProfileImage = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
        }

        #endregion
    }
}