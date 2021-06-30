using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthenticationService 
    {
        Task<bool> SignUpAsync(string login, string password);
        Task<bool> SignInAsync(string login, string password);
    }
}
