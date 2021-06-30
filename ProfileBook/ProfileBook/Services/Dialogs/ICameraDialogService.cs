using System.Threading.Tasks;

namespace ProfileBook.Services.Dialogs
{
    public interface ICameraDialogService
    {
        Task<string> GetPhotoFullPathAsync();
        Task<string> TakePhotoFullPathAsync();
    }
}
