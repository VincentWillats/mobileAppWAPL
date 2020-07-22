using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MobileApp.Pages.Popups
{
    public partial class Popup_UpcomingTournaments : PopupPage
    {
        public Popup_UpcomingTournaments(object selectedItem)
        {           
            InitializeComponent();
            BindingContext = new Popup_UpcomingTournamentsViewModel(selectedItem);     
        }

        public Popup_UpcomingTournaments(string tournyID)
        {
            InitializeComponent();
            int tournyInt = int.Parse(tournyID);
            BindingContext = new Popup_UpcomingTournamentsViewModel(tournyInt);
        }
    }
}