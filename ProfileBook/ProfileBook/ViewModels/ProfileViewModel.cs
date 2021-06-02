using Prism.Mvvm;

namespace ProfileBook.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        private int _id;
        private string _nickName;
        private string _name;
        private string _creationTime;
        private string _profileImage;
        private string _description;
        private int _userId;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        public int UserID
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }
    }
}
