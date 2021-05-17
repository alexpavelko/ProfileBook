using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
       
        private IAuthenticationService _authenticationService;
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
            IAuthenticationService authenticationService,
            ISettingsManager settingsManager) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsManager = settingsManager;
        }

        #region --- Commands ---

        public ICommand SignUpCommand => new Command(SignUp);

        public ICommand UserSignInCommand => new Command(SignIn);

        #endregion

        #region --- Overrides ---

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

        private async void SignUp()
        {
            await NavigationService.NavigateAsync(nameof(SignUpView));
        }

        private async void SignIn()
        {
            var signInResult = await _authenticationService.SignIn(Login, Password);

            if (signInResult == true)
            {
                await NavigationService.NavigateAsync(nameof(MainListView));
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Login or password wrong", "Error login or password", "Ok");
            }
        }

        #endregion
    }
}