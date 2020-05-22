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
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Command SwipedBackCommand { get; }
        public Command SocialClickedCommand { get; }
        
        
        public Page_SocialsViewModel(INavigation navigation)
        {            
            _navigation = navigation;
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
                    break;
                case "youtube":
                    await Launcher.TryOpenAsync(new Uri("https://www.youtube.com/user/WAPokerLeague"));
                    break;
                case "twitch":
                    await Launcher.TryOpenAsync(new Uri("https://www.twitch.tv/wapokerleague"));
                    break;
                case "website":
                    await Launcher.TryOpenAsync(new Uri("http://www.wapokerleague.com.au/"));
                    break;
            }
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
