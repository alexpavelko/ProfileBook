using ProfileBook.DefaultValues;
using Xamarin.Essentials;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public int UserId
        {
            get => Preferences.Get(nameof(UserId), Values.DEFAULT_USER_ID);
            set => Preferences.Set(nameof(UserId), value);
        }
        public string Language
        {
            get => Preferences.Get(nameof(Language), Values.DEFAULT_LANG);
            set => Preferences.Set(nameof(Language), value);
        }

        public void ChangeUserId(int userId)
        {
            Preferences.Set(nameof(UserId), userId);
        }
    }
}
