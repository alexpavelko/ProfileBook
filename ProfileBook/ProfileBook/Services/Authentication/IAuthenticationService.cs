using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthenticationService 
    {
        Task<bool> SignUp(string login, string password);
        Task<bool> SignIn(string login, string password);
    }
}
