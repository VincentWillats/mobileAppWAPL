using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            string name = img.ClassId;
            await OpenSocial(name);        
        }
        private async Task OpenSocial(string name)
        {
            switch (name)
            {
                case "facebook":
                    await Launcher.TryOpenAsync(new Uri("https://www.facebook.com/WAPokerLeague/"));
                    break;
                case "youtube":
                    await Launcher.TryOpenAsync(new Uri("https://www.youtube.com/user/WAPokerLeague"));
                    break;
                case "twitch":
                    await Launcher.TryOpenAsync(new Uri("https://www.twitch.tv/wapokerleague"));
                    break;
                case "website":
                    await Launcher.TryOpenAsync(new Uri("http://www.wapokerleague.com.au/"));
                    break;
            }
        }
    }
}