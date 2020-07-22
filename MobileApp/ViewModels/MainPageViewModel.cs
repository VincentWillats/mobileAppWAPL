using Microsoft.AppCenter.Analytics;
using MobileApp.Pages.Popups;
using MobileApp.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

/* ViewModel for MainPage
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
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command PageClickCommand { get; }
        public Command SettingsClickCommand { get; }

        bool pageLoading = false;
        public MainPageViewModel()
        {
            PageClickCommand = new Command<Label>(async (x) => await OpenPage(x));
            SettingsClickCommand = new Command<Image>(async (x) => await OpenSettings(x));

            //MessagingCenter.Subscribe<string>(this, "Update", (sender) =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        System.Diagnostics.Debug.WriteLine(sender.ToString());
            //    });
            //});
        }
       

        private async Task OpenSettings(Image whatImage)
        {
            AudioController.PlayClick();
            string whatPage = whatImage.ClassId;
            if (pageLoading) { return; }
            pageLoading = true;
            switch (whatPage)
            {
                case "settings":
                    await Application.Current.MainPage.Navigation.PushAsync(new Page_Settings());
                    Analytics.TrackEvent("Viewed Page", new Dictionary<string, string>
                                         {{ "What Page", "Settings" }});
                    break;
            }
            pageLoading = false;
        }


        private async Task OpenPage(Label whatButton)
        {
            AudioController.PlayClick();
            string whatPage = whatButton.ClassId;
            if (pageLoading) { return; }
            pageLoading = true;
            switch (whatPage)
            {
                case "upcomingTournaments":
                    Analytics.TrackEvent("Viewed Page", new Dictionary<string, string>
                    {{ "What Page", "UpcomingTournaments" }});
                    await Application.Current.MainPage.Navigation.PushAsync(new Page_UpcomingTournaments());
                    
                    break;
                case "leaderboards":
                    Analytics.TrackEvent("Viewed Page", new Dictionary<string, string>
                    {{ "What Page", "Leaderboards" }});
                    await Application.Current.MainPage.Navigation.PushAsync(new Page_Leaderboards());     
                    
                    break;
                case "results":
                    Analytics.TrackEvent("Viewed Page", new Dictionary<string, string>
                    {{ "What Page", "Results" }});
                    await Application.Current.MainPage.Navigation.PushAsync(new Page_Results());
                    
                    break;
                case "playerSearch":
                    Analytics.TrackEvent("Viewed Page", new Dictionary<string, string>
                    {{ "What Page", "PlayerSearch" }});
                    await Application.Current.MainPage.Navigation.PushAsync(new Page_PlayerSearch());
                   
                    break;
                case "socials":
                    Analytics.TrackEvent("Viewed Page", new Dictionary<string, string>
                    {{ "What Page", "Socials" }});
                    await Application.Current.MainPage.Navigation.PushAsync(new Page_Socials());
                    
                    break;
            }
            pageLoading = false;
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
