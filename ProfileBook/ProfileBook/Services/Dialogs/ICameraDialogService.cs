using System.Threading.Tasks;

namespace ProfileBook.Services.Dialogs
{
    public interface ICameraDialogService
    {
        Task<string> GetPhotoAsync();
        Task<string> TakePhotoAsync();
    }
}
