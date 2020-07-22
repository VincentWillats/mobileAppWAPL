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

/* ViewModel for Page_Leaderboards
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
    class Page_LeaderboardsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool firstLoad = true;

        private bool _leaderboardLoading;
        private Data_LeaderboardEntry _objItemSelected;

        private List<int> _seasons;
        private int _selectedSeason;

        public int SelectedSeason
        {

            get { return _selectedSeason; }
            set
            {
                _selectedSeason = value;                
                OnPropertyChanged();    
                if(!firstLoad) { AudioController.PlayClick(); }
                LoadSesonLeaderboard(_selectedSeason);
            }
        }

        public bool LeaderboardLoading
        {
            get { return _leaderboardLoading; }
            set
            {
                _leaderboardLoading = value;
                OnPropertyChanged();
            }
        }

        public List<int> Seasons
        {
            get { return _seasons; }
            set
            {
                _seasons = value;
                OnPropertyChanged();
            }
        }

        public Data_LeaderboardEntry ObjItemSelected
        {
            get { return _objItemSelected; }
            set
            {
                if (_objItemSelected != value)
                {
                    _objItemSelected = value;
                    if (_objItemSelected != null)
                    {                        
                        LoadPlayerProfile(_objItemSelected.player, _objItemSelected.SeasonID);
                        _objItemSelected = null;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public Command SwipedBackCommand { get; }

        public ObservableCollection<Data_LeaderboardEntry> LeaderboardEntries { get; private set; }

        public Page_LeaderboardsViewModel()
        {   
            SwipedBackCommand = new Command(SwipedBack);
            LeaderboardEntries = new ObservableCollection<Data_LeaderboardEntry>();
            LoadSeasonList();
        }

        private async void LoadSesonLeaderboard(int seasonID)
        {
            LeaderboardLoading = true;
            LeaderboardEntries.Clear();
            int posCount = 1;
            List<Data_LeaderboardEntry> _leaderboardEntries = await Controller_SQL.LoadLeaderboardStats(seasonID);
            foreach (Data_LeaderboardEntry entry in _leaderboardEntries)
            {
                entry.Position = posCount;
                posCount++;
                LeaderboardEntries.Add(entry);
            }
            LeaderboardLoading = false;
        }

        private async void LoadPlayerProfile(Data_Player player, int seasonID)
        {
            AudioController.PlayClick();
            await Application.Current.MainPage.Navigation.PushAsync(new Page_PlayerProfile(player, seasonID));
            Analytics.TrackEvent("Viewed Player Profile", new Dictionary<string, string>
            {
                {"Player Name", player.FullName },
                {"Player ID", player.PlayerID.ToString() },
                {"Season", seasonID.ToString() }  
            });
        }

        private async void LoadSeasonList()
        {
            Seasons = await Controller_SQL.LoadSeasonList();
            SelectedSeason = Seasons[0];
            firstLoad = false;
        }

        private void SwipedBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
