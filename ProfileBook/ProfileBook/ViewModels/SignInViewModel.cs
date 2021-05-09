using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private IAuthorizationManager _authorizationManager;
        private ISettingsManager _settingsManager;
        public User User { get; set; }

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
            if (User == null) User = new User();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            parameters.TryGetValue("User", out User user);
            this.User = user;
        }

        #endregion

        #region --- Private Helpers ---

        private async void Register()
        {
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignUpView)}");
        }

        private async void SignIn()
        {
            if (await _authorizationManager.SignIn(User.Login, User.Password) == true)
            {
                _settingsManager.UserId = User.Id;
                await UserDialogs.Instance.AlertAsync($"Successful SignIn!", "Successful", "Ok");

                await NavigationService.NavigateAsync(nameof(MainListView));
            }
            else await UserDialogs.Instance.AlertAsync("Login or password wrong", "Error login or password");
        }
        #endregion
    }
}