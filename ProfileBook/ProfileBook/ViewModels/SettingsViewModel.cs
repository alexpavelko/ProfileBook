using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        #region --Public Propertties--

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _selectedLanguage, value);
                    SettingsManager.Language = SelectedLanguage;
                }
            }
        }

        private List<string> _languages;
        public List<string> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        #endregion

        public SettingsViewModel(INavigationService navigationService,
            IUserDialogs userDialogs, ISettingsManager settingsManager)
            : base(navigationService, userDialogs, settingsManager)
        {
            _navigationService = NavigationService;
            Languages = new List<string>(System.Enum.GetNames(typeof(Languages)));

            SelectedLanguage = SettingsManager.Language;
        }

        #region --Overrides--

        public new void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}