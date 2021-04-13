using Prism.Ioc;
using Prism.Unity;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App() {}
        #region ---Overrides--
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"{nameof(SignInView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();

            //containerRegistry.RegisterForNavigation<FriendsListPage, FriendsListViewModel >();
           // containerRegistry.RegisterForNavigation<FriendPage, FriendViewModel>();
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
