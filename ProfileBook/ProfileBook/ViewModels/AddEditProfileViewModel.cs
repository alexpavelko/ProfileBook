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

namespace ProfileBook.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel
    {
        private IProfileManager _profileManager;
        private ICameraDialogService _dialogService;

        public AddEditProfileViewModel(INavigationService navigationService,
            IProfileManager profileManager, CameraDialogService dialogService) : base(navigationService)
        {
            _profileManager = profileManager;
            _dialogService = dialogService;
        }

        #region -- Public Propreties --
        public Profile CurrentProfile { get; set; }
        private ICommand _previousPageCommand;
        public ICommand PreviousPageCommand => _previousPageCommand ?? new Command(Back);
        private ICommand _profileImageClickCommand;
        public ICommand ProfileImageClickCommand => _profileImageClickCommand ?? new Command(ClickImage);
        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? new Command(Save);

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

        #endregion

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

        #region --- Private Helpers ---

        private async void Back()
        {
            await NavigationService.NavigateAsync(nameof(MainListView));
        }

        private void ClickImage()
        {

            UserDialogs.Instance
               .ActionSheet(new ActionSheetConfig()
                                   .SetTitle("Choose Action")
                                   .Add("Pick at Gallery", async () => ProfileImage = await _dialogService.GetPhotoAsync(), "gallery_icon.png")
                                   .Add("Take photo with camera", async () => ProfileImage = await _dialogService.TakePhotoAsync(), "camera_icon.png")
                               );
        }

        private async void Save()
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
                UserDialogs.Instance.Alert(errorMessage, "Error", "Ok");
            }
            else
            {
                CurrentProfile.ProfileImage = ProfileImage;

                await _profileManager.SaveProfile(CurrentProfile);

                await NavigationService.NavigateAsync(nameof(MainListView));
            }
        }

        #endregion
    }
}