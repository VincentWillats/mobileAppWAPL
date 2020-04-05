using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/* View Controller for PlayerSearch 
 * 
 * Todo:
 * Lots  
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    30/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 30/03/2020 -- File Made, Search working
 */

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_PlayerSearch : ContentPage
    {
        Controller_SQL sqlController = new Controller_SQL();
        public ObservableCollection<Data_Player> Players { get; private set; }
        bool searching = false;              

        public Page_PlayerSearch()
        {
            InitializeComponent();            
            Players = new ObservableCollection<Data_Player>();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Players.Clear();
            activityIndicator.IsRunning = true;
            if (searching) { return; }
            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text;
            searchText = searchText.Replace("%", "");
            if (searchText.Length >= 2)
            {                
                FillPlayerList(searchText);
            }
            else
            {
                activityIndicator.IsRunning = false;
            }
        }

        private async void FillPlayerList(string searchStr)
        {
            searching = true;            
            List<Data_Player> _players = await sqlController.LoadPlayerSearchedListAsync(searchStr);
            foreach (Data_Player player in _players)
            {                
                Players.Add(player);
            }
            System.Diagnostics.Debug.WriteLine(Players.Count);
            BindingContext = this;
            activityIndicator.IsRunning = false;
            searching = false;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;

            DisplayPlayerProfile(listView.SelectedItem);
            listView.SelectedItem = null;
        }

        private async void DisplayPlayerProfile(object playerObj)
        {
            await Navigation.PushAsync(new Page_PlayerProfile(playerObj));
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}