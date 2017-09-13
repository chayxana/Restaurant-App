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

        protected override void OnAppearing()
        {
            var nav = Parent as NavigationPage;
            if (nav != null)
            {
                ApplyTheme(nav);
            }

            base.OnAppearing();
            OnLoaded();
        }

        protected virtual void Initialize()
        {
        }

        protected abstract void OnLoaded();

        protected virtual void UnLoad()
        {   
        }

        protected void ApplyTheme(NavigationPage nav)
        {
            nav.BarBackgroundColor = ActionBarBackgroundColor;
            nav.BarTextColor = ActionBarTextColor;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UnLoad();
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
