using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

/* View for MainPage
 * 
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    28/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 17/05/2020 -- Moved to MVVM
 * 15/04/2020 -- Fixed double loading
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
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        public MainPage(string tournyID)
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(tournyID);           
        }
    }
}
