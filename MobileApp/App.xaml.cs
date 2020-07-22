using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MobileApp
{
    public partial class App : Application
    {
        public static string tournyID;
        public bool navigating;
        public App(bool shallNavigate)
        {
            navigating = shallNavigate;

            InitializeComponent();

            if (navigating == false) // This condition for normar process and in else part is pushnotification process**
            {
                MainPage = new NavigationPage(new MainPage());// HomePage
            }
            else
            {         
                MainPage = new NavigationPage(new MainPage(tournyID));// HomePage

            }
            //MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start(Keys.Keys._AndroidSecret +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
