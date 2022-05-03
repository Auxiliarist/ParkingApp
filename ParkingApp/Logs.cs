using MvvmCross;
using MvvmCross.Logging;

namespace ParkingApp
{
    public static class Logs
    {
        public static IMvxLog Instance { get; } = Mvx.Resolve<IMvxLogProvider>().GetLogFor("Parking");
    }
}