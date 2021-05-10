﻿using Xamarin.Forms;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using System.Collections.ObjectModel;
using ProfileBook.Models;
using ProfileBook.Services.Settings;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Authentication;

namespace ProfileBook.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        private ISettingsManager _settingsManager;
        private IProfileManager _profileManager;
        private IAuthenticationManager _authenticationManager;

        #region --- Properties ---
        private string _btnAdd;
        public string BtnAddIsVisible
        {
            get => _btnAdd;
            set => SetProperty(ref _btnAdd, value);
        }
        private Profile _selectedItem;
        public Profile SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

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


        public ObservableCollection<Profile> _profileList;
        public ObservableCollection<Profile> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #endregion
        public MainListViewModel(INavigationService navigationService,
                               IProfileManager profileManager,
                               ISettingsManager settingsManager,
                               IAuthenticationManager authenticationManager
                                 ) : base(navigationService)
        {
            _profileManager = profileManager;
            _settingsManager = settingsManager;
            _authenticationManager = authenticationManager;
            UserDialogs.Instance.AlertAsync("\nSettingM = " + settingsManager.UserId, "ok", "ok");
        }

        #region --- Commands ---
        public ICommand LogOutCommand => new Command(Logout);

        public ICommand AddProfileCommand => new Command(Add);

        public ICommand UpdateCommand => new Command(Update);

        public ICommand DeleteCommand => new Command(Delete);

        #endregion

        #region --- Overrides ---
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            ProfileList = new ObservableCollection<Profile>();
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            BtnAddIsVisible = "False";

            await LoadUsers();

            BtnAddIsVisible = "True";
        }

        #endregion

        #region --- Private Helpers ---

        private async Task LoadUsers()
        {
            this.ProfileList.Clear();

            var profiles = await _profileManager.GetProfiles(_settingsManager.UserId);

            if (profiles != null)
            {
                profiles.ForEach(item => this.ProfileList.Add(item));
                ProfileList = new ObservableCollection<Profile>(profiles);
            }
        }

        private async void Logout()
        {
            if(!_authenticationManager.outAuthorized())
                await NavigationService.NavigateAsync(nameof(SignInView));
        }

        private async void Add()
        {
            await NavigationService.NavigateAsync(nameof(AddEditProfileView));
            RaisePropertyChanged(nameof(ProfileList));
        }

        private async void Update()
        {
            if (SelectedItem != null)
            {
                var parameters = new NavigationParameters()
               {
                   { "Profile", SelectedItem }
               };
                await NavigationService.NavigateAsync(nameof(AddEditProfileView), parameters);
                RaisePropertyChanged(nameof(ProfileList));
            }

        }

        private async void Delete()
        {
            bool isConfirmed = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to delete?", "Confirm action", "OK", "Cancel");
            if (SelectedItem != null && isConfirmed)
            {
                await _profileManager.RemoveProfile(SelectedItem);
                ProfileList.Remove(SelectedItem);
            }
        }

        #endregion
    }
}