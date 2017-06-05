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
        }

        protected override void OnLoaded()
        {

        }
    }

    public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
    {   
    }
}