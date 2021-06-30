using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProfileBook.Models;
using ProfileBook.Services.ProfileManager;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;

namespace ProfileBook.Services.ProfileManager
{
    public class ProfileManager : IProfileManager
    {
        private IRepository  _repository;
        private ISettingsManager _settingsManager;

        public ProfileManager(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region -- IProfileManager implementation --

        public async Task<IEnumerable<Profile>> GetProfilesAsync()
        {
            var currentUserId = _settingsManager.UserId;

            string sqlCommand = $"SELECT * FROM Profiles WHERE UserId={currentUserId}";

            var profiles = await _repository.GetAllWithQueryAsync<Profile>(sqlCommand);

            return profiles;
        }

        public async Task<Profile> RemoveProfileAsync(Profile profile)
        {
            if (File.Exists(profile.ProfileImage))
            {
                File.Delete(profile.ProfileImage);
            }

            await _repository.DeleteAsync(profile);

            return profile;
        }

        public async Task<Profile> SaveProfileAsync(Profile profile)
        {
            var profileToSave = profile;

            profile.UserId = _settingsManager.UserId;

            await _repository.AddOrUpdateAsync(profileToSave);

            return profile;
        }

        #endregion
    }
}
