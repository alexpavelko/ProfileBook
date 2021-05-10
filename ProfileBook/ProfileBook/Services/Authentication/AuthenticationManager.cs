using ProfileBook.Services.Settings;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    class AuthenticationManager : IAuthenticationManager
    {
        private ISettingsManager _settingsManager;
        public AuthenticationManager(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public bool outAuthorized()
        {
            _settingsManager.ChangeUserId(-1);
            bool isAuthorized = false;
            return isAuthorized;
        }
    }
}
