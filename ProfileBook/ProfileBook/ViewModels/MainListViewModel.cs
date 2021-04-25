using Xamarin.Forms;
using Prism.Mvvm;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using System.Collections.ObjectModel;
using ProfileBook.Models;
using ProfileBook.Services.Settings;
using System;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.ComponentModel;
using ProfileBook.Services.Profile;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace ProfileBook.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        private ISettingsManager _settingsManager;
        private IProfileManager _profileManager;

        #region --- Properties ---

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
                               ProfileManager profileManager,
                                 ISettingsManager settingsManager) : base(navigationService)
        {
            _settingsManager = settingsManager;
            _profileManager = profileManager;
        }

        #region --- Comands ---
        public ICommand LogOutCommand => new Command(async () => {
            //TODO: Users.Delete(userId) разлогинивание
            await NavigationService.NavigateAsync(nameof(SignInView));
        });

        public ICommand AddProfileCommand => new Command(async () => {
            await NavigationService.NavigateAsync(nameof(AddEditProfileView));
        });

        public ICommand UpdateCommand => new Command(async ()=> {
        if (SelectedItem != null)
            {
                var parameters = new NavigationParameters()
               {
                   { "Profile", SelectedItem }
               };
                await NavigationService.NavigateAsync(nameof(AddEditProfileView),parameters);
            }
        });
        public ICommand DeleteCommand => new Command(async () => {
            bool isConfirmed = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to delete?", "Confirm action", "OK", "Cancel");
            if (SelectedItem != null && isConfirmed)
            {
                await _profileManager.RemoveProfile(SelectedItem);
            }
        });

        #endregion

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            ProfileList =  new ObservableCollection<Profile> (await GetProfiles());
        }

        #region --- Private Helpers ---

        private async Task<List<Profile>> GetProfiles()
        {
            ProfileList.Clear();

            var profileList = await _profileManager.GetProfiles(Preferences.Get("UserId", 0));

            if (profileList != null && profileList.Count > 0)
            {
                profileList.ForEach(item => ProfileList.Add(item));
            }
            return profileList;
        }
 
        #endregion
    }
}