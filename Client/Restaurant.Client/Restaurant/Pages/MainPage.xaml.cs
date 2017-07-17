using Autofac;
using ReactiveUI;
using Restaurant.ViewModels;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainPage : MainPageXaml
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<IMainViewModel>();
            Master = new MasterPage();
            var view = App.Container.Resolve<IViewFor<FoodsViewModel>>();
            view.ViewModel = App.Container.Resolve<FoodsViewModel>();
            var page = view as Page;

            Detail = new NavigationPage(page);
        }
    }

    public class MainPageXaml : BaseMasterDetailPage<MainViewModel>
    {

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
