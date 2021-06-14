namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int UserId { get; set; }
        string Language { get; set; }
        void ChangeUserId(int userId);
    }
}
