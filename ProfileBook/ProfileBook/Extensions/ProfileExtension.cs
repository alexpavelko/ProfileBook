using ProfileBook.Models;
using ProfileBook.ViewModels;

namespace ProfileBook.Extension
{
    public static class ProfileExtension
    {
        public static Profile ToModel(this ProfileViewModel extensionViewModel)
        {
            Profile profile = new Profile();

            profile.Id = extensionViewModel.Id;
            profile.Name = extensionViewModel.Name;
            profile.NickName = extensionViewModel.NickName;
            profile.ProfileImage = extensionViewModel.ProfileImage;
            profile.UserId = extensionViewModel.UserID;
            profile.Description = extensionViewModel.Description;
            profile.CreationTime = extensionViewModel.CreationTime;

            return profile;      
        }

        public static ProfileViewModel ToViewModel(this Profile profile)
        {
            ProfileViewModel extensionViewModel = new ProfileViewModel();

            extensionViewModel.Id = profile.Id;
            extensionViewModel.Name = profile.Name;
            extensionViewModel.NickName = profile.NickName;
            extensionViewModel.ProfileImage = profile.ProfileImage;
            extensionViewModel.UserID = profile.UserId;
            extensionViewModel.Description = profile.Description;
            extensionViewModel.CreationTime = profile.CreationTime;

            return extensionViewModel;
        }
    }
}
