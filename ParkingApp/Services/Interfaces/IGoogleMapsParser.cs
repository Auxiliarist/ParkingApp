using ParkingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingApp.Services.Interfaces
{
    public interface IGoogleMapsParser
    {
        string BuildDirectionsUrl(Position start, Position end);

        string BuildStaticMapUrl(Position start, double radius);

        Task<string> DownloadUrlAsync(string url);

        Task<List<List<Dictionary<string, string>>>> ParsePolylinesAsync(string json);
    }
}