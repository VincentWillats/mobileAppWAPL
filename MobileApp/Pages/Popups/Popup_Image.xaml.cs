using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup_Image : PopupPage
    {

        public Data_Image Image { get; private set; }
        public Popup_Image(object _image)
        {
            Image = (Data_Image)_image;
            BindingContext = this;

            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}