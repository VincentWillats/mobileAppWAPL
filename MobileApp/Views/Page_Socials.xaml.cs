using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.ViewModels;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Socials : ContentPage
    {
        public Page_Socials()
        {
            InitializeComponent();
            BindingContext = new Page_SocialsViewModel();
        }
    }
}