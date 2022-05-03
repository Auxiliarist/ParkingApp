namespace ParkingApp.Models
{
    public class Spot
    {
        public double Accuracy { get; set; }
        public string Address { get; set; }
        public float DistanceTo { get; set; }
        public string ImageUrl { get; set; }
        public string Key { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Timestamp { get; set; }
    }
}