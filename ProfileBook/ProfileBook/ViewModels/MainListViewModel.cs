using Xamarin.Forms;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Authorization;
using ProfileBook.Extension;
using ProfileBook.Services.Settings;

namespace ProfileBook.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        private IProfileManager _profileManager;
        private IAuthorizationService _authorizationService;
    
        #region -- Public properties --  

        public ObservableCollection<ProfileViewModel> _profileList;
        public ObservableCollection<ProfileViewModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        private bool _labelIsVisible;
        public bool LabelIsVisible
        {
            get => _labelIsVisible;         
            set => SetProperty(ref _labelIsVisible, value);
        }
        public ICommand LogOutCommand => new Command(OnLogOutTap);

        public ICommand AddProfileCommand => new Command(OnAddTap);

        public ICommand UpdateCommand => new Command(OnUpdateTap);

        public ICommand DeleteCommand => new Command(OnDeleteTap);

        public ICommand SettingsCommand => new Command(OnSettingsTap);

        #endregion

        public MainListViewModel(INavigationService navigationService, IUserDialogs userDialogs,
           IProfileManager profileManager, IAuthorizationService authenticationService,
           ISettingsManager settingsManager
           ) : base(navigationService, userDialogs, settingsManager)
        {
            _profileManager = profileManager;
            _authorizationService = authenticationService;           
        }


        #region -- Overrides --
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ProfileList = new ObservableCollection<ProfileViewModel>();
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetAllUserProfiles();

            UpdateCollection();
        }

        #endregion

        #region -- Private Helpers --

        private async Task GetAllUserProfiles()
        {
            ProfileList.Clear();

            var profiles = await _profileManager.GetProfiles(); 

            if (profiles != null)
            {
                profiles.ForEach(item => ProfileList.Add(item.ToViewModel()));
            }
        }

        private async void OnLogOutTap()
        {
            var isAuthorize = _authorizationService.IsAuthorize();
            var confirmExit = await UserDialogs.ConfirmAsync(Resources["SureQuestion"], Resources["ConfirmAction"], Resources["Ok"], Resources["Cancel"]); 

            if (isAuthorize && confirmExit)
            {
                _authorizationService.LogOut();

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
        }

        private async void OnAddTap()
        {
            await NavigationService.NavigateAsync($"{nameof(AddEditProfileView)}");
        }

        private async void OnUpdateTap(object profileToUpdate)
        {
            if (profileToUpdate != null)
            {
                var profile = (ProfileViewModel)profileToUpdate;

                var parameters = new NavigationParameters()
                {
                    { "Profile", profile.ToModel() } 
                };
                await NavigationService.NavigateAsync($"{nameof(AddEditProfileView)}", parameters);
            }
        }

        private async void OnDeleteTap(object profileToDelete)
        {
            bool isConfirmed = await UserDialogs.ConfirmAsync(Resources["SureQuestion"], Resources["Delete"], Resources["OK"], Resources["Cancel"]);            
            
            if (profileToDelete != null && isConfirmed)
            {
                var profile = (ProfileViewModel)profileToDelete;

                await _profileManager.RemoveProfile(profile.ToModel());

                ProfileList.Remove(profile);

                UpdateCollection();
            }
        }

        private async void OnSettingsTap()
        {
            await NavigationService.NavigateAsync($"{nameof(SettingsView)}");
        }

        private void UpdateCollection()
        {
            LabelIsVisible = ProfileList.Count == 0;
        }

        #endregion
    }
}