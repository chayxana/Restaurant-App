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
        private double _imageHeight;

        public FoodDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            BindingContext = ViewModel;

            scrollView.Scrolled += (sender, e) => Parallax();
            Parallax();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void Parallax()
        {
            if (_imageHeight <= 0)
                _imageHeight = photoImage.Height;

            var y = scrollView.ScrollY * .4;
            if (y >= 0)
            {
                //Move the Image's Y coordinate a fraction of the ScrollView's Y position
                photoImage.Scale = 1;
                photoImage.TranslationY = y;
            }
            else
            {
                //Calculate a scale that equalizes the height vs scroll
                double newHeight = _imageHeight + (scrollView.ScrollY * -1);
                photoImage.Scale = newHeight / _imageHeight;
                photoImage.TranslationY = scrollView.ScrollY / 2;
            }
        }
    }

    public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
    {
    }
}