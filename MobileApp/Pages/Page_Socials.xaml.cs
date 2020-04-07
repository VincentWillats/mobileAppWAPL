using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Socials : ContentPage
    {
        public Page_Socials()
        {
            InitializeComponent();
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            string name = img.ClassId;

            switch (name)
            {
                case "facebook":
                    Device.OpenUri(new Uri("https://www.facebook.com/WAPokerLeague/"));
                    break;
                case "youtube":
                    Device.OpenUri(new Uri("https://www.youtube.com/user/WAPokerLeague"));
                    break;
                case "twitch":
                    Device.OpenUri(new Uri("https://www.twitch.tv/wapokerleague"));
                    break;
                case "website":
                    Device.OpenUri(new Uri("http://www.wapokerleague.com.au/"));
                    break;
            }
        }
    }
}