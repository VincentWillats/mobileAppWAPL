using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Leaderboards : ContentPage
    { 
       
        public ObservableCollection<Data_LeaderboardEntry> LeaderboardEntries{ get; private set; }
        public ObservableCollection<int> Seasons { get; private set; }


        Controller_SQL sqlController = new Controller_SQL();

        public Page_Leaderboards()
        {            
            LeaderboardEntries = new ObservableCollection<Data_LeaderboardEntry>();
            Seasons = new ObservableCollection<int>();


            LoadSeasonList();
            BindingContext = this;

            InitializeComponent();
            //NameLabel.Text = player.FullName;

        }

        private async void LoadSesonLeaderboard(int seasonID)
        {
            activityIndicator.IsRunning = true;
            List<Data_LeaderboardEntry> _leaderboardEntry = new List<Data_LeaderboardEntry>();
            _leaderboardEntry = await sqlController.LoadLeaderboardStats(seasonID);
            foreach(Data_LeaderboardEntry entry in _leaderboardEntry)
            {
                LeaderboardEntries.Add(entry);
            }
            activityIndicator.IsRunning = false;
        }

        private async void LoadSeasonList()
        {
            List<int> _seasonsPlayedIn = new List<int>();
            _seasonsPlayedIn = await sqlController.LoadSeasonList();
            foreach (int season in _seasonsPlayedIn)
            {
                Seasons.Add(season);
            }
            SeasonPicker.SelectedIndex = 0;

        }
    

        private void SeasonPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            int seasonID;

            if (!int.TryParse(picker.SelectedItem.ToString(), out seasonID))
            {
                return;
            }
            else
            {
                LoadSesonLeaderboard(seasonID);
            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}