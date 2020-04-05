using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/* View Controller for MainPage
 * 
 * 
 * Todo:
 * Lots  
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    28/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 30/03/2020 -- Working Player Search button
 * 29/03/2020 -- Working Recent Result button
 * 28/03/2020 -- File made, Working Upcoming Tournament button
 */

namespace MobileApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.ClassId)
            {
                case "upcomingTournaments":
                    await Navigation.PushAsync(new Page_UpcomingTournaments());
                    break;
                case "venues":
                    break;
                case "results":
                    await Navigation.PushAsync(new Page_Results());
                    break;
                case "playerSearch":
                    await Navigation.PushAsync(new Page_PlayerSearch());
                    break;
            }
        }
    }
}
