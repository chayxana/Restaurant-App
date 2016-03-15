using ReactiveUI;
using Restaurant.ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant
{
    public class BaseContentPage<T> : MainBaseContentPage, IViewFor<T> where T : class, INavigatableViewModel
    {
        public T ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (T)value; }
        }
        public BaseContentPage()
        {
            ViewModel = Locator.Current.GetService<T>();
            BindingContext = ViewModel;
        }
    }

    public class MainBaseContentPage : ContentPage, IColoredPage
    {
        public Color ActionBarBackgroundColor { get; set; }

        public Color ActionBarTextColor { get; set; }

        public Color NavigationBarColor { get; set; }

        public Color StatusBarColor { get; set; }

        protected virtual void Initialize()
        {
        }

        /// <summary>
		/// Wraps the ContentPage within a NavigationPage
		/// </summary>
		/// <returns>The navigation page.</returns>
		public NavigationPage WithinNavigationPage()
        {
            var nav = new ThemedNavigationPage(this);
            ApplyTheme(nav);
            return nav;
        }

        public NavigationPage ToThemedNavigationPage()
        {
            var nav = new NavigationPage(this);
            ApplyTheme(nav);
            return nav;
        }


        public void ApplyTheme(NavigationPage nav)
        {
            nav.BarBackgroundColor = ActionBarBackgroundColor;
            nav.BarTextColor = ActionBarTextColor;
        }

        public void AddDoneButton(string text = "Done", ContentPage page = null)
        {
            var btnDone = new ToolbarItem
            {
                Text = text,
            };

            btnDone.Clicked += async (sender, e) =>
            await Navigation.PopModalAsync();

            page = page ?? this;
            page.ToolbarItems.Add(btnDone);
        }
    }

    public interface IColoredPage
    {
        Color ActionBarTextColor { get; set; }

        Color ActionBarBackgroundColor { get; set; }

        Color NavigationBarColor { get; set; }

        Color StatusBarColor { get; set; }
    }

    public class ThemedNavigationPage : NavigationHost
    {
        public ThemedNavigationPage()
        {
        }

        public ThemedNavigationPage(ContentPage root) : base(root)
        {
        }
    }

    public class ColoredThemedNavigationPage : NavigationPage
    {

    }
}
