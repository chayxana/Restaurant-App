using ReactiveUI;
using Xamarin.Forms;

namespace Restaurant
{
    public class BaseContentPage<T> : MainBaseContentPage, IViewFor<T> where T : class
    {
        public T ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (T)value;
        }
    }

    public class MainBaseContentPage : ContentPage, IColoredPage
    {
        public Color ActionBarBackgroundColor { get; set; }

        public Color ActionBarTextColor { get; set; }

        public Color NavigationBarColor { get; set; }

        public Color StatusBarColor { get; set; }

        bool _hasSubscribed;

        public bool HasInitialized
        {
            get;
            private set;
        }

        public MainBaseContentPage()
        {
            SubscribeToAuthentication();
            SubscribeToIncomingPayload();
        }

        private void SubscribeToAuthentication()
        {
            //SubscribeToAuthenTication
        }

        protected override void OnAppearing()
        {
            if (!_hasSubscribed)
            {
                SubscribeToAuthentication();
                SubscribeToIncomingPayload();
                _hasSubscribed = true;
            }

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

        private void SubscribeToIncomingPayload()
        {
            // TODO: Notification
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void OnLoaded()
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


        protected void ApplyTheme(NavigationPage nav)
        {
            nav.BarBackgroundColor = ActionBarBackgroundColor;
            nav.BarTextColor = ActionBarTextColor;
        }

        public void AddDoneButton(string text = "Done", ContentPage page = null)
        {
            var btnDone = new ToolbarItem
            {
                Text = text
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

    public class ThemedNavigationPage : NavigationPage
    {
        public ThemedNavigationPage()
        {
        }

        public ThemedNavigationPage(ContentPage root) : base(root)
        {
        }
    }

}
