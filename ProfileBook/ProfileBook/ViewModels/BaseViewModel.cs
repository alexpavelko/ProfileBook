using Prism.Mvvm;
using Prism.Navigation;

namespace ProfileBook.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware
    {
        protected INavigationService NavigationService { get; private set; }
        public BaseViewModel(INavigationService navigationService)
        {
           NavigationService = navigationService;
        }
        public void Initialize(INavigationParameters parameters)
        {
           
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
           
        }
    }
}
