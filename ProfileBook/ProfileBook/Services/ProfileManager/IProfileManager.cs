using ProfileBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.ProfileManager
{
    public interface IProfileManager
    {
        Task<IEnumerable<Profile>> GetProfilesAsync();
        Task<Profile> SaveProfileAsync(Profile profile);
        Task<Profile> RemoveProfileAsync(Profile profile);
    }
}
