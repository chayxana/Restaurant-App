using System.Threading.Tasks;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Facades
{
    [UsedImplicitly]
    public class NavigationFacade : INavigationFacade
    {
        private INavigation Navigation
        {
            get
            {
                if (App.Current.MainPage is MasterDetailPage masterDetailPage)
                {
                    if (masterDetailPage.Detail is NavigationPage navigationPage)
                        return navigationPage.Navigation;
                    var detailNavigationPage = new NavigationPage(masterDetailPage);
                    return detailNavigationPage.Navigation;
                }
                return App.Current.MainPage.Navigation;
            }
        }

        public Task PushAsync(IViewFor page)
        {
            return Navigation.PushAsync(page as Page, true);
        }

        public Task PushModalAsync(IViewFor page)
        {
            return Navigation.PushModalAsync(page as Page, true);
        }

        public Task PopModalAsync(bool animated)
        {
            return Navigation.PopModalAsync(animated);
        }

        public Task NavigateToMainPage(IViewFor page)
        {
            if (Device.RuntimePlatform == Device.Android)
                App.Current.MainPage = page as Page;
            else if (Device.RuntimePlatform == Device.iOS)
                App.Current.MainPage = new NavigationPage(page as Page);

            return Task.CompletedTask;
        }

        public Task NavigateToMainPageContent(IViewFor page)
        {
            if (App.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.Detail = page as Page;
                return Task.CompletedTask;
            }

            if (App.Current.MainPage is TabbedPage tabbedPage)
            {
                tabbedPage.CurrentPage = page as Page;
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}