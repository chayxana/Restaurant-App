using System;
using System.Collections.Generic;
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

            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Foods",
                IconSource = "ic_restaurant_menu_black.png",
                TargetType = typeof(FoodsPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Orders",
                IconSource = "ic_basket.png",
                TargetType = typeof(FoodsPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Chat",
                IconSource = "ic_wechat.png",
                TargetType = typeof(FoodsPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Settings",
                IconSource = "ic_settings.png",
                TargetType = typeof(FoodsPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "About",
                IconSource = "ic_alert_circle_outline.png",
                TargetType = typeof(FoodsPage)
            });

            listView.ItemsSource = masterPageItems;
        }

        public void Cell_Tapped(object sender, EventArgs e)
        {
           
        }
    }



    public class MasterPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
