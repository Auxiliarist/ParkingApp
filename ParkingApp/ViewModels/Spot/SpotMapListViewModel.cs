using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ParkingApp.Services.Interfaces;
using ReactiveUI;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkingApp.ViewModels
{
    public class SpotMapListViewModel : MvxViewModel
    {
        private readonly IMvxJsonConverter _jsonConverter;
        private readonly IGoogleMapsParser _mapParser;
        private readonly IMvxNavigationService _navigationService;

        public IMvxCommand AddSpotCommand { get; }
        public ICommand Command { get; }
        public ReactiveCommand ClaimSpotCommand { get; }

        public SpotMapListViewModel(IMvxNavigationService navigationService = null)
        {
            _navigationService = navigationService ?? Mvx.Resolve<IMvxNavigationService>();

            AddSpotCommand = new MvxAsyncCommand(async token => await AddSpotAsync(token));
            Command = ReactiveCommand.CreateFromTask(async token => await AddSpotAsync(token));

            ClaimSpotCommand = ReactiveCommand.CreateFromTask(async () => await _navigationService.Navigate<ClaimSpotViewModel, IMvxBundle>(param: ClaimBundle()));
        }

        private async Task AddSpotAsync(CancellationToken token)
        {
            await Task.Delay(2000);

            Logs.Instance.Debug("AddSpotAsync");
        }

        private IMvxBundle ClaimBundle()
        {
            MvxBundle bundle = new MvxBundle();
            //bundle.Data["ClaimSpot"] = _jsonConverter.SerializeObject(spot);

            return bundle;
        }
    }
}