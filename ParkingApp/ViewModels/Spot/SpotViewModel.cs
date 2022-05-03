using MvvmCross.ViewModels;
using ReactiveUI;
using ParkingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using MvvmCross;

namespace ParkingApp.ViewModels
{
    public class SpotViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public Spot Spot { get; }

        public ReactiveCommand ViewSpotCommand { get; }
        public ReactiveCommand ClaimSpotCommand { get; }

        public SpotViewModel(Spot spot, IMvxNavigationService navigationService = null)
        {
            Spot = spot;

            _navigationService = navigationService ?? Mvx.Resolve<IMvxNavigationService>();
        }
    }
}
