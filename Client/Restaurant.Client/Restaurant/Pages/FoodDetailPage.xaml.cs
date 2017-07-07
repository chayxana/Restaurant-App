using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetailPage : FoodDetailPageXaml
    {
        public FoodDetailPage()
        {
            InitializeComponent();
            MainScroll.ParallaxView = HeaderView;
        }

        protected override void OnLoaded()
        {
            BindingContext = ViewModel;
            MainScroll.Parallax();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            MainScroll.Parallax();
        }
    }

    public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
    {
    }
}