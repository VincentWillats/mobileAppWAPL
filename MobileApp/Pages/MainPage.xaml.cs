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
 * 05/03/2020 -- Working Leaderboard button
 * 30/03/2020 -- Working Player Search button
 * 29/03/2020 -- Working Recent Result button
 * 28/03/2020 -- File made, Working Upcoming Tournament button
 */

namespace MobileApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        FontSizeController fontSizeController = new FontSizeController();
        public MainPage()
        {
            //SetFontSize();
            InitializeComponent();
            SetFontSizes();
        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.ClassId)
            {
                case "upcomingTournaments":
                    await Navigation.PushAsync(new Page_UpcomingTournaments());
                    break;
                case "Leaderboards":
                    await Navigation.PushAsync(new Page_Leaderboards());
                    break;
                case "results":
                    await Navigation.PushAsync(new Page_Results());
                    break;
                case "playerSearch":
                    await Navigation.PushAsync(new Page_PlayerSearch());
                    break;
            }
        }

        private void SetFontSizes()
        {
            fontSizeController.SetFontSize(ref upComingTournamentsBtn);
            fontSizeController.SetFontSize(ref leaderboardsBtn);
            fontSizeController.SetFontSize(ref resultsBtn);
            fontSizeController.SetFontSize(ref playerSearchBtn);
        }
    }
}
