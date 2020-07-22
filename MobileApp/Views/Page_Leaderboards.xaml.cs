using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Leaderboards : ContentPage
    {

        public Page_Leaderboards()
        {                        
            InitializeComponent();
            BindingContext = new Page_LeaderboardsViewModel();
        }    
    }
}