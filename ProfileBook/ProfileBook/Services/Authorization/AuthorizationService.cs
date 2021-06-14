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

        public bool IsAuthorize()
        {
            var result = false;

            if(_settingsManager.UserId >= 0)
            {
                result = true;
            }

            return result;
        }

        public void LogOut()
        {
            _settingsManager.ChangeUserId(Values.DEFAULT_USER_ID);
        }

        #endregion
    }
}