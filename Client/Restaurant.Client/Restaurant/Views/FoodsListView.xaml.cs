using System;
using Restaurant.Models;
using Xamarin.Forms;

namespace Restaurant.Views
{
    public partial class FoodsListView : ListView
    {
        public FoodsListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            InitializeComponent();
        }
    }
}
