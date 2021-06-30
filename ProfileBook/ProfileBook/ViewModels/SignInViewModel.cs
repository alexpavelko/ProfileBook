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
        
        #region -- Public Properties -- 
        
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

        public ICommand SignUpCommand => new Command(OnSignUpTap);
        public ICommand SignInCommand => new Command(OnSignInTap);

        #endregion

        public SignInViewModel(INavigationService navigationService, IUserDialogs userDialogs,
            IAuthenticationService authenticationService, ISettingsManager settingsManager
            ) : base(navigationService, userDialogs, settingsManager)
        {
            _authenticationService = authenticationService;   
        }


        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            parameters.TryGetValue("login", out string login);

            if (login != null)
            {
                Login = login;
            }
        }

        #endregion

        #region -- Private Helpers --

        private async void OnSignUpTap()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        private async void OnSignInTap()
        {
            var signInResult = await _authenticationService.SignInAsync(Login, Password);

            if (signInResult)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
            else
            {
                await UserDialogs.AlertAsync(Resources["LoginPasswordWrong"], Resources["ErrorLoginPassword"], Resources["Ok"]);
            }
        }

        #endregion
    }
}