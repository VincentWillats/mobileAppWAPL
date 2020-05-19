using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using MobileApp.ViewModels;

namespace MobileApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public  partial class Popup_Result : PopupPage
    {        
        public Popup_Result(object selectedItem)
        {
            BindingContext = new Popup_ResultViewModel(this.Navigation, selectedItem);
            InitializeComponent();     
        }           
    }
}