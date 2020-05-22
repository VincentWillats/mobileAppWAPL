using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MobileApp
{
    class AudioController
    {
        private static bool isLoaded = false;
        public static void PlayClick()
        {
            if (!Preferences.Get("AppSounds", true))
            {
                System.Diagnostics.Debug.WriteLine("click off");
                return;
            }
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

            if (!isLoaded)
            {
                player.Load("click.wav");
            }
            

            player.Play();
        }
    }
}
