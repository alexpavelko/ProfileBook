namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        void LogOut();
        bool IsAuthorize();
    }
}
