using System;
using Xamarin.Forms;
using Prism.Mvvm;
using System.Windows.Input;
using Prism.Navigation;
using ProfileBook.Views;
using System.Collections.ObjectModel;
using ProfileBook.Models;
using ProfileBook.Services.Settings;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;
using ProfileBook.Validators;
using Acr.UserDialogs;
using Xamarin.Essentials;
using System.IO;

namespace ProfileBook.ViewModels
{
    class AddEditProfileViewModel : BindableBase
    {
        public AddEditProfileViewModel(INavigationService navigationService, IRepository repository)
        {
            ProfileImage = "user_person.png";
            _navigationService = navigationService;
            _repository = repository;
        }


        #region --- Public Properties---

        private INavigationService _navigationService;
        private IRepository _repository;
        public ICommand PreviousPageCommand => new Command(OnPreviousPageClick);
        public ICommand SaveCommand => new Command(OnSaveProfileClick);
        public ICommand ProfileImageClickCommand => new Command(OnImageClick);

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

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        #endregion

        #region --- Private Methods ---

        private async void OnPreviousPageClick()
        {         
            await _navigationService.NavigateAsync(nameof(MainListView));
        }

        private async void OnSaveProfileClick()
        {
            if (Validate() == true)
            {
                var profileToSave = new ProfileModel
                {
                    Name = Name,
                    NickName = NickName,
                    ProfileImage = ProfileImage,
                    CreationTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")                    
                };
              
                var id = await _repository.InsertAsync(profileToSave);

                OnPreviousPageClick();
            }
        }

        private void OnImageClick()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                              .SetTitle("Choose Action")
                              .Add("Pick at Gallery", () => GetPhotoAsync(), "gallery_icon.png")
                              .Add("Take photo with camera", () => TakePhotoAsync(), "camera_icon.png")
                          );
        }

        #endregion

        #region --- Private Helpers ---
        private async void GetPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                ProfileImage = photo.FullPath;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message, "Error", "OK");
            }
        }

       

        private bool Validate()
        {
            if (Description == null)
                Description = string.Empty;
            var validation = new AddEditProfileViewValidator();
            if (validation.CheckUserFields(Name, NickName, Description) == false)
            {
                UserDialogs.Instance.Alert(validation.errorMessage, "Error");
                return false;
            }
            return true;
        }

        #endregion

    }
}