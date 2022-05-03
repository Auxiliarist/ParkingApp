using ParkingApp.Models;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace ParkingApp.Services.Interfaces
{
    public interface ILocationService
    {
        IConnectableObservable<Position> RequestPositionUpdates();

        IObservable<Position> LastPosition();

        Task<Address> GetAddressAsync(Position position);
    }
}