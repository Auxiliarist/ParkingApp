using Android.App;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.ViewModels;
using System.Threading.Tasks;
using MvvmCross.Logging;
using System.Collections.Generic;
using System;
using ParkingApp.Droid.Controls;
using Android.Support.Design.Widget;

namespace ParkingApp.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(LaunchMode = LaunchMode.SingleTop, Name = "parkingapp.droid.activities.ClaimSpotActivity")]
    public class ClaimSpotActivity : BaseActivity<ClaimSpotViewModel>, IOnMapReadyCallback,
        GoogleMap.IOnMapLoadedCallback, FloatingActionMenu.IOnMenuToggleListener, FloatingActionMenu.IOnMenuItemClickListener
    {
        protected override string TAG => "ClaimSpotActivity";
        protected override int LayoutResource => Resource.Layout.ClaimSpotActivity;

        private MapView mapView;
        private GoogleMap googleMap;

        private LatLng MyLocation, SpotLocation;

        private Bundle viewState;

        private FloatingActionMenu fabMenuMain;
        private FloatingActionButton fabMenuOne;
        private FloatingActionButton fabMenuTwo;
        private FloatingActionButton fabMenuThree;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            mapView = FindViewById<MapView>(Resource.Id.ClaimSpotMapView);
            mapView.SaveEnabled = true;

            fabMenuMain = FindViewById<FloatingActionMenu>(Resource.Id.ClaimSpotFabMenu);
            fabMenuOne = FindViewById<FloatingActionButton>(Resource.Id.ClaimSpotFabOne);
            fabMenuTwo = FindViewById<FloatingActionButton>(Resource.Id.ClaimSpotFabTwo);
            fabMenuThree = FindViewById<FloatingActionButton>(Resource.Id.ClaimSpotFabThree);

            if (viewState != null)
                mapView.OnCreate(viewState);
            else
                mapView.OnCreate(null);

            fabMenuMain.AddToggleListener(this);
            fabMenuMain.AddClickListener(this);
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            InitializeMap(googleMap);

            await ClaimSpotAsync();
        }

        public void OnMapLoaded()
        {
        }

        private void InitializeMap(GoogleMap map)
        {
            googleMap = map;

            // Enables or disables the my-location button
            googleMap.MyLocationEnabled = true;

            // Turns the 3D buildings layer on or off
            googleMap.BuildingsEnabled = true;
        }

        private async Task ClaimSpotAsync()
        {
            var currentLat = 39.9288969; // ViewModel.LastPosition.Latitude;
            var currentLng = -75.2457533; // ViewModel.LastPosition.Longitude;

            MyLocation = new LatLng(currentLat, currentLng);
            SpotLocation = new LatLng(39.9350000, -75.2458522);
            //SpotLocation = new LatLng(ViewModel.EndLat, ViewModel.EndLng);

            // Creating MarkerOptions
            MarkerOptions options = new MarkerOptions();

            // Add marker for spot
            options.SetPosition(SpotLocation);
            options.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed));
            options.SetTitle("Test");
            var marker = googleMap.AddMarker(options);
            //MarkerList.Add(marker, options);
            //KeyList.Add(ViewModel.Spot.Key);

            // Move map camera
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLng(MyLocation));

            //await ViewModel.ParseAsync();
            //await Task.Delay(1000);

            // Zoom map Camera
            var bounds = LatLngBounds.InvokeBuilder().Include(MyLocation).Include(SpotLocation).Build();
            var update = CameraUpdateFactory.NewLatLngBounds(bounds, 125);
            googleMap.AnimateCamera(update);

            // Get the polylines from the parsed data
            var polylines = await AddPolyLinesAsync(ViewModel.ParsedData);

            if (polylines != null)
            {
                var polyline = googleMap.AddPolyline(polylines);
                //PolylineList.Add(polyline, polylines);
            }
            else
                Logs.Instance.Debug("No Continuous Routes Available");
        }

        private async Task<PolylineOptions> AddPolyLinesAsync(List<List<Dictionary<string, string>>> result)
        {
            Logs.Instance.Debug("AddPolylinesAsync");

            List<LatLng> points;
            PolylineOptions lineOptions = null;

            await Task.Run(() =>
            {
                try
                {
                    // Traversing through all the routes
                    for (int i = 0; i < result.Count; i++)
                    {
                        points = new List<LatLng>();
                        lineOptions = new PolylineOptions();

                        // Fetching i-th route
                        List<Dictionary<string, string>> path = result[i];

                        // Fetching all the points in i-th route
                        for (int j = 0; j < path.Count; j++)
                        {
                            Dictionary<string, string> point = path[j];

                            double lat = System.Double.Parse(point["lat"]);
                            double lng = System.Double.Parse(point["lng"]);
                            LatLng position = new LatLng(lat, lng);

                            points.Add(position);
                        }

                        // Adding all the points in the route to LineOptions
                        foreach (LatLng p in points)
                            lineOptions.Add(p);
                        lineOptions.InvokeWidth(5f);
                        lineOptions.InvokeColor(Android.Graphics.Color.Blue);

                        Logs.Instance.Debug("Polylines decoded");
                    }
                }
                catch (Exception e)
                {
                    Logs.Instance.Error(e.Message);
                }
            });

            return lineOptions;
        }

        protected override void OnStart()
        {
            base.OnStart();
            mapView.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
            mapView.GetMapAsync(this);
        }

        protected override void OnStop()
        {
            mapView.OnStop();
            base.OnStop();
        }

        protected override void OnPause()
        {
            mapView.OnPause();
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            viewState = null;
            mapView.OnDestroy();
            fabMenuMain.RemoveToggleListener(this);
            fabMenuMain.RemoveClickListener(this);
            base.OnDestroy();
        }

        public void OnMenuToggle(bool opened)
        {
            Logs.Instance.Debug("fab menu opened: " + opened.ToString());
        }

        // On back button close menu if its open
        public void OnMenuItemClick(FloatingActionMenu floatingActionMenu, int index, FloatingActionButton item)
        {
            Logs.Instance.Debug("fab item index: " + index.ToString());
        }
    }
}