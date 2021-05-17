﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;

namespace ProfileBook.Services.Profile
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

        public async Task<List<Models.Profile>> GetProfiles()
        {
            var currentUserId = _settingsManager.UserId;

            string sqlCommand = $"SELECT * FROM Profiles WHERE UserId={currentUserId}";

            return await _repository.GetAllWithQueryAsync<Models.Profile>(sqlCommand);
        }

        public async Task RemoveProfile(Models.Profile profile)
        {
            if (File.Exists(profile.ProfileImage))
                File.Delete(profile.ProfileImage);

            await _repository.DeleteAsync(profile);
        }

        public async Task SaveProfile(Models.Profile profile)
        {
            var profileToSave = profile;

            profile.UserId = _settingsManager.UserId;

            await _repository.AddOrUpdateAsync(profileToSave);
        }
    }
}
