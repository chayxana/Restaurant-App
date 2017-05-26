using System;
using System.Collections.Generic;
using ReactiveUI;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public partial class MasterPage : ContentPage
    {
        public MainViewModel ViewModel { get; set; }

        public MasterPage()
        {
            InitializeComponent();

            
           
        }

        public void Cell_Tapped(object sender, EventArgs e)
        {
           
        }
    }



    public class MasterItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type NavigationType { get; set; }
    }
}
