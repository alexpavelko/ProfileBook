using System.Text.RegularExpressions;

namespace ProfileBook.Services.Validators
{
    public static class Validator
    {
        public static string alert { get ; set; }
        private static bool isValid { get; set; }
        
        public static bool IsProfileValid(Models.Profile profile)
        {
            isValid = true;

            if (string.IsNullOrEmpty(profile.Name) || string.IsNullOrEmpty(profile.NickName))
            {
                alert = "NickName and name must be filled!";
                isValid = false;
            }

            if (profile.Description == null)
            {
                profile.Description = string.Empty;
            }

            if (profile.Description.Length > 120)
            {
                alert = "Description must be no more than 120 characters!";
                isValid = false;
            }

            return isValid;
        }
        public static bool IsLoginValid(string login)
        {
            if (login != null)
            {
                isValid = true;

                if (!Regex.IsMatch(login, @"^[a-zA-Z][a-zA-Z0-9]{3,16}$"))
                {
                    alert = "Login:\n * Minimum 4 chars, maximum 16 chars.\n * Can not starts with number\n";
                    isValid = false;
                }
                else
                {
                    alert = "All fields must be field!";
                }
            }

            return isValid;
        }
        public static bool IsPasswordValid(string password, string confirmPassword)
        {
           if (password != null && confirmPassword != null)
           {
                isValid = true;

                if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$"))
                {
                    alert = "Passsword:\n * Minimum 8 chars, maximum 16 chars.\n * At least 1 uppercase letter, 1 lowercase letter and 1 number\n";
                    isValid = false;
                }

                if (!confirmPassword.Equals(password))
                {
                    alert = "Confirm password:\n * Must match the password\n";
                    isValid = false;
                }               
            }
            else 
            {
                alert = "All fields must be field!";
            }

            return isValid;
        }
    }
}
