using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MobileApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public  partial class Popup_Result : PopupPage
    {
        Controller_SQL sqlController = new Controller_SQL();
        public Data_Result Result { get; private set; }
        public ObservableCollection<Data_ResultDetailPlayer> ResultPlayerList { get; private set; }
        public ObservableCollection<Data_Image> ImageList { get; private set; }
        
        public Popup_Result(object selectedItem)
        {
            Result = (Data_Result)selectedItem;
            ImageList = new ObservableCollection<Data_Image>();
            ResultPlayerList = new ObservableCollection<Data_ResultDetailPlayer>();
            BindingContext = this;
            InitializeComponent();     
        }

        private async void LoadPage()
        {
            await LoadResultDetailsAsyn();            
            await LoadImagesAsync();
            activityIndicator.IsRunning = false;
        }

        private async Task LoadResultDetailsAsyn()
        {
            List<Data_ResultDetailPlayer> _results = await sqlController.LoadResultDetailsAsyn(Result.tourny.TournamentID);
            foreach(Data_ResultDetailPlayer player in _results)
            {
                ResultPlayerList.Add(player);
            }
        }

        private async Task LoadImagesAsync()
        {
            if (!IsWebsiteUp())
            {
                System.Diagnostics.Debug.WriteLine("WebsiteDown");
                return;
            }
            List<Data_Image> _images = await sqlController.LoadResultImages(Result.tourny.TournamentID);
            foreach (Data_Image image in _images)
            {
                ImageList.Add(image);
            }            
        }

        private bool IsWebsiteUp()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.wapokerleague.com.au/");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        protected override void OnAppearing()
        {
                   
        }        
        

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            LoadPage();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
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
    }
}