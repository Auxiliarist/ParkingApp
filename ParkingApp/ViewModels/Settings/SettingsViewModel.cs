using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Logging;
using MvvmCross.Commands;

namespace ParkingApp.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        public bool DarkMode
        {
            get => AppSettings.DarkModeEnabled;
            set => AppSettings.DarkModeEnabled = value;
        }

        public SettingsViewModel()
        {
        }
    }
}