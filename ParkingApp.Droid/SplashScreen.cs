using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ParkingApp.Droid
{
    [MvxActivityPresentation]
    [Activity(Label = "@string/app_name", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }

        public override bool SupportRequestWindowFeature(int featureId)
        {
            return base.SupportRequestWindowFeature(featureId);
        }

        protected override object GetAppStartHint(object hint = null)
        {
            return base.GetAppStartHint(hint);
        }
    }
}