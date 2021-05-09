using Acr.UserDialogs;
using ProfileBook.Services.Dialogs;
using ProfileBook.Models;
using System.Threading.Tasks;

namespace ProfileBook.Dialogs
{
    public class PickImageDialog
    {
        private IDialogService _dialogService;
        public PickImageDialog()
        {
            _dialogService = new DialogService();
        }
        public void ChoosePhoto(Profile CurrentProfile)
        {
            UserDialogs.Instance
                .ActionSheet(new ActionSheetConfig()
                                    .SetTitle("Choose Action")
                                    .Add("Pick at Gallery", () => _dialogService.GetPhotoAsync(CurrentProfile), "gallery_icon.png")
                                    .Add("Take photo with camera", () => _dialogService.TakePhotoAsync(CurrentProfile), "camera_icon.png")
                                );
        }
    }
}
