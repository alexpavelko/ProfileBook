using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        public ICommand NextPageCommand => new Command(OnNextPage);

        private INavigationService _navigationService;

        private void OnNextPage()
        {
            _navigationService.NavigateAsync(nameof(SignUpView));
        }

        public SignInViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
