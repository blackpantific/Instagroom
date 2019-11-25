using Instagroom.Contracts;
using Instagroom.Helpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Instagroom.Services
{


    class ValidationService : IValidationService
    {
        public string RemarkToUsername { get => "Please enter your username"; }
        public string RemarkUsernameExists { get => "This username is already taken"; }
        public string RemarkFirstName { get => "Enter your first name"; }
        public string RemarkLastName { get => "Enter your last name"; }
        public string RemarkEmailName { get => "Please enter a valid email address"; }
        public string RemarkPasswordLenght { get => "Please enter a valid password at least 8 characters long"; }
        public string RemarkPasswordNotEqual { get => "The passwords don't match"; }
        public string RemarkRegistrationCompletedSuccessfully { get => "Your account has been registered"; }

        public bool ValidateEmail(string email)
        {
            if(Regex.IsMatch(email, ConstantHelper.emailPattern.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PasswordValidation ValidatePassword(string passFirstEntry, string passSecondEntry)
        {
           if(String.Equals(passFirstEntry, passSecondEntry))
            {
                if (passFirstEntry.Length > 7)
                {
                    return PasswordValidation.Ok;
                }
                else
                {
                    return PasswordValidation.TooShort;
                }
            }
            else
            {
                return PasswordValidation.NotEqual;
            }
        }
    }
}
