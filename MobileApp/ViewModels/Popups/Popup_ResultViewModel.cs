using Microsoft.AppCenter.Analytics;
using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

/* ViewModel for Popup_Result
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
    class Popup_ResultViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        private Data_Result _currentTournament;
        private bool _imageLoading;
        private bool _resultsLoading;
        private Data_Image _imgItemSelected;
        private Data_ResultDetailPlayer _resultSelected;

        public bool ResultsLoading
        {
            get { return _resultsLoading; }
            set
            {
                _resultsLoading = value;
                OnPropertyChanged();
            }
        }

        public bool ImageLoading
        {
            get { return _imageLoading; }
            set
            {
                _imageLoading = value;
                NoImageText = !_imageLoading;
                OnPropertyChanged();
            }
        }

        private bool _NoImageText;
        public bool NoImageText
        {
            get { return _NoImageText; }
            set
            {
                _NoImageText = value;
                OnPropertyChanged();
            }
        }

        public Data_Result CurrentTournament
        {
            get { return _currentTournament; }
            set
            {
                _currentTournament = value;
                OnPropertyChanged();                
            }
        }

        public Data_Image ImgItemSelected
        {
            get { return _imgItemSelected; }
            set
            {
                if (_imgItemSelected != value)
                {
                    _imgItemSelected = value;
                    if(_imgItemSelected != null)
                    {
                        AudioController.PlayClick();
                        OpenImage(_imgItemSelected);
                        _imgItemSelected = null;
                        OnPropertyChanged();
                    }                    
                }                
            }
        }

        public Data_ResultDetailPlayer ResultSelected
        {
            get { return _resultSelected; }
            set
            {
                if (_resultSelected != value)
                {
                    _resultSelected = value;
                    if (_resultSelected != null)
                    {
                        AudioController.PlayClick();
                        ResultClicked(_resultSelected.player);
                        _resultSelected = null;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public Command SwipedBackCommand { get; }
        
        public ObservableCollection<Data_ResultDetailPlayer> ResultPlayerList { get; private set; }

        public ObservableCollection<Data_Image> ImageList { get; private set; }

        public Popup_ResultViewModel(INavigation navigation, object selectedItem)
        {
            _navigation = navigation;
            CurrentTournament = (Data_Result)selectedItem;
            ImageList = new ObservableCollection<Data_Image>();
            ResultPlayerList = new ObservableCollection<Data_ResultDetailPlayer>();

            SwipedBackCommand = new Command(SwipedBack);
            LoadPage();
        }

        private async void LoadPage()
        {            
            await LoadResultDetailsAsyn();
            await LoadImagesAsync();           
        }

        private async Task LoadResultDetailsAsyn()
        {
            ResultsLoading = true;
            List<Data_ResultDetailPlayer> _results = await Controller_SQL.LoadResultDetailsAsyn(CurrentTournament.tourny.TournamentID);
            foreach (Data_ResultDetailPlayer player in _results)
            {
                ResultPlayerList.Add(player);
            }
            ResultsLoading = false;
        }

        private async Task LoadImagesAsync()
        {
            ImageLoading = true;
            if (!IsWebsiteUp())
            {
                System.Diagnostics.Debug.WriteLine("WebsiteDown");
                return;
            }
            List<Data_Image> _images = await Controller_SQL.LoadResultImages(CurrentTournament.tourny.TournamentID);
            foreach (Data_Image image in _images)
            {
                ImageList.Add(image);
            }
            ImageLoading = false;
        }

        private async void OpenImage(Data_Image img)
        {    
            //await Navigation.PushPopupAsync(img);    
            await PopupNavigation.Instance.PushAsync(new Popup_Image(img));
            Analytics.TrackEvent("Image Opened", new Dictionary<string, string>
            {
                {"Image Path", img.ImagePath},
                {"Tournament ID", CurrentTournament.tourny.TournamentID.ToString() },
                {"Tournament Venue", CurrentTournament.tourny.VenueName }
            });
        }

        private async void ResultClicked(Data_Player result)
        {
            await _navigation.PushAsync(new Page_PlayerProfile(result));
            Analytics.TrackEvent("Viewed Player Profile", new Dictionary<string, string>
            {
                {"Player Name", result.FullName },
                {"Player ID", result.PlayerID.ToString() },
                {"Season", "N/A" }
            });
            await PopupNavigation.Instance.PopAsync();
        }

        private bool IsWebsiteUp()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.wapokerleague.com.au/");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        private void SwipedBack()
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
