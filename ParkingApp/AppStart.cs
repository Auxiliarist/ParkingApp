using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ParkingApp.Services.Interfaces;
using ParkingApp.ViewModels;
using MvvmCross.Logging;
using System;

namespace ParkingApp
{
    public class AppStart : MvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthService _authService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IAuthService authService) : base(application, navigationService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }

        protected override void NavigateToFirstViewModel(object hint = null)
        {
            if (!_authService.IsLoggedIn())
                _navigationService.Navigate<LoginViewModel>();
            else
                _navigationService.Navigate<SpotMapListViewModel>();
        }
    }
}