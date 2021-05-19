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


        private string _image;
        public string Image
        {
            get
            {
                if (_image == null) 
                {
                    return "user_person.png";
                }
                else
                {
                    return _image;
                }
            }
            set => SetProperty(ref _image, value); 
        }

        #endregion


        #region --- Overrides ---

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (CurrentProfile == null)
            {
                CurrentProfile = new Profile();

                CurrentProfile.CreationTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                CurrentProfile.ProfileImage = "user_person.png";
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            parameters.TryGetValue("Profile", out Profile profile);
            if (profile != null)
            {
                CurrentProfile = profile;
            }

            RaisePropertyChanged(nameof(CurrentProfile));
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
                                   .Add("Pick at Gallery", () => _dialogService.GetPhotoAsync(CurrentProfile), "gallery_icon.png")
                                   .Add("Take photo with camera", () => _dialogService.TakePhotoAsync(CurrentProfile), "camera_icon.png")
                               );

            RaisePropertyChanged(nameof(CurrentProfile));
        }



        private async void Save()
        {
            bool isValid = Validator.IsProfileValid(CurrentProfile);

            string errorMessage = Validator.alert;

            if (!isValid)
            {
                UserDialogs.Instance.Alert(errorMessage, "Error", "Ok");
            }
            else
            {
                await _profileManager.SaveProfile(CurrentProfile);

                await NavigationService.NavigateAsync(nameof(MainListView));
            }
        }

        #endregion
    }
}