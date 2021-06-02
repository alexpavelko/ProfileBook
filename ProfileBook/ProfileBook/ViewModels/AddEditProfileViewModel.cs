﻿using Xamarin.Forms;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using Acr.UserDialogs;
using ProfileBook.Services.Profile;
using ProfileBook.Models;
using System;
using ProfileBook.Services.Validators;
using ProfileBook.Services.Dialogs;

namespace ProfileBook.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel
    {
        private IProfileManager _profileManager;
        private ICameraDialogService _dialogService;
        private Profile CurrentProfile { get; set; }

        #region -- Public Propreties --

        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        public ICommand OnImageTapCommand => new Command(OnImageTap);
        public ICommand SaveCommand => new Command(OnSaveTap);

        #endregion

        public AddEditProfileViewModel(INavigationService navigationService, IUserDialogs userDialogs,
           IProfileManager profileManager, CameraDialogService dialogService) : base(navigationService, userDialogs)
        {
            _profileManager = profileManager;
            _dialogService = dialogService;
        }

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ProfileImage = "user_person.png";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            parameters.TryGetValue("Profile", out Profile profile);

            if (profile != null)
            {
                CurrentProfile = profile;
                ProfileImage = CurrentProfile.ProfileImage;
                Name = CurrentProfile.Name;
                NickName = CurrentProfile.NickName;
                Description = CurrentProfile.Description;
            }
        }

        #endregion

        #region -- Private Helpers --

        private void OnImageTap()
        {
            UserDialogs.ActionSheet(new ActionSheetConfig()
                                   .SetTitle("Choose Action")
                                   .Add("Pick at Gallery", async () => ProfileImage = await _dialogService.GetPhotoAsync(), "gallery_icon.png")
                                   .Add("Take photo with camera", async () => ProfileImage = await _dialogService.TakePhotoAsync(), "camera_icon.png")
                               );
        }

        private async void OnSaveTap()
        {
            if (CurrentProfile == null)
            {
                CurrentProfile = new Profile();
            }
                CurrentProfile.CreationTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                CurrentProfile.Name = Name;
                CurrentProfile.NickName = NickName;
                CurrentProfile.Description = Description;
            

            bool isValid = Validator.IsProfileValid(CurrentProfile);

            string errorMessage = Validator.alert;

            if (!isValid)
            {
                UserDialogs.Alert(errorMessage, "Error", "Ok");
            }
            else
            {
                var list = _profileManager.GetProfiles();

                CurrentProfile.ProfileImage = ProfileImage;

                await _profileManager.SaveProfile(CurrentProfile);

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
        }

        #endregion
    }
}