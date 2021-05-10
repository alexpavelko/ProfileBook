namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int UserId { get; set; }
        void ChangeUserId(int userId);
    }
}
