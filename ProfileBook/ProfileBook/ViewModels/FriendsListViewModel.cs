using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class FriendsListViewModel : BindableBase
    {
        int _count = 0;
        public ICommand IncrementLabelCommand => new Command(OnIncrementLabel);

        public ICommand NextPageCommand => new Command(OnNextPage);

        private void OnNextPage()
        {
            _navigationService.NavigateAsync(nameof(FriendPage));
        }

        private void OnIncrementLabel()
        {
            Title = _count++.ToString();
        }

      

        private INavigationService _navigationService;
        public FriendsListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Title))
            {
                Console.WriteLine();
            }
        }
    }
}
