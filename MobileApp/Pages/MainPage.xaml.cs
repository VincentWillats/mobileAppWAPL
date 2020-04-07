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
 * 07/03/2020 -- Working socials button
 * 06/03/2020 -- Font all same size
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
        List<Label> labelList = new List<Label>();

        public MainPage()
        {
            InitializeComponent();

            labelList.Add(UpcomingTournamentsLbl);
            labelList.Add(LeaderboardsLbl);
            labelList.Add(resultsLbl);
            labelList.Add(PlayerSearchLbl);
        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            Label button = (Label)sender;
            System.Diagnostics.Debug.WriteLine(button.ClassId);
            switch (button.ClassId)
            {
                case "upcomingTournaments":
                    await Navigation.PushAsync(new Page_UpcomingTournaments());
                    break;
                case "leaderboards":
                    await Navigation.PushAsync(new Page_Leaderboards());
                    break;
                case "results":
                    await Navigation.PushAsync(new Page_Results());
                    break;
                case "playerSearch":
                    await Navigation.PushAsync(new Page_PlayerSearch());
                    break;
                case "socials":
                    await Navigation.PushAsync(new Page_Socials());
                    break;
            }
        }
        

        private void SetFont()
        {
            int minFontSize = 10000;
            foreach(Label label in labelList)
            {
                int fontSize = fontSizeController.GetMaxFontSize(label);
                  if (fontSize < minFontSize)
                {
                    minFontSize = fontSize;
                }
            }            
            foreach(Label label in labelList)
            {
                label.FontSize = minFontSize / 2;
            }
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {          
            SetFont();
        }
    }
}
