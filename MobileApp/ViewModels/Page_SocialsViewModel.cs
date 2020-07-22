using Microsoft.AppCenter.Analytics;
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

/* ViewModel for Page_Socials
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
    class Page_SocialsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Command SwipedBackCommand { get; }
        public Command SocialClickedCommand { get; }
        
        
        public Page_SocialsViewModel()
        {            
            SwipedBackCommand = new Command(SwipedBack);
            SocialClickedCommand = new Command<Image>(async (x) => await SocialClicked(x));
        }


        private async Task SocialClicked(Image img)
        {
            AudioController.PlayClick();
            string name = img.ClassId;
            switch (name)
            {
                case "facebook":
                    await Launcher.TryOpenAsync(new Uri("https://www.facebook.com/WAPokerLeague/"));                    
                    Analytics.TrackEvent("Social Clicked", new Dictionary<string, string>
                    {{ "What Social", "Facebook" }});

                    break;
                case "youtube":
                    await Launcher.TryOpenAsync(new Uri("https://www.youtube.com/user/WAPokerLeague"));
                    Analytics.TrackEvent("Social Clicked", new Dictionary<string, string>
                    {{ "What Social", "Youtube" }});
                    break;
                case "twitch":
                    await Launcher.TryOpenAsync(new Uri("https://www.twitch.tv/wapokerleague"));
                    Analytics.TrackEvent("Social Clicked", new Dictionary<string, string>
                    {{ "What Social", "Twitch" }});
                    break;
                case "website":
                    await Launcher.TryOpenAsync(new Uri("http://www.wapokerleague.com.au/"));
                    Analytics.TrackEvent("Social Clicked", new Dictionary<string, string>
                    {{ "What Social", "WAPL Website" }});
                    break;
            }
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
