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
        protected INavigationService NavigationService { get; }
        protected IUserDialogs UserDialogs { get; }
        protected ISettingsManager SettingsManager { get; }
        public LocalizedResources Resources { get; }

        public BaseViewModel(INavigationService navigationService, IUserDialogs userDialogs, ISettingsManager settingsManager)
        {
            SettingsManager = settingsManager;
            NavigationService = navigationService;
            UserDialogs = userDialogs;

            if (string.IsNullOrEmpty(settingsManager.Language)) 
            {
                Resources = new LocalizedResources(typeof(AppResource), Values.DEFAULT_LANG);
            }
            else
            {
                Resources = new LocalizedResources(typeof(AppResource), SettingsManager.Language);
            }
        }

        #region -- IInitialize implementation --

        public virtual void Initialize(INavigationParameters parameters) 
        {
        
        }

        #endregion

        #region -- INavigationAware  implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        #endregion

        #region -- IDestructible implementation --

        public virtual void Destroy() { }

        #endregion

        public new void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        
        public new event PropertyChangedEventHandler PropertyChanged;
    }
}