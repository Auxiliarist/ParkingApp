using ParkingApp.Models;
using System.Threading.Tasks;

namespace ParkingApp.Services.Interfaces
{
    public interface IFirebaseService
    {
        void Initialize();

        void StartListener();

        void StopListener();

        Task AddSpot();

        Task DeleteSpot();

        Task RefreshSpots();

        bool SpotStillExists(Spot spot);
    }
}