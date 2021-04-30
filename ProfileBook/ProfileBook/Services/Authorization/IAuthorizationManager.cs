using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationManager
    {
        Task<bool> RegisterUser(string login, string password);
        Task<bool> SignIn(string login, string password);
    }
}
