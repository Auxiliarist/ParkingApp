using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android;
using ParkingApp.Models;
using ParkingApp.Services.Interfaces;
using Address = Android.Locations.Address;

namespace ParkingApp.Droid.Services
{
    public class LocationService : ILocationService
    {
        FusedLocationProviderClient client;
        SettingsClient settingsClient;
        LocationRequest request;
        LocationSettingsRequest settingsRequest;

        public LocationService()
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as AppCompatActivity;

            client = LocationServices.GetFusedLocationProviderClient(activity);
            settingsClient = LocationServices.GetSettingsClient(activity);

            CreateLocationRequest();
        }

        public async Task<Models.Address> GetAddressAsync(Position position)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as AppCompatActivity;

            var result = new Models.Address();

            Address address = new Address(Java.Util.Locale.Default);
            IList<Address> addressList = new List<Address>();

            try
            {
                addressList = await new Geocoder(activity).GetFromLocationAsync(position.Latitude, position.Longitude, 1);
                address = addressList.FirstOrDefault();
            }
            catch (Exception e)
            {

            }

            if (addressList.Count > 0)
            {
                result.AdminArea = address.AdminArea;
                result.CountryName = address.CountryName;
                result.Locality = address.Locality;
                result.AddressString = $"{address.SubThoroughfare} {address.Thoroughfare}, {address.Locality}";

                return result;
            }
            else
            {

            }

            return null;
        }

        public IObservable<Position> LastPosition()
        {
            Observable.Create<Position>(async observer =>
            {
                try
                {
                    var location = await client.GetLastLocationAsync();

                    Position position = new Position
                    {
                        Accuracy = location.Accuracy,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Time = location.Time
                    };

                    observer.OnNext(position);

                    return () => observer.OnCompleted();

                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                    return () => { };
                }
            });

            return null;
        }

        public IConnectableObservable<Position> RequestPositionUpdates()
        {
            Observable.Create<Position>(async observer =>
            {
                try
                {
                    // StopUpdates
                    var isStop = false;

                    // Check our state here to ensure location is turned on
                    await settingsClient.CheckLocationSettingsAsync(settingsRequest);

                    // Subscribe to location changes
                    var listener = new LocationListener(location =>
                    {
                        if (isStop)
                            return;

                        Position position = new Position
                        {
                            Accuracy = location.Accuracy,
                            Latitude = location.Latitude,
                            Longitude = location.Longitude,
                            Time = location.Time
                        };

                        observer.OnNext(position);
                    });

                    await client.RequestLocationUpdatesAsync(request, listener);

                    // Dispose
                    return async () =>
                    {
                        if (isStop)
                            return;

                        isStop = true;
                        await client.RemoveLocationUpdatesAsync(listener);
                        observer.OnCompleted();
                    };
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                    return () => { };
                }
            }).Publish();

            return null;
        }

        private void CreateLocationRequest()
        {
            request = new LocationRequest();

            // Sets the desired interval for active location updates. This interval is
            // inexact. You may not receive updates at all if no location sources are available, or
            // you may receive them slower than requested. You may also receive updates faster than 
            // requested if other applications are requesting location at a faster interval.
            request.SetInterval(Constants.Location.UPDATE_INTERVAL);

            // Sets the fastest rate for active location updates. This interval is exact, and your
            // application will never receive updates faster than this value.
            request.SetFastestInterval(Constants.Location.FASTEST_UPDATE_INTERVAL);

            request.SetPriority(LocationRequest.PriorityHighAccuracy);

            settingsRequest = new LocationSettingsRequest
                .Builder()
                .AddLocationRequest(request)
                .Build();
        }

        class LocationListener : LocationCallback
        {
            private readonly Action<Location> action;

            public LocationListener(Action<Location> action)
            {
                this.action = action;
            }

            public override void OnLocationResult(LocationResult result)
            {
                base.OnLocationResult(result);
                action(result.LastLocation);
            }
        }
    }
}