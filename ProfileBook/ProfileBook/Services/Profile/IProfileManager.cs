using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileManager
    {
        Task<List<Models.Profile>> GetProfiles(int user_id);
        Task SaveProfile(Models.Profile profile);
        Task RemoveProfile(Models.Profile profile);
    }
}
