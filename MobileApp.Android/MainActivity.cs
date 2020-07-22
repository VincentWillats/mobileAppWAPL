using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FormsToolkit.Droid;
using Android.Gms.Common;
using Android.Graphics;
using Android.Content;
using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;

namespace MobileApp.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Toolkit.Init();
            LoadApplication(new App());
            Window.SetStatusBarColor(Color.Black);

            if (IsPlayServiceAvailable() == false)
            {
                throw new Exception("This device does not have Google Play Services and cannot receive push notifications.");
            }
            CreateNotificationChannel();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
            AudioController.PlayClick();
        }


        protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            LoadApplication(new App());
            if (Intent.Extras != null)
            {                
                string popupType = Intent.GetStringExtra("popupType");
                string tournyID = Intent.GetStringExtra("tournyID");
                if(!string.IsNullOrWhiteSpace(popupType) || !string.IsNullOrWhiteSpace(tournyID))
                {
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new Popup_UpcomingTournaments(tournyID));
                }               
            }   
        }

        protected override async void OnPostResume()
        {
            base.OnPostResume();
            if (Intent.Extras != null)
            {
                string popupType = Intent.GetStringExtra("popupType");
                string tournyID = Intent.GetStringExtra("tournyID");
                if (!string.IsNullOrWhiteSpace(popupType) || !string.IsNullOrWhiteSpace(tournyID))
                {
                    Analytics.TrackEvent("Notification Clicked!", new Dictionary<string, string>
                                                            {{ "Type of Notification", popupType },
                                                            { "Tournament ID", tournyID }});
                    switch (popupType)
                    {
                        case "upcomingTournament":
                            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new Popup_UpcomingTournaments(tournyID));
                            break;
                        case "result":
                            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new Popup_Result(tournyID));
                            break;
                    }                    
                }
            }
        }

        bool IsPlayServiceAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Console.WriteLine("MainActivity", GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Console.WriteLine("MainActivity", "This device is not supported");
                }
                return false;
            }
            return true;
        }

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }
            var upcomingTournament = new NotificationChannel(FirebaseService.CHANNEL_ID01, FirebaseService.channelName01, NotificationImportance.High)
            {
                Description = "Upcoming Tournaments"
            };
            var results = new NotificationChannel(FirebaseService.CHANNEL_ID02, FirebaseService.channelName02, NotificationImportance.High)
            {
                Description = "Tournament Results"
            };
            var specialOffer = new NotificationChannel(FirebaseService.CHANNEL_ID03, FirebaseService.channelName03, NotificationImportance.High)
            {
                Description = "Special Offers"
            };
            var specialEvent = new NotificationChannel(FirebaseService.CHANNEL_ID04, FirebaseService.channelName04, NotificationImportance.High)
            {
                Description = "Special Events"
                
            };
            upcomingTournament.EnableVibration(true);
            results.EnableVibration(true);
            specialOffer.EnableVibration(true);
            specialEvent.EnableVibration(true);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(upcomingTournament);
            notificationManager.CreateNotificationChannel(results);
            notificationManager.CreateNotificationChannel(specialOffer);
            notificationManager.CreateNotificationChannel(specialEvent);
            
        }

    }
}