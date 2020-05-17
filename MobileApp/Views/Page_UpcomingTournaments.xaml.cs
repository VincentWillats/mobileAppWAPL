using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.ViewModels;


namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_UpcomingTournaments : ContentPage
    {
        public Page_UpcomingTournaments()
        {
            InitializeComponent();
            BindingContext = new Page_UpcomingTournamentsViewModel(this.Navigation);
        }           
    }
}