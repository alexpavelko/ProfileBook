using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private IAuthorizationManager _authorizationManager;
        private ISettingsManager _settingsManager;

        #region --- Properties --- 
        
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        #endregion
        public SignInViewModel(INavigationService navigationService,
            IAuthorizationManager authorizationManager,
            ISettingsManager settingsManager) : base(navigationService)
        {
            _authorizationManager = authorizationManager;
            _settingsManager = settingsManager;
        }

        #region --- Commands ---

        public ICommand SignUpCommand => new Command(Register);

        public ICommand UserSignInCommand => new Command(SignIn);

        #endregion

        #region --- Overrides ---

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            parameters.TryGetValue("password", out string password);
            this.Password = password;
            parameters.TryGetValue("login", out string login);
            this.Login = login;
        }

        #endregion

        #region --- Private Helpers ---

        private async void Register()
        {
            await NavigationService.NavigateAsync(nameof(SignUpView));
        }

        private async void SignIn()
        {
            User user = new User();
            user.Login = Login;
            user.Password = Password;

            if (await _authorizationManager.SignIn(user.Login, user.Password) == true)
            {
                if (user.Id != -1)
                    await NavigationService.NavigateAsync(nameof(MainListView));
            }
            else await UserDialogs.Instance.AlertAsync("Login or password wrong", "Error login or password");
        }
        #endregion
    }
}