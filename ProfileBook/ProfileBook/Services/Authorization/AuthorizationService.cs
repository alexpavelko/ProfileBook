using ProfileBook.DefaultValues;
using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private ISettingsManager _settingsManager;

        public AuthorizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #region -- IAuthorizationService implementation --

        public bool IsAuthorized() => _settingsManager.UserId >= 0;

        public void LogOut() => _settingsManager.ChangeUserId(Values.DEFAULT_USER_ID);
   
        #endregion
    }
}