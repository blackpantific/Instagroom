using Acr.UserDialogs;
using Instagroom.Contracts;
using Instagroom.Model;
using Instagroom.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Instagroom.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IValidationService _validationService;
        private readonly IUserDataService _userDataService;

        private string _username;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _repeatPassword;

        #region Bindable properties
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { SetProperty(ref _repeatPassword, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        #endregion

        private DelegateCommand _toLoginPageAfterRegistration;
        public DelegateCommand ToLoginPageAfterRegistration =>
            _toLoginPageAfterRegistration ?? (_toLoginPageAfterRegistration = new DelegateCommand(ExecuteToLoginPageAfterRegistration, CanExecuteToLoginPageAfterRegistration));

        async void ExecuteToLoginPageAfterRegistration()
        {
            if (String.IsNullOrWhiteSpace(Username))
            {
                UserDialogs.Instance.Toast(_validationService.RemarkToUsername);
                return;
            }

            var userProfile = await _userDataService.GetUserByUsernameAsync(Username);

            if(userProfile != null)
            {
                await UserDialogs.Instance.AlertAsync(_validationService.RemarkUsernameExists, "Warning", "OK");
                return;
            }

            if (String.IsNullOrWhiteSpace(FirstName))
            {
                UserDialogs.Instance.Toast(_validationService.RemarkFirstName);
                return;
            }
            else if (String.IsNullOrWhiteSpace(LastName))
            {
                UserDialogs.Instance.Toast(_validationService.RemarkLastName);
                return;
            }
            else if (String.IsNullOrWhiteSpace(Email) || _validationService.ValidateEmail(Email))
            {
                UserDialogs.Instance.Toast(_validationService.RemarkEmailName);
                return;
            }

                switch (_validationService.ValidatePassword(Password, RepeatPassword))
                {
                    case PasswordValidation.Ok:
                    {
                        var newUserProfile = new User()
                        {
                            Username = this.Username,
                            FirstName = this.FirstName,
                            LastName = this.LastName,
                            Email = this.Email,
                            Password = this.Password
                        };

                        await _userDataService.AddUserAsync(newUserProfile);
                        UserDialogs.Instance.Toast(_validationService.RemarkRegistrationCompletedSuccessfully);

                        await NavigationService.NavigateAsync("/NavigationPage/WelcomeView/LoginView");
                        break;
                    }
                    case PasswordValidation.TooShort:
                        {
                            UserDialogs.Instance.Toast(_validationService.RemarkPasswordLenght);
                            Password = string.Empty;
                            RepeatPassword = string.Empty;
                            return;
                        }
                    case PasswordValidation.NotEqual:
                        {
                            await UserDialogs.Instance.AlertAsync(_validationService.RemarkPasswordNotEqual, "Error", "OK");
                            Password = string.Empty;
                            RepeatPassword = string.Empty;
                            return;
                        }
                }
            
        }

        bool CanExecuteToLoginPageAfterRegistration()
        {
            return true;
        }


        private DelegateCommand _toLoginPage;
        public DelegateCommand ToLoginPage =>
            _toLoginPage ?? (_toLoginPage = new DelegateCommand(ExecuteToLoginPage, CanExecuteToLoginPage));

        async void ExecuteToLoginPage()
        {
            await NavigationService.NavigateAsync("/NavigationPage/WelcomeView/LoginView");
        }

        bool CanExecuteToLoginPage()
        {
            return true;
        }
        public RegistrationViewModel(INavigationService navigationService, IUserDataService userDataService,
            IValidationService validationService) :
            base(navigationService)
        {
            _userDataService = userDataService;
            _validationService = validationService;
        }
    }
}
