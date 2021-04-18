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


namespace ProfileBook.ViewModels
{
    class MainListViewModel : BindableBase, IInitializeAsync
    {
        #region --- Public Properties---

        private ISettingsManager _settingsManager;
        private INavigationService _navigationService;
        private IRepository _repository;
        private INavigation navigation;
        public ICommand LogOutCommand => new Command(OnLogOutTap);
        public ICommand AddProfileCommand => new Command(OnAddButtonTap);
        public ICommand UpdateCommand => new Command(OnUpdateTap);
        public ICommand DeleteCommand => new Command(OnDeleteTap);

        private ProfileModel _selectedItem;
        public ProfileModel SelectedItem
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


        public ObservableCollection<ProfileModel> _profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #endregion


        public MainListViewModel(INavigationService navigationService,
                                 IRepository repository,
                                 ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _repository = repository;
            _navigationService = navigationService;
        }     

        #region --- Public Methods ---

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new ObservableCollection<ProfileModel>(profileList);
        }

        #endregion

        #region ----Private Helpers----

        private async void OnLogOutTap()
        {
            //TODO: Users.Delete(userId) разлогинивание
            await _navigationService.NavigateAsync(nameof(SignInView));
        }

        private async void OnAddButtonTap()
        {
            await _navigationService.NavigateAsync(nameof(AddEditProfileView));

            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new ObservableCollection<ProfileModel>(profileList);
        }
        

        #endregion

    }
}
