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
        FontSizeController fontController = new FontSizeController();
        public ObservableCollection<Data_LeaderboardEntry> LeaderboardEntries { get; private set; }
        public ObservableCollection<int> Seasons { get; private set; }

        Data_LeaderboardEntry selectedEntry = new Data_LeaderboardEntry();

        Controller_SQL sqlController = new Controller_SQL();

        public Page_Leaderboards()
        {            
            LeaderboardEntries = new ObservableCollection<Data_LeaderboardEntry>();
            Seasons = new ObservableCollection<int>();


            LoadSeasonList();
            BindingContext = this;

            InitializeComponent();
        }

        private async void LoadSesonLeaderboard(int seasonID)
        {
            activityIndicator.IsRunning = true;
            LeaderboardEntries.Clear();
            int posCount = 1;
            List<Data_LeaderboardEntry> _leaderboardEntries = new List<Data_LeaderboardEntry>();
            _leaderboardEntries = await sqlController.LoadLeaderboardStats(seasonID);
            foreach(Data_LeaderboardEntry entry in _leaderboardEntries)
            {
                entry.Position = posCount;
                posCount++;
                LeaderboardEntries.Add(entry);
            }
            activityIndicator.IsRunning = false;
        }

        private async void LoadSeasonList()
        {
            //activityIndicator.IsRunning = true;
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

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Data_LeaderboardEntry result = (Data_LeaderboardEntry)listView.SelectedItem;
            if(result == selectedEntry)
            {
                LoadPlayerProfile(result.player, result.SeasonID);
                listView.SelectedItem = new Data_LeaderboardEntry();
            }
            else
            {
                selectedEntry = result;
            }

            

        }

        private async void LoadPlayerProfile(object player, int seasonID)
        {
            await Navigation.PushAsync(new Page_PlayerProfile(player, seasonID));
        }
    }
}