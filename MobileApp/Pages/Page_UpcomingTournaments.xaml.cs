using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_UpcomingTournaments : ContentPage
    {
        Controller_SQL sqlControl = new Controller_SQL();
        FontSizeController fontController = new FontSizeController();
        public IList<Data_Tournament> UpcomingTournaments { get; private set; }

        

        public Page_UpcomingTournaments()
        {
            InitializeComponent();
            activityIndicator.IsRunning = true;
            UpcomingTournaments = new List<Data_Tournament>();
            LoadUpcomingTournaments();            
        }

        private async void LoadUpcomingTournaments()
        {
            List<Data_Tournament> _tournies = await sqlControl.LoadUpcomingTournamentsAsync();
            foreach (Data_Tournament tourny in _tournies)
            {
                UpcomingTournaments.Add(tourny);
            }
            activityIndicator.IsRunning = false;
            BindingContext = this;
       
        }


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedItem != null)
            {               
                DisplayEventDetails(new Popup_UpcomingTournaments(listView.SelectedItem));
                listView.SelectedItem = null;

            }
        }

        private async void DisplayEventDetails(PopupPage page)
        {
            await Navigation.PushPopupAsync(page);
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}