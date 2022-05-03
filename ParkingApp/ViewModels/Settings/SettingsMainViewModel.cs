using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ParkingApp.ViewModels
{
    public class SettingsMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxAsyncCommand CloseCommand { get; }

        public SettingsMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            CloseCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
        }
    }
}