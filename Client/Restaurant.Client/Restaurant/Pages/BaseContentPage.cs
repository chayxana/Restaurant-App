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
		
        public Color StatusBarColor { get; set; }

	    public bool IsTransparentToolbar { get; set; }

	    protected override void OnAppearing()
        {
	        if (Parent is NavigationPage nav)
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

	    private void ApplyTheme(NavigationPage nav)
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

        Color StatusBarColor { get; set; }

		bool IsTransparentToolbar { get; set; }
    }
}
