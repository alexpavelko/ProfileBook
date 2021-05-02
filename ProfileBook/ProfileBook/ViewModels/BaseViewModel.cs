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
        public  virtual void Initialize(INavigationParameters parameters)
        {
           
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
           
        }
    }
}
