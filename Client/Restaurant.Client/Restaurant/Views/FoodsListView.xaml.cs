using Restaurant.Controls;
using Restaurant.Models;
using System;
using Xamarin.Forms;

namespace Restaurant.Views
{
    public partial class FoodsListView : ListView
    {
        public FoodsListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            InitializeComponent();
        }
        private FloatActionButton prevActionButton;
        private void ActionButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as FloatActionButton;
            if (button == null) return;
            if(prevActionButton != null && prevActionButton != button)
            {
                var lastFood = prevActionButton.BindingContext as Order;
                if (lastFood != null) lastFood.IsOrdered = false;
                prevActionButton.ButtonColor = (Color)App.Current.Resources["indigoPinkAccent"];
                prevActionButton.ButtonIcon = NControl.Controls.FontMaterialDesignLabel.MDPlus;
            }
            prevActionButton = button;
            var food = button.BindingContext as Order;
            if (food != null && food.IsOrdered)
            {
                button.ButtonColor = (Color)App.Current.Resources["indigoPinkAccent"];
                button.ButtonIcon = NControl.Controls.FontMaterialDesignLabel.MDPlus;
                food.ApplyOrder.Execute(null);
            }
            else
            {
                button.ButtonColor = (Color)App.Current.Resources["greenPrimary"];
                button.ButtonIcon = NControl.Controls.FontMaterialDesignLabel.MDCheck;
                if (food != null) food.IsOrdered = true;
            }
        }
    }
}
