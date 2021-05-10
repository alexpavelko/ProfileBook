using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Validators;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private IAuthorizationManager _authorizationManager;

        #region --- Properties ---
        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        #endregion
        
        public SignUpViewModel(INavigationService navigationService,
            IAuthorizationManager authorizationManager) : base(navigationService)
        {
            _authorizationManager = authorizationManager;
        }

        #region --- Commands ---

        public ICommand BackCommand => new Command(SignIn);

        public ICommand SignUpCommand => new Command(Register);
        #endregion

        #region --- Private Helpers ---

        private async void SignIn()
        {
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
        }

        private async void Register()
        {
            if (Validator.IsLoginValid(Login) &&
           Validator.IsPasswordValid(Password, ConfirmPassword))
                if (await _authorizationManager.RegisterUser(Login, Password))
                {
                    User user = new User { Login = Login, Password = Password };
                    await UserDialogs.Instance.AlertAsync("Successful Registration!", "Successful", "Ok");
                    var parameters = new NavigationParameters
                    {
                        {"login", user.Login },
                        {"password", user.Password }
                    };
                    await NavigationService.NavigateAsync(nameof(SignInView), parameters);
                }
                else await UserDialogs.Instance.AlertAsync("Login is already takern!", "Sign up", "Ok");
            else await UserDialogs.Instance.AlertAsync(Validator.errorMessage, "Sign in", "Ok");
        }
        #endregion
    }
}
