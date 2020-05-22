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
    class Page_SettingsViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public Command SwipedBackCommand { get; }
        
        
        public Page_SettingsViewModel(INavigation navigation)
        {            
            _navigation = navigation;
            SwipedBackCommand = new Command(SwipedBack);
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
