using Firebase.Database;
using GeoFire.Xamarin.Android;
using ParkingApp.Models;
using ParkingApp.Services.Interfaces;
using System;
using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android;
using Android.Support.V7.App;
using MvvmCross;
using ParkingApp.ViewModels;
using System.Linq;

namespace ParkingApp.Droid.Services
{
    public class FirebaseService : IFirebaseService, IGeoQueryDataEventListener
    {
        public DatabaseReference database;
        public GeoQuery GeoQuery;

        public FirebaseService()
        {
        }

        public void Initialize()
        {
            Logs.Instance.Debug("Firebase - Initialize");

            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as AppCompatActivity;
            Firebase.FirebaseApp.InitializeApp(activity);

            database = FirebaseDatabase.Instance.Reference.Child("Users_Spots");
        }

        // Change QueryAtLocation
        public void StartListener()
        {
            Logs.Instance.Debug("Firebase - StartListener");

            var geofire = new Geofire(database);

            GeoQuery = geofire.QueryAtLocation(new GeoLocation(39.9287942, -75.2457803), 5);
            GeoQuery.AddGeoQueryDataEventListener(this);
        }

        public void StopListener()
        {
            GeoQuery.RemoveAllListeners();
            GeoQuery = null;
        }

        public async Task AddSpot()
        {
            var geofire = new Geofire(database);

            var userId = "UserId1";
            var address = "923 Macdade Boulevard, Yeadon";
            double accuracy = 9;

            await geofire.SetSpotAsync(userId, address, accuracy, new GeoLocation(39.9287942, -75.2457803));
            //await geofire.SetSpotAsync("UserId2", "927 Macdade Boulevard, Yeadon", accuracy, new GeoLocation(39.9339685, -75.2457803));
            //MainViewModel.Data.Add(new SpotViewModel(new Spot() { Address = "927 Macdade Boulevard, Yeadon", Accuracy = 8, Latitude = 39.9339685, Longitude = -75.2458522, Key = "-KzebLHeo23TOOsTUkl7", Timestamp = 1511469704482 }));
        }

        public async Task DeleteSpot()
        {
            var geofire = new Geofire(database);

            await geofire.RemoveLocationAsync(null);
        }

        public Task RefreshSpots()
        {
            throw new NotImplementedException();
        }

        public bool SpotStillExists(Spot spot)
        {
            throw new NotImplementedException();
        }

        public void OnGeoQueryReady()
        {
            Logs.Instance.Debug("Geofire - All initial key entered events have been fired");
        }

        public void OnDataChanged(DataSnapshot dataSnapshot, GeoLocation location)
        {
            Logs.Instance.Debug($"Geofire - OnDataChanged: {dataSnapshot.Key} - [{location.Latitude}], [{location.Longitude}]");
        }

        public void OnDataEntered(DataSnapshot dataSnapshot, GeoLocation location)
        {
            Logs.Instance.Debug($"Geofire - {dataSnapshot.Key} entered at {location.Latitude}, {location.Longitude}");

            var position = new Position(location.Latitude, location.Longitude);
            var accuracy = (double)dataSnapshot?.Child("p")?.Value;

            Spot spot = new Spot
            {
                Accuracy = accuracy,
                Address = (string)dataSnapshot?.Child("a")?.Value,
                //ImageUrl = url,
                Key = dataSnapshot.Key,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Timestamp = (long)dataSnapshot?.Child("t")?.Value
            };

            MainViewModel.Data.Add(new SpotViewModel(spot));
        }

        public void OnDataExited(DataSnapshot dataSnapshot)
        {
            Logs.Instance.Debug($"Geofire - {dataSnapshot.Key} is no longer in the search area");

            var key = dataSnapshot.Key;

            MainViewModel.Data.Remove(MainViewModel.Data.Where(item => item.Spot.Key == key).First());
        }

        public void OnDataMoved(DataSnapshot dataSnapshot, GeoLocation location)
        {
            Logs.Instance.Debug($"Geofire - {dataSnapshot.Key} moved to {location.Latitude}, {location.Longitude}");

            throw new NotImplementedException();
        }

        public void OnGeoQueryError(DatabaseError error)
        {
            Logs.Instance.Debug($"Geofire - Error: {error.Message}");

            throw new NotImplementedException();
        }

    }
}