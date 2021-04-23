namespace ProfileBook.Validators
{
    public class AddEditProfileViewValidator
    {
        public string errorMessage { get; set; }
        public bool CheckUserFields(string Name, string NickName, string Description)
        {
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(NickName))
            {
                errorMessage = "NickName and Name must be filled!";
                return false;
            }
            else if (string.IsNullOrEmpty(Name))
            {
                errorMessage = "Name must be filled!";
                return false;
            }
            else if (string.IsNullOrEmpty(NickName))
            {
                errorMessage = "NickName must be filled!";
                return false;
            }
            else if (Description.Length > 120) 
            {
                errorMessage = "Description must be no more than 120 characters!";
                return false;
            }
            return true;
        }
    }
}
