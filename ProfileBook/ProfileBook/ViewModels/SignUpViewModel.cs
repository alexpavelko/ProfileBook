using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : BindableBase
    {
        public ICommand BackCommand => new Command(OnBack);

        private INavigationService _navigationService;

        private void OnBack()
        {
            _navigationService.NavigateAsync(nameof(SignInView));
        }

        public SignUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
