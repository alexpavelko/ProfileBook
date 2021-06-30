using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Settings;
using ProfileBook.Services.Validators;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private IAuthenticationService _authenticationService;

        #region -- Public Properties --

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

        public ICommand SignUpCommand => new Command(OnSignUpTap);
        
        #endregion

        public SignUpViewModel(INavigationService navigationService, IUserDialogs userDialogs,
            IAuthenticationService authenticationService, ISettingsManager settingsManager) 
            : base(navigationService, userDialogs, settingsManager)
        {
            _authenticationService = authenticationService;
        }

        #region -- Private Helpers --

        private async void OnSignUpTap()
        {
            bool isLoginValid = Validator.IsLoginValid(Login);
            bool isPasswrodValid = Validator.IsPasswordValid(Password, ConfirmPassword);

            if (isLoginValid && isPasswrodValid)
            {
                var signUpResult = await _authenticationService.SignUpAsync(Login, Password);

                if (signUpResult)
                {
                    var user = new User { Login = Login, Password = Password };

                    await UserDialogs.AlertAsync(Resources["RedirectingToSignIn"], Resources["SuccessfullRegistration"], Resources["Ok"]);

                    var parameters = new NavigationParameters
                    {
                        {"login", user.Login }                       
                    };
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}", parameters);
                }

                else
                {
                    await UserDialogs.AlertAsync(Resources["LoginIsTaken"], Resources["SignUp"], Resources["Ok"]);
                }
            }

            else
            {
                await UserDialogs.AlertAsync(Validator.alert, Resources["SignUp"], Resources["Ok"]);
            }
        }

        #endregion
    }
}