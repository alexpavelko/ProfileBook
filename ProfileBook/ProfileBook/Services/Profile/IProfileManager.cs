using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileManager
    {
        Task<List<Models.Profile>> GetProfiles();
        Task SaveProfile(Models.Profile profile);
        Task RemoveProfile(Models.Profile profile);
    }
}
