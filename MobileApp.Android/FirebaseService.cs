﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Firebase.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WindowsAzure.Messaging;
using Xamarin.Forms;

namespace MobileApp.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT"})]
    class FirebaseService : FirebaseMessagingService
    {
        public static string FCMTemplateBody { get; set; } =    "{" +
                                                                    "\"data\":" +
                                                                        "{" +
                                                                            "\"message\":\"$(messagePM)\"," +
                                                                            "\"android_channel_id\":\"$(android_channel_idPM)\"," +
                                                                            "\"image\":\"$(imagePM)\"," +
                                                                            "\"title\":\"$(titlePM)\"" +
                                                                        "}" +                                                                  
                                                                "}";
        public static string NotificationHubName { get; set; } = Keys.Keys._NotificationHubName;
        public static string ListenConnectionString { get; set; } = Keys.Keys._ListenConnectionString;
        public static string[] SubscriptionTags { get; set; } = { "UpcomingTournament" , "SpecialOffer", "SpecialEvent", "Result" };

        public static string DebugTag { get; set; } = "XamarinNotify";


        public static string CHANNEL_ID01 = "UpcomingTournament";
        public static string CHANNEL_ID02 = "Result";
        public static string CHANNEL_ID03 = "SpecialOffer";
        public static string CHANNEL_ID04 = "SpecialEvent";


        public static string channelName01 = "Upcoming Tournaments";
        public static string channelName02 = "Tournament Results";
        public static string channelName03 = "Special Offers";
        public static string channelName04 = "Special Events";


        public static string NotificationName01 = "Upcoming Tournament";
        public static string NotificationName02 = "Tournament Result";
        public static string NotificationName03 = "Special Offer";
        public static string NotificationName04 = "Special Event";


        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            SendRegistrationToServer(token);
        }

        void SendRegistrationToServer(string token)
        {
            try
            {
                NotificationHub hub = new NotificationHub(NotificationHubName, ListenConnectionString, this);
                // register device with Azure Notification Hub using the token from FCM
                Registration reg = hub.Register(token, SubscriptionTags);
                // subscribe to the SubscriptionTags list with a simple template.
                string pnsHandle = reg.PNSHandle;
                //hub.RegisterTemplate(pnsHandle, "defaultTemplate", FCMTemplateBody, SubscriptionTags);
                TemplateRegistration templateReg = hub.RegisterTemplate(pnsHandle, "defaultTemplate", FCMTemplateBody, SubscriptionTags);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(DebugTag, $"Error registering device: {e.Message}");
            }
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            try
            {
                MessagingCenter.Send(message.Data.Values.First(), "Notification Sent");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error sending message via MessagingCenter: " + e.Message);
            }

            SendLocalNotification(message.Data);
        }

        void SendLocalNotification(IDictionary<string, string> data)
        {

            var intent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
            //var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }
            BuildNotifications(data, intent, pendingIntent);
        }

        private void BuildNotifications(IDictionary<string, string> data, Intent intent, PendingIntent pendingIntent)
        {
            string message = "";
            string channel = "";
            string title = "";
            string largeImg = "";
            Bitmap img = null;

            data.TryGetValue("message", out message);
            data.TryGetValue("android_channel_id", out channel);
            data.TryGetValue("title", out title);
            data.TryGetValue("image", out largeImg);

            if (!string.IsNullOrEmpty(largeImg))
            {
                img = GetImageBitmapFromUrl(largeImg);
            }
            else
            {
                img = BitmapFactory.DecodeResource(Resources, Resource.Drawable.roundlogo);
            }


            var notificationManager = NotificationManager.FromContext(this);

            var notificationBuilder = new NotificationCompat.Builder(this, channel)
                .SetContentTitle(title)
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetShowWhen(false)
                .SetLargeIcon(img)
                .SetContentIntent(pendingIntent);              
            notificationManager.Notify(0, notificationBuilder.Build());
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}