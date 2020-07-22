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
    class Page_PlayerSearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _pageLoading = false;
        private bool _searching;
        private Data_Player _objItemSelected;

        private string _searchString;

        public string SearchString
        {
            get { return _searchString;  }
            set
            {                
                
                _searchString = value;
                OnPropertyChanged();
                FillPlayerList(_searchString);
            }
        }

        public bool Searching
        {
            get { return _searching; }
            set
            {
                _searching = value;
                OnPropertyChanged();
            }
        }

        public Data_Player ObjItemSelected
        {
            get { return _objItemSelected; }
            set
            {
                if (_objItemSelected != value)
                {
                    _objItemSelected = value;
                    if (_objItemSelected != null)
                    {
                        AudioController.PlayClick();
                        DisplayPlayerProfile(_objItemSelected);
                        _objItemSelected = null;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public Command SwipedBackCommand { get; }

        public ObservableCollection<Data_Player> Players { get; private set; }

        public Page_PlayerSearchViewModel()
        {    
            SwipedBackCommand = new Command(SwipedBack);
            Players = new ObservableCollection<Data_Player>();
        } 

        private async void DisplayPlayerProfile(Data_Player playerObj)
        {
            if (_pageLoading) { return; }
            _pageLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new Page_PlayerProfile(playerObj));
            Analytics.TrackEvent("Viewed Player Profile", new Dictionary<string, string>
            {
                {"Player Name", playerObj.FullName },
                {"Player ID", playerObj.PlayerID.ToString() },
                {"Season", "N/A" }
            });
            _pageLoading = false;
        }

        private async void FillPlayerList(string searchStr)
        {
            Players.Clear();
            if(searchStr.Length <= 2) { return; }
            if (_searching) { return; }
            Searching = true;

            List<Data_Player> _players = await Controller_SQL.LoadPlayerSearchedListAsync(searchStr);
            foreach (Data_Player player in _players)
            {
                Players.Add(player);
            }
            Searching = false;
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
