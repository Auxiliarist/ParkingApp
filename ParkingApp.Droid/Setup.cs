using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.Preference;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Presenters;
using ParkingApp.Droid.Activities;
using ParkingApp.Droid.Services;
using ParkingApp.Services.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Reflection;

namespace ParkingApp.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(ViewPager).Assembly,
        };

        /// <summary>
        /// Fill the Binding Factory Registry with bindings from the support library.
        /// </summary>
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<NavigationView>
                ("NavigationItemSelected", v => new NavigationItemSelectedTargetBinding(v));

            MvxPreferenceSetupHelper.FillTargetFactories(registry);

            base.FillTargetFactories(registry);
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }

        public override MvxLogProviderType GetDefaultLogProviderType() => MvxLogProviderType.Serilog;

        protected override IMvxLogProvider CreateLogProvider()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.AndroidLog()
                .CreateLogger();

            return base.CreateLogProvider();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.LazyConstructAndRegisterSingleton<IAuthService, AuthService>();
            Mvx.LazyConstructAndRegisterSingleton<IFirebaseService, FirebaseService>();

            base.InitializeFirstChance();
        }
    }
}