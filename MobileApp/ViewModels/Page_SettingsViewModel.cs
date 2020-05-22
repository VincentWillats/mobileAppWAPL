using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


/* ViewModel for Page_Settings
 * 
 * Creator:         Vincent Willats
 * Date Created:    22/05/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 22/05/2020 -- Created
 */

namespace MobileApp.ViewModels
{
    public class Page_SettingsViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _notifcationsForUpcomingTournaments;
        private bool _notifcationsForResults;
        private bool _notifcationsForSpecialEvents;
        private bool _notifcationsForSpecialOffers;
        private bool _applicationSounds;

        public bool NotifcationsForUpcomingTournaments
        {
            get { return _notifcationsForUpcomingTournaments; }
            set
            {
                _notifcationsForUpcomingTournaments = value;
                OnPropertyChanged();
                Preferences.Set("UpcomingTournaments", _notifcationsForUpcomingTournaments);
                MessagingCenter.Send<Page_SettingsViewModel, bool>(this, "UpdateNotifcationsForUpcomingTournaments", _notifcationsForUpcomingTournaments);
                AudioController.PlayClick();
            }
        }
        public bool NotifcationsForResults
        {
            get { return _notifcationsForResults; }
            set
            {
                _notifcationsForResults = value;
                OnPropertyChanged();
                Preferences.Set("Result", _notifcationsForResults);
                MessagingCenter.Send<Page_SettingsViewModel, bool>(this, "UpdateNotifcationsForResults", _notifcationsForResults);
                AudioController.PlayClick();
            }
        }
        public bool NotifcationsForSpecialEvents
        {
            get { return _notifcationsForSpecialEvents; }
            set
            {
                _notifcationsForSpecialEvents = value;
                OnPropertyChanged();
                Preferences.Set("SpecialEvents", _notifcationsForSpecialEvents);
                MessagingCenter.Send<Page_SettingsViewModel, bool>(this, "UpdateNotifcationsForSpecialEvents", _notifcationsForSpecialEvents);
                AudioController.PlayClick();
            }
        }
        public bool NotifcationsForSpecialOffers
        {
            get { return _notifcationsForSpecialOffers; }
            set
            {
                _notifcationsForSpecialOffers = value;
                OnPropertyChanged();
                Preferences.Set("SpecialOffers", _notifcationsForSpecialOffers);
                MessagingCenter.Send<Page_SettingsViewModel, bool>(this, "UpdateNotifcationsForSpecialOffers", _notifcationsForSpecialOffers);
                AudioController.PlayClick();
            }
        }
        public bool ApplicationSounds
        {
            get { return _applicationSounds; }
            set
            {
                _applicationSounds = value;
                Preferences.Set("AppSounds", _applicationSounds);
                OnPropertyChanged();
                AudioController.PlayClick();
            }
        }

        public Command SwipedBackCommand { get; }
        public Command EmailCommand { get; }
        public Command CallCommand { get; }
        
        
        public Page_SettingsViewModel(INavigation navigation)
        {            
            _navigation = navigation;
            SwipedBackCommand = new Command(SwipedBack);
            EmailCommand = new Command(OpenEmail);
            CallCommand = new Command(OpenCall);

            LoadSettings();
        }    

        private async void OpenEmail()
        {
            await SendEmail();
        }

        public async Task SendEmail()
        {
            try
            {
                var message = new EmailMessage();
                List<string> emailTo = new List<string>();
                emailTo.Add("info@wapokerleague.com.au");
                message.To = emailTo;
                await Email.ComposeAsync(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error opening email: " + ex.Message);
            }
        }

        private void OpenCall()
        {
            try
            {
                PhoneDialer.Open("+61413155193");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error calling: " + ex.Message);
            }
        }

        private void LoadSettings()
        {
            NotifcationsForUpcomingTournaments = Preferences.Get("UpcomingTournaments", true);
            NotifcationsForResults = Preferences.Get("Result", true);
            NotifcationsForSpecialEvents = Preferences.Get("SpecialEvents", true);
            NotifcationsForSpecialOffers = Preferences.Get("SpecialOffers", true);
            ApplicationSounds = Preferences.Get("AppSounds", true);
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
