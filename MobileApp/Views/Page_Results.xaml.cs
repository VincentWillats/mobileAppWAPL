using MobileApp.Pages.Popups;
using MobileApp.ViewModels;
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
        public Page_Results()
        {
            InitializeComponent();
            BindingContext = new Page_ResultsViewModel();
        }

    }
}