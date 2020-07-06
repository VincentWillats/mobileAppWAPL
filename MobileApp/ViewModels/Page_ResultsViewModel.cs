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

/* ViewModel for Page_Results
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
    class Page_ResultsViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool pageLoading = false;
        private bool _tournamentsLoading;
        private Data_Result _objItemSelected;

        public bool TournamentsLoading
        {
            get { return _tournamentsLoading; }
            set
            {
                _tournamentsLoading = value;
                OnPropertyChanged();
            }
        }       

        public Data_Result ObjItemSelected
        {
            get { return _objItemSelected; }
            set
            {
                if (_objItemSelected != value)
                {
                    _objItemSelected = value;
                    if(_objItemSelected != null)
                    {                        
                        TournamentClicked(_objItemSelected);
                        _objItemSelected = null;
                        OnPropertyChanged();
                    }                    
                }                
            }
        }

        public Command SwipedBackCommand { get; }

        public ObservableCollection<Data_Result> Results { get; private set; }

        public Page_ResultsViewModel(INavigation navigation)
        {           
            
            _navigation = navigation;
            SwipedBackCommand = new Command(SwipedBack);
            Results = new ObservableCollection<Data_Result>();
            LoadResults();
        }

        private async void LoadResults()
        {
            TournamentsLoading = true;
            List<Data_Result> _results = await Controller_SQL.LoadResultsAsync();
            foreach (Data_Result result in _results)
            {
                Results.Add(result);
            }
            TournamentsLoading = false;
        }

        private async void TournamentClicked(Data_Result result)
        {
            if (pageLoading) { return; }  
            pageLoading = true;
            AudioController.PlayClick();
            await _navigation.PushPopupAsync(new Popup_Result(result));
            Analytics.TrackEvent("Viewed Tournament Result", new Dictionary<string, string>
            {
                {"Tournament Date", result.tourny.RegoDate.Date.ToShortDateString()},
                {"Tournament ID", result.tourny.TournamentID.ToString() },
                {"Tournament Venue", result.tourny.VenueName },
                {"Tournament Buyin", result.tourny.Buyin.ToString() },
                {"Winner Name", result.winner.FullName },
                {"Winner ID", result.winner.PlayerID.ToString() },
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
