using Xamarin.Forms;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using Acr.UserDialogs;
using ProfileBook.Services.Profile;
using ProfileBook.Models;
using System;
using ProfileBook.Services.Validators;
using ProfileBook.Services.Dialogs;
using ProfileBook.DefaultValues;
using ProfileBook.Services.Settings;

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
           IProfileManager profileManager, CameraDialogService dialogService, ISettingsManager settingsManager) 
            : base(navigationService, userDialogs, settingsManager)
        {
            _profileManager = profileManager;
            _dialogService = dialogService;
        }

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ProfileImage = Values.DEFAULT_PROFILE_IMAGE;
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
            else
            {
                CurrentProfile = new Profile();
            }
        }

        #endregion

        #region -- Private Helpers --

        private void OnImageTap()
        {
            UserDialogs.ActionSheet(new ActionSheetConfig()
                                   .SetTitle(Resources["ChooseAction"])
                                   .Add(Resources["Pick at Gallery"], async () => ProfileImage = await _dialogService.GetPhotoAsync(), Values.GALLERY_ICON)
                                   .Add(Resources["TakePhoto"], async () => ProfileImage = await _dialogService.TakePhotoAsync(), Values.CAMERA_ICON)
                                   .Add(Resources["Cancel"], () => ProfileImage = ProfileImage, Values.CANCEL)
                                   );
        }

        private async void OnSaveTap()
        {
            CurrentProfile.CreationTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            CurrentProfile.Name = Name;
            CurrentProfile.NickName = NickName;
            CurrentProfile.Description = Description;          

            bool isValid = Validator.IsProfileValid(CurrentProfile);         

            if (!isValid)
            {
                UserDialogs.Alert(Resources["NickNameNameIsEmpty"], Resources["Oops"], Resources["Ok"]);
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