using Android.App;
using Android.Content;
using Android.OS;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.ViewModels;
using System;
using MvvmCross.Logging;

namespace ParkingApp.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Name = "parkingapp.droid.activities.AuthActivity")]
    public class AuthActivity : BaseActivity<AuthViewModel>
    {
        protected override string TAG => "AuthActivity";
        protected override int LayoutResource => Resource.Layout.AuthActivity;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
    }
}