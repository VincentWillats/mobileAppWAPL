using MobileApp.Pages.Popups;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]    
    public partial class Page_Results : ContentPage
    {
        Controller_SQL sqlController = new Controller_SQL();
        public IList<Data_Result> Results { get; private set; }
        public Page_Results()
        {
            InitializeComponent();
            Results = new List<Data_Result>();
            LoadResults();            
        }
        private async void LoadResults()
        {
            List<Data_Result> _results = await Controller_SQL.LoadResultsAsync();
            foreach(Data_Result result in _results)
            {
                Results.Add(result);
            }
            activityIndicator.IsRunning = false;
            BindingContext = this;
        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedItem != null)
            {
                await DisplayResultDetails(new Popup_Result(listView.SelectedItem));
                listView.SelectedItem = null;
            }            
        }     
        private async Task DisplayResultDetails(PopupPage page)
        {
            await Navigation.PushPopupAsync(page);
        }
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}