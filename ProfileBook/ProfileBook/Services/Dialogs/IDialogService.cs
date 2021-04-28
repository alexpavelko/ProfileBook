namespace ProfileBook.Services.Dialogs
{
    public interface IDialogService
    {
        void GetPhotoAsync(Models.Profile CurrentProfile);
        void TakePhotoAsync(Models.Profile CurrentProfile);
    }
}
