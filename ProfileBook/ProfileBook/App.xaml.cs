using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using ProfileBook.Services.Dialogs;
using Xamarin.Forms;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Authentication;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App() { }
        #region ---Overrides--
        protected override void OnInitialized()
        {
            InitializeComponent();
            
            ISettingsManager settingsManager = new SettingsManager();
            if (settingsManager.UserId != -1)
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
            else NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IProfileManager>(Container.Resolve<ProfileManager>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IDialogService>(Container.Resolve<DialogService>());
            containerRegistry.RegisterInstance<IAuthorizationManager>(Container.Resolve<AuthorizationManager>());
            containerRegistry.RegisterInstance<IAuthenticationManager>(Container.Resolve<AuthenticationManager>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion
    }
}