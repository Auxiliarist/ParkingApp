using Plugin.Settings;
using Plugin.Settings.Abstractions;
using MvvmCross.Logging;

namespace ParkingApp
{
    /// <summary> 
    ///    This is the Settings static class that can be used in your Core solution or in any of your
    ///    client applications. All settings are laid out the same exact way with getters and setters.
    /// </summary>
    public static class AppSettings
    {
        private static ISettings Settings => CrossSettings.Current;

        private const string SpotRadiusKey = "spot_radius";
        private const string FirstLaunchKey = "is_first_launch";
        private const string DarkModeKey = "is_dark_mode";

        private static readonly string StringDefault = string.Empty;

        public static bool IsFirstTimeLaunch
        {
            get => Settings.GetValueOrDefault(FirstLaunchKey, true);
            set
            {
                Logs.Instance.Debug($"Settings: IsFirstTimeLaunch = {value}");
                Settings.AddOrUpdateValue(FirstLaunchKey, value);
            }
        }

        public static double SpotRadius
        {
            get => Settings.GetValueOrDefault(SpotRadiusKey, 10.0);
            set
            {
                Logs.Instance.Debug($"Settings: SpotRadius = {value}");
                Settings.AddOrUpdateValue(SpotRadiusKey, value);
            }
        }

        public static bool DarkModeEnabled
        {
            get => Settings.GetValueOrDefault(DarkModeKey, false);
            set
            {
                Logs.Instance.Debug($"Settings: DarkModeEnabled = {value}");
                Settings.AddOrUpdateValue(DarkModeKey, value);
            }
        }
    }
}