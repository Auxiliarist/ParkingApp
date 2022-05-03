using System;
using System.Threading;
using Android.App;
using Android.Gms.Maps;
using Android.Runtime;
using Android.Util;
using ParkingApp.Constants;

namespace ParkingApp.Droid
{
    [MetaData("com.google.android.maps.v2.API_KEY", Value = ApiKeys.GOOGLE_MAPS_ANDROID_API_KEY)]
    [MetaData("com.google.android.gms.version", Resource = "@integer/google_play_services_version")]
    [Application]
    public class MainApplication : Application
    {
        public MainApplication()
        {
        }

        protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            MapsInitializer.Initialize(this);

            Log.Debug("MAPS", "Initialized");

            new Thread(() =>
            {
                try
                {
                    MapView mv = new MapView(this);
                    mv.OnCreate(null);
                    mv.OnResume();
                    mv.OnPause();
                    mv.OnDestroy();
                }
                catch (Exception ignore)
                {
                    Log.Error("MapException", ignore.Message);
                }
            }).Start();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
        }
    }
}