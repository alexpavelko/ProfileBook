using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private IRepository _repository;
        private ISettingsManager _settingsManager;
        public AuthorizationManager(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }
        public async Task<bool> SignIn(string login, string password)
        {
            string sqlCommand = $"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'";
            User user = await _repository.FindWithCommandAsync<User>(sqlCommand);
            if(user != null)
            {
                _settingsManager.UserId = user.Id;
                return true;
            }
            return false;
        }
        public async Task<bool> RegisterUser(string login, string password)
        {
            string sqlCommand = $"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'";

            User user = await _repository.FindWithCommandAsync<User>(sqlCommand);
            if (user != null)
                return false;

            User new_user = new User
            {
                Login = login,
                Password = password
            };
            await _repository.AddAsync<User>(new_user);
            return true;
        }
    }
}