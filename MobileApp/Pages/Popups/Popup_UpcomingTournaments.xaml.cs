using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MobileApp.Pages.Popups
{
    public partial class Popup_UpcomingTournaments : PopupPage
    {
        public Data_Tournament upcomingTournament { get; private set; }
        public Popup_UpcomingTournaments(object selectedItem)
        {
            upcomingTournament = (Data_Tournament)selectedItem;

            SetMap();
            InitializeComponent();
            BindingContext = this;     
        }

        private async void SetMap()
        {
            var address = upcomingTournament.Address01 + " " + upcomingTournament.Address02 + " " +
                            upcomingTournament.Address03 + " " + upcomingTournament.Address04;

            var location = await Geocoding.GetLocationsAsync(address);

            var pos = new Position(location.FirstOrDefault().Latitude, location.FirstOrDefault().Longitude);

            var pin = new Pin
            {
                Type = PinType.Place,
                Label = upcomingTournament.VenueName,
                Position = pos
            };

            MapSpan mapSpan = MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(2));
            map01.MoveToRegion(mapSpan);
            map01.Pins.Add(pin);
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            //PopupNavigation.Instance.PopAsync();
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            System.Diagnostics.Debug.WriteLine("BACK GROUND CLICKED");
            PopupNavigation.Instance.PopAsync();
            return true;
        }
               
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}