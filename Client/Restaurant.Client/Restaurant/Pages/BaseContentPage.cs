using ReactiveUI;
using Xamarin.Forms;

namespace Restaurant.Pages
{
    public abstract class BaseContentPage<T> : MainBaseContentPage, IViewFor<T> where T : class
    {
        public T ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (T)value;
        }
    }

    public abstract class MainBaseContentPage : ContentPage, IColoredPage
    {
        public Color ActionBarBackgroundColor { get; set; }

        public Color ActionBarTextColor { get; set; }

        public Color NavigationBarColor { get; set; }

        public Color StatusBarColor { get; set; }
        
        public bool HasInitialized
        {
            get;
            private set;
        }
        
        protected override void OnAppearing()
        {
            var nav = Parent as NavigationPage;
            if (nav != null)
            {
                nav.BarBackgroundColor = ActionBarBackgroundColor;
                nav.BarTextColor = ActionBarTextColor;
            }

            if (!HasInitialized)
            {
                HasInitialized = true;
                OnLoaded();
            }
            base.OnAppearing();
        }
        protected virtual void Initialize()
        {
        }

        protected abstract void OnLoaded();

        /// <summary>
        /// Wraps the ContentPage within a NavigationPage
        /// </summary>
        /// <returns>The navigation page.</returns>
        public NavigationPage WithinNavigationPage()
        {
            var nav = new NavigationPage(this);
            ApplyTheme(nav);
            return nav;
        }

        protected void ApplyTheme(NavigationPage nav)
        {
            nav.BarBackgroundColor = ActionBarBackgroundColor;
            nav.BarTextColor = ActionBarTextColor;
        }
    }

    public interface IColoredPage
    {
        Color ActionBarTextColor { get; set; }

        Color ActionBarBackgroundColor { get; set; }

        Color NavigationBarColor { get; set; }

        Color StatusBarColor { get; set; }
    }
}
