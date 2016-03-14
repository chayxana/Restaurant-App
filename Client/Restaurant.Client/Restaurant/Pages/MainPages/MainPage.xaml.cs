using ReactiveUI;
using Restaurant.ReactiveUI;
using Restaurant.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages.MainPages
{
    public partial class MainPage : MainPageXaml
    {
        public MainPage()
        {
            InitializeComponent();
            Master = new MasterPage(ViewModel);
            Detail = new FoodsPage().ToThemedNavigationPage();
            AddDoneButton();
        }
    }

    public class MainPageXaml : BaseMasterDetailPage<MainViewModel>
    {

    }

    public class BaseMasterDetailPage<T> : MainBaseMasterDetailPage, IViewFor<T> where T : class, INavigatableViewModel
    {

        public T ViewModel
        {
            get; set;
        }

        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create<BaseMasterDetailPage<T>, T>(x => x.ViewModel, default(T), BindingMode.OneWay);


        object IViewFor.ViewModel
        {
            get { return ViewModel; }

            set { ViewModel = (T)value; }
        }
        public BaseMasterDetailPage()
        {
            ViewModel = Locator.Current.GetService<T>();
            BindingContext = ViewModel;
        }
    }

    public class MainBaseMasterDetailPage : MasterDetailPage, IColoredPage
    {
        public Color ActionBarTextColor { get; set; }

        public Color ActionBarBackgroundColor { get; set; }

        public Color StatusBarColor { get; set; }

        public Color NavigationBarColor { get; set; }

        public void AddDoneButton(string text = "Done")
        {
            var btnDone = new ToolbarItem
            {
                Icon = "ic_plus.png"
            };

            btnDone.Clicked += async (sender, e) =>
            await Navigation.PopModalAsync();

            this.ToolbarItems.Add(btnDone);
        }
    }
}
