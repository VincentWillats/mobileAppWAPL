using MobileApp.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/* View Controller for PlayerSearch 
 * 
 * Todo:
 * Lots  
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    30/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 30/03/2020 -- File Made, Search working
 */

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_PlayerSearch : ContentPage
    {
        public Page_PlayerSearch()
        {
            InitializeComponent();
            BindingContext = new Page_PlayerSearchViewModel(this.Navigation);
        }
    }
}