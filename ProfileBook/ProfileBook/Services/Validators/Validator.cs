using System.Text.RegularExpressions;

namespace ProfileBook.Services.Validators
{
    public static class Validator
    {
        public static string errorMessage { get ; set; }
        public static string IsProfileValid(Models.Profile profile)
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(profile.Name))
            {
                errorMessage = "Name must be filled!";
            }
            else if (string.IsNullOrEmpty(profile.NickName))
            {
                errorMessage = "NickName must be filled!";
            }
            else if (string.IsNullOrEmpty(profile.Description))
            {
                profile.Description = string.Empty;
            }
            else if (profile.Description.Length > 120)
            {
                errorMessage = "Description must be no more than 120 characters!";
            }
            return errorMessage;
        }
        public static bool IsLoginValid(string login)
        {
            if (!Regex.IsMatch(login, @"^[a-zA-Z][a-zA-Z0-9]{3,16}$"))
            {
                errorMessage += "Login:\n * Minimum 4 chars, maximum 16 chars.\n * Can not starts with number\n";
                return false;
            }
            return true;
        }

        public static bool IsPasswordValid(string password, string confirmPassword)
        {
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$"))
            {
                errorMessage += "Passsword:\n * Minimum 8 chars, maximum 16 chars.\n * At least 1 uppercase letter, 1 lowercase letter and 1 number\n";
                return false;
            }

            if (!confirmPassword.Equals(password))
            {
                errorMessage += "Confirm password:\n * Must match the password\n";
                return false;
            }
            return true;
        }
    }
}
