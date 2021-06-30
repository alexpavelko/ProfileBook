using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IRepository _repository;
        private ISettingsManager _settingsManager;
        public AuthenticationService(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region -- IAuthenticationService implementation --

        public async Task<bool> SignInAsync(string login, string password)
        {
            bool signInResult = false;

            string sqlCommand = $"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'";

            User user = await _repository.FindWithCommandAsync<User>(sqlCommand);

            if (user != null)
            {
                _settingsManager.ChangeUserId(user.Id);
                signInResult = true;
            }

            return signInResult;
        }
        public async Task<bool> SignUpAsync(string login, string password)
        {
            var signUpResult = false;

            string sqlCommand = $"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'";

            User user = await _repository.FindWithCommandAsync<User>(sqlCommand);

            if (user == null)
            {
                User new_user = new User
                {
                    Login = login,
                    Password = password
                };

                await _repository.AddAsync<User>(new_user);

                signUpResult = true;
            }

            return signUpResult;
        }

        #endregion
    }
}