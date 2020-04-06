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
        

        private void SetFont()
        {
            int minFontSize = 1000;
            foreach(Label label in labelList)
            {
                int fontSize = fontSizeController.GetFontSize(label);
                if(fontSize < minFontSize)
                {
                    minFontSize = fontSize;
                }
                System.Diagnostics.Debug.WriteLine(minFontSize.ToString());
            }
            
            foreach(Label label in labelList)
            {
                label.FontSize = minFontSize;
            }
        }

        private void Lbl_SizeChanged(object sender, EventArgs e)
        {          
            Label lbl = (Label)sender;
            SetFont();
            lbl.SizeChanged -= Lbl_SizeChanged;
            //fontSizeController.SetFontSize(button);
            ////button.SizeChanged += Btn_SizeChanged;

        }
    }
}
