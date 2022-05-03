using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Logging;
using ParkingApp.Constants;
using ParkingApp.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;

//Don't use SetSupportActionBar?? Use Toolbar API instead??
//Maybe do location permissions in Activity OnResume??
//Start Firebase Listener in MapViewModel??
//Claim spot in new activity, with directiions to spot, spot timestamp, and buttons saying spot taken/spot succussfulyl claimed
//Can search spots anywhere, but can only claim when in radius??
//Add visibility flag to spot in firebase/look into OnDataChanged for geofire for when flag changes
//Use Messenger for Claim spot activity

namespace ParkingApp.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(LaunchMode = LaunchMode.SingleTop, Name = "parkingapp.droid.activities.MainActivity")]
    public class MainActivity : BaseActivity<MainViewModel>, Android.Support.V4.App.FragmentManager.IOnBackStackChangedListener
    {
        protected override string TAG => "MainActivity";
        protected override int LayoutResource => Resource.Layout.MainActivity;

        public static int selectedItemId;
        public static bool locationPermissionGranted;

        public DrawerLayout DrawerLayout { get; set; }
        public NavigationView NavigationView { get; set; }
        public ActionBarDrawerToggle Toggle { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            
            SetSupportActionBar(toolbar);
            //toolbar.SetOnMenuItemClickListener(this);

            SupportFragmentManager.AddOnBackStackChangedListener(this);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.MainActivityLayout);
            NavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            Toggle = new ActionBarDrawerToggle(this, DrawerLayout, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);

            DrawerLayout.AddDrawerListener(Toggle);

            var set = this.CreateBindingSet<MainActivity, MainViewModel>();
            set.Bind(NavigationView).For("NavigationItemSelected").To(vm => vm.NavCommand);
            set.Apply();

            //if (bundle == null)
            //    selectedItemId = Resource.Id.nav_spots;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (Toggle.OnOptionsItemSelected(item))
                return true;

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            // Close Nav Drawer if Opened
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
                DrawerLayout.CloseDrawers();

            // Close App if on MapView
            else if(SupportFragmentManager.BackStackEntryCount == 1)
            {
                var rootFrag = SupportFragmentManager.FindFragmentByTag(ViewTags.MAPLIST);

                if (rootFrag != null && rootFrag.IsVisible)
                    FinishAffinity();
            }

            else
                base.OnBackPressed();
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            Toggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            Toggle.OnConfigurationChanged(newConfig);
        }

        public void OnBackStackChanged()
        {
            var count = SupportFragmentManager.BackStackEntryCount.ToString();
            Logs.Instance.Debug($"Backstack: {count}");

            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                var currentFragment = SupportFragmentManager.Fragments.FirstOrDefault();
                SelectNavigationItem(currentFragment.Tag);
                Logs.Instance.Debug("Current Fragment: " + currentFragment.Tag);
            }
        }

        private void SelectNavigationItem(string tag)
        {
            switch (tag)
            {
                case ViewTags.SPOTLIST:
                    if (selectedItemId != Resource.Id.nav_spots)
                        NavigationView.Menu.GetItem(0).SetChecked(true);
                    break;
                case ViewTags.MAPLIST:
                    if (selectedItemId != Resource.Id.nav_map)
                        NavigationView.Menu.GetItem(1).SetChecked(true);
                    break;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case Constants.Location.PERMISSION_REQUEST_LOCATION:
                    // If request is cancelled, the result arrays are empty.
                    if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                    {
                        // permission was granted, yay! Do the
                        // task you need to do.
                        locationPermissionGranted = true;
                    }
                    else
                    {
                        // permission denied, boo! Disable the
                        // functionality that depends on this permission.
                        locationPermissionGranted = false;
                    }
                    break;

                    // other 'case' lines to check for other
                    // permissions this app might request.
            }
        }
    }

    public class NavigationItemSelectedTargetBinding : MvxConvertingTargetBinding<NavigationView, ICommand>
    {
        private ICommand _command;

        // used to figure out whether a subscription to MyPropertyChanged has been made
        private bool _subscribed;

        private NavigationView _target;

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        public NavigationItemSelectedTargetBinding(NavigationView view) : base(view)
        {
        }

        // describes how to set MyProperty on MyView
        protected override void SetValueImpl(NavigationView target, ICommand value)
        {
            _target = target;
            if (_target == null)
                return;

            _command = value;

            _subscribed = true;
            _target.NavigationItemSelected += View_Click;
        }

        private void View_Click(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            //e.Handled = true;
            object parameter = null;

            if (e.MenuItem.ItemId == Resource.Id.nav_spots)
                parameter = ViewModelTag.SpotListViewModel;
            else if (e.MenuItem.ItemId == Resource.Id.nav_map)
                parameter = ViewModelTag.SpotMapListViewModel;
            //else if (e.MenuItem.ItemId == Resource.Id.nav_timer)
            //    parameter = ViewModelTag.TimerViewModel;
            else if (e.MenuItem.ItemId == Resource.Id.nav_settings)
                parameter = ViewModelTag.SettingsViewModel;

            // update selected item
            MainActivity.selectedItemId = e.MenuItem.ItemId;

            _command.Execute(parameter);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                if (_target != null && _subscribed)
                {
                    _target.NavigationItemSelected -= View_Click;
                    _subscribed = false;
                }
            }
        }
    }
}