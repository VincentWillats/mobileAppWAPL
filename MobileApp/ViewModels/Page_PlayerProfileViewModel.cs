using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

/* ViewModel for Page_PlayerProfile
 * 
 * Creator:         Vincent Willats
 * Date Created:    19/05/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 22/05/2020 -- Added click sounds
 * 19/05/2020 -- Moved to MVVM
 */

namespace MobileApp.ViewModels
{
    class Page_PlayerProfileViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        private int _seasonPicked = -1;
        private bool _statsLoading;
        private bool firstLoad = true;
        private Data_Player _player;
        private string _playerName;

        public Data_PlayerStats PlayerStats { get; private set; }
        public ObservableCollection<Data_Stat> StatsToShow { get; private set; }

        private List<int> _seasonsIn;

        public List<int> SeasonsIn
        {
            get { return _seasonsIn; }
            set
            {
                _seasonsIn = value;
                OnPropertyChanged();
            }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        public bool StatsLoading
        {
            get { return _statsLoading; }
            set
            {
                _statsLoading = value;
                OnPropertyChanged();
            }
        }

        public int SeasonPicked
        {
            get { return _seasonPicked; }
            set
            {                
                if(value >= 1)
                {                    
                    _seasonPicked = value;
                    OnPropertyChanged();                    
                    LoadPlayerStats(_player.PlayerID, _seasonPicked);
                }

            }
        }

        public Command SwipedBackCommand { get; }


        public Page_PlayerProfileViewModel(INavigation navigation, object playerObj)
        {
            _navigation = navigation;
            //firstLoad = true;
            _player = (Data_Player)playerObj;
            StatsToShow = new ObservableCollection<Data_Stat>();            

            LoadPlayerSeasonsPlayedList(_player.PlayerID);
            PlayerName = _player.FullName;

            SwipedBackCommand = new Command(SwipedBack);
        }

        public Page_PlayerProfileViewModel(INavigation navigation, object playerObj, int seasonID)
        {
            _navigation = navigation;
            _player = (Data_Player)playerObj;            
            StatsToShow = new ObservableCollection<Data_Stat>();
            _seasonPicked = seasonID;

            LoadPlayerSeasonsPlayedList(_player.PlayerID);
            PlayerName = _player.FullName;            
        }

        private async void LoadPlayerSeasonsPlayedList(int playerID)
        {           
            SeasonsIn = await Controller_SQL.LoadPlayerPlayedSeasons(playerID);   
            if(SeasonsIn.Count <= 0) { return; }
            SeasonPicked = (_seasonPicked != -1) ? SeasonsIn[SeasonsIn.IndexOf(_seasonPicked)] : SeasonsIn[0];
            firstLoad = false;
        }

        private async void LoadPlayerStats(int playerID, int seasonID)
        {
            if (seasonID == -1) { return; }
            StatsLoading = true;            
            if (firstLoad == false) { AudioController.PlayClick(); }            
            Data_PlayerStats _playerStats = await Controller_SQL.LoadPlayerStats(playerID, seasonID);        
            PlayerStats = _playerStats;
            PlayerStats.Player = _player;
            AddStatsToShow();

            StatsLoading = false;
        }

        private void AddStatsToShow()
        {
            StatsToShow.Clear();
            decimal in_this_PCT_to_Show = 0.25M;
            StatsToShow.Add(new Data_Stat("Total Games", PlayerStats.Total_Games.ToString()));
            StatsToShow.Add(new Data_Stat("Total Games Rank", PlayerStats.Total_Games_RankNo.ToString()));
            StatsToShow.Add(new Data_Stat("Total Games in top", PlayerStats.Total_Games_In_TopPCT_Str + " of Players"));
            StatsToShow.Add(new Data_Stat("Total Wins", PlayerStats.Wins_Amount.ToString()));
            if (PlayerStats.Wins_Amount_In_TopPCT < in_this_PCT_to_Show)
            {
                StatsToShow.Add(new Data_Stat("Total Wins Rank", PlayerStats.Wins_Amount_RankNo.ToString()));
                StatsToShow.Add(new Data_Stat("Total Wins in top", PlayerStats.Wins_Amount_In_TopPCT_Str + " of Players"));
            }
            StatsToShow.Add(new Data_Stat("Win Percent", PlayerStats.Overall_Win_PCT_Str));
            if (PlayerStats.Overall_Win_PCT_In_TopPCT < in_this_PCT_to_Show)
            {
                StatsToShow.Add(new Data_Stat("Win Percent Rank", PlayerStats.Overall_Win_PCT_RankNo.ToString()));
                StatsToShow.Add(new Data_Stat("Win Perecnt in top", PlayerStats.Overall_Win_PCT_In_TopPCT_Str + " of Players"));
            }
            StatsToShow.Add(new Data_Stat("Total Heads Up", PlayerStats.HeadsUp_Amount.ToString()));
            if (PlayerStats.HeadsUp_Amount_In_TopPCT < in_this_PCT_to_Show)
            {
                StatsToShow.Add(new Data_Stat("Total Heads Up Rank", PlayerStats.HeadsUp_Amount_RankNo.ToString()));
                StatsToShow.Add(new Data_Stat("Total Heads Up in top", PlayerStats.HeadsUp_Amount_In_TopPCT_Str + " of Players"));
            }
            StatsToShow.Add(new Data_Stat("Heads Up Win Percent", PlayerStats.HeadsUp_Win_PCT_Str));
            if (PlayerStats.HeadsUp_Win_PCT_In_TopPCT < in_this_PCT_to_Show)
            {
                StatsToShow.Add(new Data_Stat("Heads Up Win Percent Rank", PlayerStats.HeadsUp_Win_PCT_RankNo.ToString()));
                StatsToShow.Add(new Data_Stat("Heads Up Win Percent in top", PlayerStats.HeadsUp_Win_PCT_In_TopPCT_Str + " of Players"));
            }
            StatsToShow.Add(new Data_Stat("Total Final Tables", PlayerStats.FinalTable_Amount.ToString()));
            if (PlayerStats.FinalTable_Amount_In_TopPCT < in_this_PCT_to_Show)
            {
                StatsToShow.Add(new Data_Stat("Total Final Tables Rank", PlayerStats.FinalTable_Amount_RankNo.ToString()));
                StatsToShow.Add(new Data_Stat("Total Final Tables in top", PlayerStats.FinalTable_Amount_In_TopPCT_Str + " of Players"));
            }
            StatsToShow.Add(new Data_Stat("Final Table Win Percent", PlayerStats.FinalTable_Win_PCT_Str));
            if (PlayerStats.FinalTable_Win_PCT_In_TopPCT < in_this_PCT_to_Show)
            {
                StatsToShow.Add(new Data_Stat("Total Final Tables Rank", PlayerStats.FinalTable_Win_PCT_RankNo.ToString()));
                StatsToShow.Add(new Data_Stat("Total Final Tables in top", PlayerStats.FinalTable_Win_PCT_In_TopPCT_Str + " of Players"));
            }
            //if (PlayerStats.Avg_Pct_Field_Beaten < ?????
            //{
            StatsToShow.Add(new Data_Stat("Average Percent of field Beaten", PlayerStats.Avg_Pct_Field_Beaten_Str));
            //}
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
