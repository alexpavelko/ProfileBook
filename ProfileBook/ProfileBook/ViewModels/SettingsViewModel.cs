using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ISettingsManager _settingsManager;

        #region -- Public Properties --

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        private ObservableCollection<string> _languages;
        public ObservableCollection<string> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        #endregion

        public SettingsViewModel(INavigationService navigationService,
            IUserDialogs userDialogs, ISettingsManager settingsManager)
            : base(navigationService, userDialogs, settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Languages = new ObservableCollection<string>(System.Enum.GetNames(typeof(Languages)));
            
            SelectedLanguage = _settingsManager.Language;
         }

        //NOT WORKS ↓

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == SelectedLanguage && SelectedLanguage != null)
            {
                _settingsManager.Language = SelectedLanguage;
            }
        }
    }
}