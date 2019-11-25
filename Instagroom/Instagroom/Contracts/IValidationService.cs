using Instagroom.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagroom.Contracts
{
    public interface IValidationService
    {

        string RemarkToUsername { get; }
        string RemarkUsernameExists { get; }
        string RemarkFirstName { get; }
        string RemarkLastName { get; }
        string RemarkEmailName { get; }
        string RemarkPasswordLenght { get; }
        string RemarkPasswordNotEqual { get; }
        string RemarkRegistrationCompletedSuccessfully { get;  }

        bool ValidateEmail(string email);
        PasswordValidation ValidatePassword(string passFirstEntry, string passSecondEntry);
    }
}
