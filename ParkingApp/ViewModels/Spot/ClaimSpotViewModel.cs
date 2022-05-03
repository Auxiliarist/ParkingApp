using MvvmCross.ViewModels;
using ParkingApp.Models;
using ParkingApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Logging;

// Use ViewModel.Close();

namespace ParkingApp.ViewModels
{
    public class ClaimSpotViewModel : MvxViewModel<IMvxBundle, bool>
    {
        private readonly IGoogleMapsParser _mapParser;

        public double EndLat { get; private set; }
        public double EndLng { get; private set; }
        public List<List<Dictionary<string, string>>> ParsedData;

        public ClaimSpotViewModel(IGoogleMapsParser mapParser)
        {
            _mapParser = mapParser;
        }

        public override void Prepare(IMvxBundle parameter)
        {
            //throw new NotImplementedException();
        }

        public async Task ParseAsync()
        {
            var currentLat = 39.9288969; // ViewModel.LastPosition.Latitude;
            var currentLng = -75.2457533; // ViewModel.LastPosition.Longitude;
            Position position = new Position(currentLat, currentLng);

            var url = _mapParser.BuildDirectionsUrl(position, new Position(39.9350000, -75.2458522));
            Logs.Instance.Debug(url);

            var jsonData = await _mapParser.DownloadUrlAsync(url);

            ParsedData = await _mapParser.ParsePolylinesAsync(jsonData);
        }
    }
}
