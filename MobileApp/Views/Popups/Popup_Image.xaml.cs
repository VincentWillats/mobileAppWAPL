using MobileApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace MobileApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup_Image : PopupPage
    {
        public Popup_Image(Data_Image _image)
        {           
            InitializeComponent();
            BindingContext = new Popup_ImageViewModel(_image);
        }
    }
}