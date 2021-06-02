using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileManager
    {
        Task<List<Models.Profile>> GetProfiles();
        Task<Models.Profile> SaveProfile(Models.Profile profile);
        Task<Models.Profile> RemoveProfile(Models.Profile profile);
    }
}
