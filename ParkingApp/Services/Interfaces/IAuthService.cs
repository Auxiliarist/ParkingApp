namespace ParkingApp.Services.Interfaces
{
    public interface IAuthService
    {
        void Login();

        void Logout();

        void Signup();

        bool IsLoggedIn();
    }
}