using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_PlayerProfile : ContentPage
    {
        Data_Player player = new Data_Player();
        int seasonID = -1;
        public Data_PlayerStats PlayerStats { get; private set; }
        public ObservableCollection<Data_Stat> StatsToShow { get; private set; }
        public ObservableCollection<int> SeasonsPlayedIn { get; private set; }

        Controller_SQL sqlController = new Controller_SQL();

        public Page_PlayerProfile(object playerObj)
        {
            player = (Data_Player)playerObj;
            StatsToShow = new ObservableCollection<Data_Stat>();
            SeasonsPlayedIn = new ObservableCollection<int>();
            
            LoadPlayerSeasonsPlayedList(player.PlayerID); 
            BindingContext = this;

            InitializeComponent();
            NameLabel.Text = player.FullName;
        }
        public Page_PlayerProfile(object playerObj, int _seasonID)
        {
            player = (Data_Player)playerObj;
            seasonID = _seasonID;
            StatsToShow = new ObservableCollection<Data_Stat>();
            SeasonsPlayedIn = new ObservableCollection<int>();

            LoadPlayerSeasonsPlayedList(player.PlayerID);
            BindingContext = this;

            InitializeComponent();
            NameLabel.Text = player.FullName;
        }
        private async void LoadPlayerStats(int playerID, int seasonID)
        {
            activityIndicator.IsRunning = true;
            Data_PlayerStats _playerStats = new Data_PlayerStats();
            _playerStats = await sqlController.LoadPlayerStats(playerID, seasonID);
            PlayerStats = _playerStats;
            PlayerStats.Player.FullName = player.FullName;
            AddStatsToShow();  
            activityIndicator.IsRunning = false;
        }
        private async void LoadPlayerSeasonsPlayedList(int playerID)
        {
            List<int> _seasonsPlayedIn = new List<int>();
            _seasonsPlayedIn = await sqlController.LoadPlayerPlayedSeasons(playerID);
            foreach(int season in _seasonsPlayedIn)
            {
                SeasonsPlayedIn.Add(season);
            }          
            if(seasonID == -1)
            {
                SeasonPicker.SelectedIndex = 0;
            }
            else
            {
                SeasonPicker.SelectedItem = seasonID;
            }
            

        }
        private void AddStatsToShow()
        {
            StatsToShow.Clear();
            decimal in_this_PCT_to_Show = 0.25M;
            StatsToShow.Add(new Data_Stat("Total Games", PlayerStats.Total_Games.ToString()));
            StatsToShow.Add(new Data_Stat("Total Games Rank", PlayerStats.Total_Games_RankNo.ToString()));
            StatsToShow.Add(new Data_Stat("Total Games in top", PlayerStats.Total_Games_In_TopPCT_Str + " of Players"));
            StatsToShow.Add(new Data_Stat("Total Wins", PlayerStats.Wins_Amount.ToString()));
            if(PlayerStats.Wins_Amount_In_TopPCT < in_this_PCT_to_Show)
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
        private void SeasonPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            int seasonID;
            Data_PlayerStats _playerStats = new Data_PlayerStats();
            PlayerStats = _playerStats;
            activityIndicator.IsRunning = true;
            if (!int.TryParse(picker.SelectedItem.ToString(), out seasonID))
            {
                return;
            }
            else
            {     
                LoadPlayerStats(player.PlayerID, seasonID);
            }            
        }
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}