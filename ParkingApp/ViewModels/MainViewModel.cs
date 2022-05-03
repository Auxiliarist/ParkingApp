using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ParkingApp.Constants;
using ParkingApp.Models;
using ParkingApp.Services.Interfaces;
using ReactiveUI;
using System.Threading.Tasks;

namespace ParkingApp.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigation;

        private static ReactiveList<SpotViewModel> _data;

        public static ReactiveList<SpotViewModel> Data
        {
            get => _data ?? (_data = new ReactiveList<SpotViewModel>()
            {
                //new SpotViewModel(new Spot() { Address = "923 Macdade Boulevard, Yeadon", Accuracy = 6, Latitude = 39.9287942, Longitude = -75.2457803, Key = "-KzebJKnzE6x4o_efgTp", Timestamp = 1511469696452 }),
                //new SpotViewModel(new Spot() { Address = "925 Macdade Boulevard, Yeadon", Accuracy = 6, Latitude = 39.9350000, Longitude = -75.2500000, Key = "-KzebKTidXdCrZW-somJ", Timestamp = 1511469701152 }),
                //new SpotViewModel(new Spot() { Address = "926 Macdade Boulevard, Yeadon", Accuracy = 8, Latitude = 39.9350000, Longitude = -75.2458522, Key = "-KzebLHeo23TOOsTUkl6", Timestamp = 1511469704482 }),
            }); set => _data = value;
        }

        public ReactiveCommand NavCommand { get; }

        public MainViewModel(IMvxNavigationService navigation)
        {
            _navigation = navigation;

            NavCommand = ReactiveCommand.CreateFromTask<ViewModelTag>(async tag => await NavigateAsync(tag));
        }

        private async Task NavigateAsync(ViewModelTag tag)
        {
            switch (tag)
            {
                case ViewModelTag.SpotListViewModel:
                    await _navigation.Navigate<SpotListViewModel>();
                    break;
                case ViewModelTag.SpotMapListViewModel:
                    await _navigation.Navigate<SpotMapListViewModel>();
                    break;
                case ViewModelTag.SettingsViewModel:
                    await _navigation.Navigate<SettingsViewModel>();
                    break;
            }
        }

        public override async Task Initialize()
        {
            Mvx.Resolve<IFirebaseService>().Initialize();

            await base.Initialize();
        }

        public override void ViewCreated()
        {
            base.ViewCreated();

            Data.Clear();

            Mvx.Resolve<IFirebaseService>().StartListener();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            // stop location updates
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            Mvx.Resolve<IFirebaseService>().StopListener();
            base.ViewDestroy(viewFinishing);
        }
    }
}