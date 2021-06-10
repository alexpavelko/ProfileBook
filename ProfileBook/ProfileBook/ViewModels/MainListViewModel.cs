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

namespace ProfileBook.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        private IProfileManager _profileManager;
        private IAuthorizationService _authorizationService;
    
        #region -- Public properties --  
        
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

        private string _creationTime;
        public string CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
        }

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

        #endregion

        public MainListViewModel(INavigationService navigationService, IUserDialogs userDialogs,
           IProfileManager profileManager, IAuthorizationService authenticationService
           ) : base(navigationService, userDialogs)
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

            if (isAuthorize)
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
            bool isConfirmed = await UserDialogs.ConfirmAsync("Are you sure you want to delete?", "Confirm action", "OK", "Cancel");            
            
            if (profileToDelete != null && isConfirmed)
            {
                var profile = (ProfileViewModel)profileToDelete;

                await _profileManager.RemoveProfile(profile.ToModel());

                ProfileList.Remove(profile);

                UpdateCollection();
            }
        }

        private void UpdateCollection()
        {
            LabelIsVisible = ProfileList.Count == 0;
        }

        #endregion
    }
}