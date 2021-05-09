using Xamarin.Forms;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using Acr.UserDialogs;
using ProfileBook.Services.Profile;
using ProfileBook.Models;
using ProfileBook.Services.Settings;
using ProfileBook.Dialogs;
using System;
using ProfileBook.Services.Validators;

namespace ProfileBook.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel
    {
        public Profile CurrentProfile { get; set; }
        private IProfileManager _profileManager;
        private ISettingsManager _settingsManager;

        public AddEditProfileViewModel(INavigationService navigationService,
            IProfileManager profileManager,
            ISettingsManager settingsManager) : base(navigationService)
        {
            this._profileManager = profileManager;
            this._settingsManager = settingsManager;
        }

        #region --- Commands ---

        public ICommand PreviousPageCommand => new Command(Back);
        public ICommand ProfileImageClickCommand => new Command(ClickImage);
        public ICommand SaveCommand => new Command(Save);

        #endregion

        #region --- Overrides ---

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (CurrentProfile == null)
                this.CurrentProfile = new Profile();
            CurrentProfile.CreationTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            CurrentProfile.ProfileImage = "user_person.png";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            parameters.TryGetValue("Profile", out Profile profile);
            if (profile != null)
                CurrentProfile = profile;
            RaisePropertyChanged(nameof(CurrentProfile));
        }

        #endregion

        #region --- Private Helpers ---

        private async void Back()
        {
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainListView)}");
        }


        private void ClickImage()
        {
            var pickImageDialog = new PickImageDialog();

            pickImageDialog.ChoosePhoto(CurrentProfile);

            RaisePropertyChanged(nameof(CurrentProfile));
        }

        private async void Save()
        {
            string errorMessage = Validator.IsProfileValid(CurrentProfile);
            bool isValid = string.IsNullOrEmpty(errorMessage);

            if (!isValid)
                UserDialogs.Instance.Alert(errorMessage, "Error", "Ok");
            else
            {
                await _profileManager.SaveProfile(CurrentProfile);

                await NavigationService.NavigateAsync(nameof(MainListView));
            }
        }

        #endregion
    }
}