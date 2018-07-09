using Restaurant.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetailPage : FoodDetailPageXaml, ITransparentActionBarPage
    {
        public FoodDetailPage()
        {
            InitializeComponent();
            IsTransparentActionBar = true;
        }

        public bool IsTransparentActionBar { get; }

        protected override void OnLoaded()
        {
            BindingContext = ViewModel;
            //var parent = Parent as CustomNavigationPage;
            //if(parent != null)
            //{
            //    //StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
            //    parent.BarBackgroundColor = Color.Transparent;
            //}
        }
    }

    public abstract class FoodDetailPageXaml : BaseContentPage<FoodDetailViewModel>
    {
    }
}