using System;
using Restaurant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodsListView : ListView
    {
        public FoodsListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            InitializeComponent();
        }
        private Button _prevActionButton;
        private void ActionButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            if(_prevActionButton != null && _prevActionButton != button)
            {
                var lastFood = _prevActionButton.BindingContext as Order;
                if (lastFood != null) lastFood.IsOrdered = false;
                _prevActionButton.BorderColor = (Color)App.Current.Resources["indigoPinkAccent"];
                //prevActionButton.ButtonIcon = NControl.Controls.FontMaterialDesignLabel.MDPlus;
            }
            _prevActionButton = button;
            var food = button.BindingContext as Order;
            if (food != null && food.IsOrdered)
            {
                button.BorderColor = (Color)App.Current.Resources["indigoPinkAccent"];
                //button.ButtonIcon = NControl.Controls.FontMaterialDesignLabel.MDPlus;
                food.ApplyOrder.Execute(null);
            }
            else
            {
                button.BorderColor = (Color)App.Current.Resources["greenPrimary"];
                //button.ButtonIcon = NControl.Controls.FontMaterialDesignLabel.MDCheck;
                if (food != null) food.IsOrdered = true;
            }
        }
    }
}
