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
        public Command TournamentClickedCommand { get; }

        public ObservableCollection<Data_Tournament> UpcomingTournaments { get; private set; }



        public Page_UpcomingTournamentsViewModel(INavigation navigation)
        {            
            _navigation = navigation;
            SwipedBackCommand = new Command(SwipedBack);
            TournamentClickedCommand = new Command<ListView>(async (x) => await TournamentClicked(x));
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


        private async Task TournamentClicked(ListView listview)
        {
            if(pageLoading) { return; }
            if (listview.SelectedItem != null)
            {
                pageLoading = true;
                Data_Tournament tourny = (Data_Tournament)listview.SelectedItem;
                System.Diagnostics.Debug.WriteLine(tourny.TournamentID.ToString());
                await _navigation.PushPopupAsync(new Popup_UpcomingTournaments(listview.SelectedItem));
                listview.SelectedItem = null;
                pageLoading = false;
            }        
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
