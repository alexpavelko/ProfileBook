using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.ProfileManager;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using ProfileBook.Services.Dialogs;
using Xamarin.Forms;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Authentication;
using Acr.UserDialogs;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        private IAuthorizationService _authorizationService;
        public IAuthorizationService AuthorizationService =>
            _authorizationService ?? (_authorizationService = Container.Resolve<IAuthorizationService>());
        public App() { }
        
        #region -- Overrides --

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var isAuthorized = AuthorizationService.IsAuthorized();

            if (isAuthorized)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
            else
            {               
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IProfileManager>(Container.Resolve<ProfileManager>());    
            containerRegistry.RegisterInstance<ICameraDialogService>(Container.Resolve<CameraDialogService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());

            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
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