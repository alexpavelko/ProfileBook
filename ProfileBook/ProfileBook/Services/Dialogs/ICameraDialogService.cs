namespace ProfileBook.Services.Dialogs
{
    public interface ICameraDialogService
    {
        void GetPhotoAsync(Models.Profile CurrentProfile);
        void TakePhotoAsync(Models.Profile CurrentProfile);
    }
}
