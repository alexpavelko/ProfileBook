using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.DefaultValues;
using ProfileBook.Localization;
using ProfileBook.Resources;
using ProfileBook.Services.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProfileBook.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IUserDialogs UserDialogs { get; private set; }
        protected ISettingsManager SettingsManager;
        public LocalizedResources Resources
        {
            get;
            private set;
        }

        public BaseViewModel(INavigationService navigationService, IUserDialogs userDialogs, ISettingsManager settingsManager)
        {
            SettingsManager = settingsManager;
            NavigationService = navigationService;
            UserDialogs = userDialogs;

            if (string.IsNullOrEmpty(SettingsManager.Language)) 
            {
                Resources = new LocalizedResources(typeof(AppResource), Values.DEFAULT_LANG);
            }
            else
            {
                Resources = new LocalizedResources(typeof(AppResource), SettingsManager.Language);
            }
        }
        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
        public virtual void Destroy() { }

        public new void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
