using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.Constants;
using ParkingApp.ViewModels;
using System.Collections.Generic;
using MvvmCross.Logging;
using System.Linq;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using System;
using System.Windows.Input;
using Android.Widget;
using Android.Support.Design.Widget;
using System.Reactive.Linq;
using MvvmCross.Binding.BindingContext;
using Android.Support.Constraints;

namespace ParkingApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), fragmentContentId: Resource.Id.content_main, addToBackStack: true, isCacheableFragment: true)]
    [Register(ViewTags.MAPLIST)]
    public class SpotMapListView : BaseFragment<SpotMapListViewModel>, IOnMapReadyCallback, 
        GoogleMap.IOnMapLoadedCallback, GoogleMap.IOnMarkerClickListener
    {
        protected override int FragmentLayoutResource => Resource.Layout.SpotMapListView;

        ConstraintLayout bottomSheetView;
        BottomSheetBehavior bottomSheet;

        TextView addressText;
        TextView timeText;
        Button claimSpotButton;

        private MapView mapView;
        private GoogleMap googleMap;

        private Dictionary<Marker, MarkerOptions> MarkerList;
        private Dictionary<Circle, CircleOptions> CircleList;
        private Dictionary<Polyline, PolylineOptions> PolylineList;
        private List<string> KeyList;

        private Bundle viewState;

        IDisposable addSub;
        IDisposable removeSub;

        bool IsInfoWindowShown = false;
        int MarkerShownIndex = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //if (ContextCompat.CheckSelfPermission(ParentActivity, Manifest.Permission.AccessFineLocation) != Permission.Granted)
            //{
            //    // Permission is not granted
            //    Logs.Instance.Error("Location Permission not granted");
            //    Activities.MainActivity.locationPermissionGranted = false;
            //    ActivityCompat.RequestPermissions(ParentActivity, new string[] { Android.Manifest.Permission.AccessFineLocation }, Constants.Location.PERMISSION_REQUEST_LOCATION);
            //}
            //else
            //{
            //    Logs.Instance.Debug("Location Permission is granted");
            //    Activities.MainActivity.locationPermissionGranted = true;
            //}
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            HasOptionsMenu = true;

            mapView = view.FindViewById<MapView>(Resource.Id.SpotMapListView);
            mapView.SaveEnabled = true;

            bottomSheetView = view.FindViewById<ConstraintLayout>(Resource.Id.SpotMapListViewSheet);
            bottomSheet = BottomSheetBehavior.From(bottomSheetView);

            bottomSheet.State = BottomSheetBehavior.StateHidden;

            addressText = view.FindViewById<TextView>(Resource.Id.SpotMapListViewSheetAddress);
            timeText = view.FindViewById<TextView>(Resource.Id.SpotMapListViewSheetTime);
            claimSpotButton = view.FindViewById<Button>(Resource.Id.SpotMapListViewSheetClaim);

            if (viewState != null)
                mapView.OnCreate(viewState);
            else
                mapView.OnCreate(null);

            //GetMapAsync called in OnResume()

            var set = this.CreateBindingSet<SpotMapListView, SpotMapListViewModel>();
            set.Bind(claimSpotButton).To(vm => vm.ClaimSpotCommand);
            set.Apply();

            return view;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            InitializeMap(googleMap);

            mapView.Visibility = ViewStates.Visible;

            PopulateMapAsync();
        }

        private void InitializeMap(GoogleMap map)
        {
            googleMap = map;
            
            // Enables or disables the my-location button
            googleMap.MyLocationEnabled = true;

            // Turns the 3D buildings layer on or off
            googleMap.BuildingsEnabled = true;
            
            // Switch to night mode
            //googleMap.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(Context, Resource.Raw.mapstyle_night));

            googleMap.SetOnMapLoadedCallback(this);
            googleMap.SetOnMarkerClickListener(this);

            //var markerEvent = Observable.FromEventPattern<GoogleMap.MarkerClickEventArgs>(h => googleMap.MarkerClick += h, h => googleMap.MarkerClick -= h);

            //var markerSub = markerEvent.Subscribe(args => { });
        }

        private void PopulateMapAsync()
        {
            MarkerList = new Dictionary<Marker, MarkerOptions>();
            CircleList = new Dictionary<Circle, CircleOptions>();
            PolylineList = new Dictionary<Polyline, PolylineOptions>();
            KeyList = new List<string>();

            DrawSpotListOverlay(39.9288969, -75.2457533);
            
            addSub = MainViewModel.Data.ItemsAdded.Subscribe(item =>
            {
                MarkerOptions options = new MarkerOptions()
                    .SetPosition(new LatLng(item.Spot.Latitude, item.Spot.Longitude))
                    .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed))
                    .SetTitle(item.Spot.Address)
                    .SetSnippet("Test");

                var marker = googleMap.AddMarker(options);

                MarkerList.Add(marker, options);
                KeyList.Add(item.Spot.Key);
            });

            removeSub = MainViewModel.Data.ItemsRemoved.Subscribe(item =>
            {
                var index = KeyList.IndexOf(item.Spot.Key);
                var marker = MarkerList.Keys.ToList().ElementAt(index);

                marker.Remove();

                MarkerList.Remove(marker);
                KeyList.RemoveAt(index);
            });
        }

        private void DrawSpotListOverlay(double lat, double lng)
        {
            var MyLocation = new LatLng(lat, lng);

            foreach (var item in MainViewModel.Data)
            {
                MarkerOptions options = new MarkerOptions()
                   .SetPosition(new LatLng(item.Spot.Latitude, item.Spot.Longitude))
                   .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed))
                   .SetTitle(item.Spot.Address);

                var marker = googleMap.AddMarker(options);
                MarkerList.Add(marker, options);
                KeyList.Add(item.Spot.Key);
            }

            var strokeColor = Color.Blue;
            var fillColor = Color.LightBlue;
            strokeColor.A = 100;
            fillColor.A = 75;

            CircleOptions circleOptions = new CircleOptions()
                .Clickable(false)
                .InvokeCenter(MyLocation)
                .InvokeRadius(1000)
                .InvokeStrokeColor(strokeColor)
                .InvokeFillColor(fillColor)
                .InvokeStrokeWidth(1f);

            var circle = googleMap.AddCircle(circleOptions);
            CircleList.Add(circle, circleOptions);

            // No end location so just zoom in on current location
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(MyLocation, 14f);
            googleMap.MoveCamera(cameraUpdate);
        }

        public void OnMapLoaded()
        {
            Logs.Instance.Debug("OnMapLoaded");
        }

        public bool OnMarkerClick(Marker marker)
        {
            if (!IsInfoWindowShown)
            {
                marker.ShowInfoWindow();
                IsInfoWindowShown = true;
                
                MarkerShownIndex = MarkerList.Keys
                    .ToList()
                    .FindIndex(m => m.Id == marker.Id);

                addressText.Text = marker.Title;

                bottomSheet.State = BottomSheetBehavior.StateCollapsed;

                return true;

            }
            else if (IsInfoWindowShown)
            {
                marker.HideInfoWindow();
                IsInfoWindowShown = false;

                bottomSheet.State = BottomSheetBehavior.StateHidden;

                return true;
            }

            return false;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.actionbar_spot, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    ViewModel.Command.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnStart()
        {
            base.OnStart();
            mapView.OnStart();
        }

        public override void OnResume()
        {
            base.OnResume();

            mapView.OnResume();

            if (ContextCompat.CheckSelfPermission(ParentActivity, Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                // Permission is not granted
                Logs.Instance.Error("Location Permission not granted");
                Activities.MainActivity.locationPermissionGranted = false;
                ActivityCompat.RequestPermissions(ParentActivity, new string[] { Android.Manifest.Permission.AccessFineLocation }, Constants.Location.PERMISSION_REQUEST_LOCATION);
            }
            else
            {
                Logs.Instance.Debug("Location Permission is granted");
                Activities.MainActivity.locationPermissionGranted = true;
                mapView.GetMapAsync(this);
            }

            //if (Activities.MainActivity.locationPermissionGranted)
            //{
            //    mapView.GetMapAsync(this);
            //}
        }

        public override void OnStop()
        {
            mapView.OnStop();
            base.OnStop();
        }

        public override void OnPause()
        {
            mapView.OnPause();
            base.OnPause();
        }

        public override void OnDestroy()
        {
            addSub.Dispose();
            removeSub.Dispose();
            mapView.OnDestroy();
            viewState = null;
            base.OnDestroy();
        }
    }
}