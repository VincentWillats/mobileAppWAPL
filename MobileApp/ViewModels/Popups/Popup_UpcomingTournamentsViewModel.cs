using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

/* ViewModel for Popup_Result
 * 
 * Creator:         Vincent Willats
 * Date Created:    19/05/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 19/05/2020 -- Moved to MVVM
 */

namespace MobileApp.ViewModels
{
    class Popup_UpcomingTournamentsViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        private Xamarin.Forms.Maps.Map _map01;
        private Pin _pin01;
        private MapSpan _mapSpan01;
        private Data_Tournament _upcomingTournament;

        public Xamarin.Forms.Maps.Map Map01
        {
            get { return _map01; }
            set
            {
                _map01 = value;
                OnPropertyChanged();
            }
        }

        public Data_Tournament UpcomingTournament
        {
            get { return _upcomingTournament; }
            set
            {
                _upcomingTournament = value;
                OnPropertyChanged();
            }
        }

        public Command SwipedBackCommand { get; }

        public Popup_UpcomingTournamentsViewModel(INavigation navigation, object selectedItem)
        {
            _navigation = navigation;
            UpcomingTournament = (Data_Tournament)selectedItem;
            SetMap();
            SwipedBackCommand = new Command(SwipedBack);
        }

        private async void SetMap()
        {
            var address = UpcomingTournament.Address01 + " " + UpcomingTournament.Address02 + " " +
                            UpcomingTournament.Address03 + " " + UpcomingTournament.Address04;

            var location = await Geocoding.GetLocationsAsync(address);
            var pos = new Position(location.FirstOrDefault().Latitude, location.FirstOrDefault().Longitude);
            _pin01 = new Pin
            {
                Type = PinType.Place,
                Label = UpcomingTournament.VenueName,
                Position = pos
            };
            _mapSpan01 = MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(2));
            Map01.MapType = MapType.Street;
            Map01.MoveToRegion(_mapSpan01);
            Map01.Pins.Add(_pin01);
        }

        private void SwipedBack()
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
