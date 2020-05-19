using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_PlayerProfile : ContentPage
    {  


        public Page_PlayerProfile(object playerObj)
        {      
            InitializeComponent();
            BindingContext = new Page_PlayerProfileViewModel(this.Navigation, playerObj);

        }
        public Page_PlayerProfile(object playerObj, int _seasonID)
        {     
            InitializeComponent();
            BindingContext = new Page_PlayerProfileViewModel(this.Navigation, playerObj, _seasonID);
        }
    }
}