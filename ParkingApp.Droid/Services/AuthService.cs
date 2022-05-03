using ParkingApp.Services.Interfaces;

namespace ParkingApp.Droid.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {
        }

        public bool IsLoggedIn()
        {
            return true;
        }

        public void Login()
        {
            throw new System.NotImplementedException();
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }

        public void Signup()
        {
            throw new System.NotImplementedException();
        }
    }
}