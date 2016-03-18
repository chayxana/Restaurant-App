using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages.MainPages
{
    public partial class MasterPage : ContentPage
    {
        public MainViewModel ViewModel { get; set; }

        public MasterPage(MainViewModel viewModel)
        {
            InitializeComponent();

            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Foods",
                IconSource = "ic_food.png",
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
    }

    public class MasterPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
