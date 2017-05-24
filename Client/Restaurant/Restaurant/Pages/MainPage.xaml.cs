using Autofac;
using ReactiveUI;
using Restaurant.ViewModels;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Pages
{
    public partial class MainPage : MainPageXaml
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<IMainViewModel>();
            Master = new MasterPage();

            var page = App.Container.Resolve<IViewFor<FoodsViewModel>>() as Page;

            Detail = new NavigationPage(page);
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MainPageXaml : BaseMasterDetailPage<MainViewModel>, IDetailedScreen
    {
        public DetailState DetailState { get; set; }

        protected MainPageXaml()
        {
        }
    }

    public class BaseMasterDetailPage<T> : MainBaseMasterDetailPage, IViewFor<T> where T : class
    {

        public T ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;

            set => ViewModel = (T)value;
        }

        protected BaseMasterDetailPage()
        {         
        }
    }

    public class MainBaseMasterDetailPage : MasterDetailPage, IColoredPage
    {
        public Color ActionBarTextColor { get; set; }

        public Color ActionBarBackgroundColor { get; set; }

        public Color StatusBarColor { get; set; }

        public Color NavigationBarColor { get; set; }
        
    }
}
