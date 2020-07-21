using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

/* ViewModel for Popup_Image
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
    class Popup_ImageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command ClosePopupCommand { get; }

        private Data_Image _image { get; set; }

        public Data_Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }


        public Popup_ImageViewModel(Data_Image imageObj)
        {
            Image = (Data_Image)imageObj;
            ClosePopupCommand = new Command(ClosePopup); 
        }

        private void ClosePopup()
        {
            AudioController.PlayClick();
            PopupNavigation.Instance.PopAsync();
        }


        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
