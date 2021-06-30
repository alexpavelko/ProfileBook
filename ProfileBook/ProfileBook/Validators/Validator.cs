using ProfileBook.Localization;
using ProfileBook.Models;
using ProfileBook.Resources;
using System.Text.RegularExpressions;

namespace ProfileBook.Services.Validators
{
    public static class Validator
    {
        private static LocalizedResources Resources { get; set; }
        public static string alert;
        public static bool IsProfileValid(Profile profile)
        {
            Resources = new LocalizedResources(typeof(AppResource), DefaultValues.Values.DEFAULT_LANG);
            bool isValid = true;

            if (string.IsNullOrEmpty(profile.Name) || string.IsNullOrEmpty(profile.NickName))
            {               
                isValid = false;
            }

            return isValid;
        }
        public static bool IsLoginValid(string login)
        {
            Resources = new LocalizedResources(typeof(AppResource), DefaultValues.Values.DEFAULT_LANG);
            bool isValid = false;

            if (login != null)
            {
               isValid = true;

                if (!Regex.IsMatch(login, @"^[a-zA-Z][a-zA-Z0-9]{3,16}$"))
                {
                    alert = Resources["LoginNotValid"];
                    isValid = false;
                }
                else
                {
                    alert = Resources["FieldsIsEmpty"];
                }
            }

            return isValid;
        }
        public static bool IsPasswordValid(string password, string confirmPassword)
        {
            bool isValid = false;

            if (password != null && confirmPassword != null)
            {
                isValid = true;

                if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$"))
                {
                    alert = Resources["PasswordNotValid"];
                    isValid = false;
                }

                if (!confirmPassword.Equals(password))
                {
                    alert = Resources["PasswordsAreNotEqual"];
                    isValid = false;
                }               
            }
            else 
            {
                alert = Resources["FieldsIsEmpty"];
            }

            return isValid;
        }
    }
}
