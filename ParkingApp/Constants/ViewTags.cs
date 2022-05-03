namespace ParkingApp.Constants
{
    public enum ViewModelTag : int
    {
        SpotListViewModel,
        SpotMapListViewModel,
        TimerViewModel,
        SettingsViewModel
    }

    public static class ViewTags
    {
        public const string SPOTLIST = "parkingapp.droid.views.SpotListView";
        public const string MAPLIST = "parkingapp.droid.views.SpotMapListView";
        public const string LOGIN = "parkingapp.droid.views.LoginView";
        public const string SETTINGS = "parkingapp.droid.views.SettingsView";

    }
}