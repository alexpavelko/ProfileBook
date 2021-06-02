using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;

namespace ProfileBook.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware
    {
        protected INavigationService NavigationService { get; private set; }
        protected IUserDialogs UserDialogs { get; private set; }
        public BaseViewModel(INavigationService navigationService, IUserDialogs userDialogs)
        {
            NavigationService = navigationService;
            UserDialogs = userDialogs;
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
