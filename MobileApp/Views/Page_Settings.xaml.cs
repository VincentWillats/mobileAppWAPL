using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Settings : ContentPage
    {
        public Page_Settings()
        {
            InitializeComponent();
            BindingContext = new Page_SettingsViewModel();
        }
    }
}