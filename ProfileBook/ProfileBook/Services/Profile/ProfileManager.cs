using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using ProfileBook.Services.Repository;

namespace ProfileBook.Services.Profile
{
    public class ProfileManager : IProfileManager
    {
        private IRepository _repository;
        public ProfileManager(IRepository repository)
        {
            this._repository = repository;
        }
        public async Task<List<Models.Profile>> GetProfiles(int user_id)
        {
            string sqlCommand = $"$SELECT * FROM Profiles WHERE UserId = {user_id}";
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
            await _repository.AddOrUpdateAsync(profile);
        }
    }
}
