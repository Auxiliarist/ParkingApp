using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ParkingApp.Services.Implementations;
using ParkingApp.Services.Interfaces;

namespace ParkingApp
{
    public class App : MvxApplication
    {
        /// <summary> 
        ///    Breaking change in v6: This method is called on a background thread. Use Startup for
        ///    any UI bound actions
        /// </summary>
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //CreatableTypes()
            //    .EndingWith("Parser")
            //    .AsInterfaces()
            //    .RegisterAsDynamic();

            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterType<IMvxTextSerializer, MvxJsonConverter>();
            Mvx.RegisterType<IGoogleMapsParser, GoogleMapsParser>();

            RegisterCustomAppStart<AppStart>();
        }
    }
}