using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.ViewModels;

// Fix Theme

namespace ParkingApp.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Name = "parkingapp.droid.activities.SettingsActivity", Theme = "@style/MyTheme.Settings")]
    public class SettingsActivity : BaseActivity<SettingsMainViewModel>
    {
        protected override string TAG => "SettingsActivity";
        protected override int LayoutResource => Resource.Layout.SettingsActivity;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var toolbar = FindViewById<Toolbar>(Resource.Id.SettingsToolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar?.SetHomeButtonEnabled(true);
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModel.CloseCommand.Execute();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}