using Microsoft.AppCenter.Analytics;
using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

/* ViewModel for Page_UpcomingTournaments
 * 
 * Creator:         Vincent Willats
 * Date Created:    17/05/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 17/05/2020 -- Moved to MVVM
 */

namespace MobileApp.ViewModels
{
    class Page_UpcomingTournamentsViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool pageLoading = false;
        private bool _tournamentsLoading;
        private bool _noUpcomingTournaments;
        private Data_Tournament _objItemSelected;

        public Data_Tournament ObjItemSelected
        {
            get { return _objItemSelected; }
            set
            {
                if (_objItemSelected != value)
                {
                    _objItemSelected = value;
                    if (_objItemSelected != null)
                    {
                        TournamentClicked(_objItemSelected);
                        _objItemSelected = null;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public bool TournamentsLoading
        {
            get { return _tournamentsLoading; }
            set
            {
                _tournamentsLoading = value;
                OnPropertyChanged();
            }
        }

        public bool NoUpcomingTournaments
        {
            get { return _noUpcomingTournaments; }
            set
            {
                _noUpcomingTournaments = value;
                OnPropertyChanged();
            }
        }

        public Command SwipedBackCommand { get; }

        public ObservableCollection<Data_Tournament> UpcomingTournaments { get; private set; }

        public Page_UpcomingTournamentsViewModel(INavigation navigation)
        {            
            _navigation = navigation;
            SwipedBackCommand = new Command(SwipedBack);
            UpcomingTournaments = new ObservableCollection<Data_Tournament>();
            LoadUpcomingTournaments();
        }

        private async void LoadUpcomingTournaments()
        {
            TournamentsLoading = true;
            List<Data_Tournament> _tournies = await Controller_SQL.LoadUpcomingTournamentsAsync();
            foreach (Data_Tournament tourny in _tournies)
            {
                UpcomingTournaments.Add(tourny);
            }
            TournamentsLoading = false;
            if (UpcomingTournaments.Count == 0)
            {
                NoUpcomingTournaments = true;
            }
        }

        private async void TournamentClicked(Data_Tournament tourny)
        {
            if(pageLoading) { return; }            
            await OpenUpcomingTournament(tourny);
        }

        private async Task OpenUpcomingTournament(Data_Tournament tourny)
        {
            pageLoading = true;
            AudioController.PlayClick();
            await _navigation.PushPopupAsync(new Popup_UpcomingTournaments(tourny));
            Analytics.TrackEvent("Viewed Tournament", new Dictionary<string, string>
                    {
                        {"Tournament Date", tourny.RegoDate.Date.ToShortDateString()}, 
                        {"Tournament ID", tourny.TournamentID.ToString() }, 
                        {"Tournament Venue", tourny.VenueName },  
                        {"Tournament Buyin", tourny.Buyin.ToString() }
                    });
            pageLoading = false;
        }

        private void SwipedBack()
        {
            _navigation.PopAsync();
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
