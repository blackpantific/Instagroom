using Acr.UserDialogs;
using Instagroom.Contracts;
using Instagroom.Helpers;
using Instagroom.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Instagroom.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserDataService _userDataService;

        private string _username;
        private string _password;

        #region Bindable Properties
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        #endregion

        private DelegateCommand _toRegistrationPage;
        public DelegateCommand ToRegistrationPage =>
            _toRegistrationPage ?? (_toRegistrationPage = new DelegateCommand(ExecuteToRegistrationPage));

        private DelegateCommand _logIn;
        public DelegateCommand LogIn =>
            _logIn ?? (_logIn = new DelegateCommand(ExecuteLogIn));

        private DelegateCommand _logInViaGoogle;


        public DelegateCommand LogInViaGoogle =>
_logInViaGoogle ?? (_logInViaGoogle = new DelegateCommand(ExecuteLogInViaGoogle));

        void ExecuteLogInViaGoogle()
        {

        }

        async void ExecuteLogIn()
        {
            if (String.IsNullOrWhiteSpace(Username))
            {
                UserDialogs.Instance.Toast("Please enter your username");
                return;
            }

            if (String.IsNullOrWhiteSpace(Password))
            {
                UserDialogs.Instance.Toast("Please enter your password");
            }

            var user = await _userDataService.GetUserByUsernameAsync(Username);

            if(user != null)
            {
                if(user.Password == Password)
                {
                    _userDataService.CurrentUser = user;
                    await NavigationService.NavigateAsync("/NavigationPage/MasterTabbedPage");

                    //ShowViewModel<MasterTabControlViewModel>(presentationBundle:
                    //       new MvxBundle(new Dictionary<string, string> { { "ClearStack", "" } }));

                //как это работает?
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(ConstantHelper.PasswordIncorrectAlert, "Error", "OK");
                    Password = string.Empty;
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync($"User \"{Username}\" does not exist. Try re-entering your credentials", "Error", "OK");
                Password = string.Empty;
            }


        }

        async void ExecuteToRegistrationPage()
        {
            await NavigationService.NavigateAsync("/NavigationPage/WelcomeView/RegistrationView");
        }


        public LoginViewModel(INavigationService navigationService, IUserDataService userData) :
            base(navigationService)
        {
            _userDataService = userData;
        }
    }
}
