using MobileApp.Views;
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
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        public Command PageClickCommand { get; }
        public Command SettingsClickCommand { get; }

        bool pageLoading = false;


        public MainPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
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
            string whatPage = whatImage.ClassId;
            if (pageLoading) { return; }
            pageLoading = true;
            switch (whatPage)
            {
                case "settings":
                    await _navigation.PushAsync(new Page_Settings());
                    break;
            }
            pageLoading = false;
        }


        private async Task OpenPage(Label whatButton)
        {
            string whatPage = whatButton.ClassId;
            if (pageLoading) { return; }
            pageLoading = true;
            switch (whatPage)
            {
                case "upcomingTournaments":
                    await _navigation.PushAsync(new Page_UpcomingTournaments());
                    break;
                case "leaderboards":
                    await _navigation.PushAsync(new Page_Leaderboards());
                    break;
                case "results":
                    await _navigation.PushAsync(new Page_Results());
                    break;
                case "playerSearch":
                    await _navigation.PushAsync(new Page_PlayerSearch());
                    break;
                case "socials":
                    await _navigation.PushAsync(new Page_Socials());
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
