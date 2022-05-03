namespace ParkingApp.Constants
{
    public static class Location
    {
        public const int REQUEST_CHECK_SETTINGS = 0x1;

        public const int PERMISSION_REQUEST_LOCATION = 0;

        /// <summary> 
        ///    The fastest rate for active location updates. Updates will never be more frequent than
        ///    this value, but they may be less frequent.
        /// </summary>
        public const long FASTEST_UPDATE_INTERVAL = 1000;

        /// <summary> 
        ///    The desired interval for location updates. Inexact. Updates may be more or less frequent.
        /// </summary>
        public const long UPDATE_INTERVAL = 5000;
    }
}