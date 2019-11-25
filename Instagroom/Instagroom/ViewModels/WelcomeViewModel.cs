using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Instagroom.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private DelegateCommand _toRegistrationPage;
        public DelegateCommand ToRegistrationPage =>
            _toRegistrationPage ?? (_toRegistrationPage = new DelegateCommand(ExecuteToRegistrationPage, CanExecuteToRegistrationPage));


        private DelegateCommand _toLoginPage;
        public DelegateCommand ToLoginPage =>
            _toLoginPage ?? (_toLoginPage = new DelegateCommand(ExecuteToLoginPage, CanExecuteToLoginPage));

        async void ExecuteToLoginPage()
        {
            await NavigationService.NavigateAsync("LoginView");
        }

        bool CanExecuteToLoginPage()
        {
            return true;
        }

        async void ExecuteToRegistrationPage()
        {
            //await NavigationService.NavigateAsync("/WelcomeView/RegistrationView", null, null, false);
            await NavigationService.NavigateAsync("RegistrationView");
        }

        bool CanExecuteToRegistrationPage()
        {
            return true;
        }

        public WelcomeViewModel(INavigationService navigationService) : 
            base(navigationService)
        {

        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);


        }

    }
}
