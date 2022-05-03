using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Models
{
    /// <summary>
    ///    Cross-platform abstraction of Android.Locations.Location 
    /// </summary>
    public class Position
    {
        public float Accuracy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Time { get; set; }

        public Position()
        {
        }

        public Position(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
