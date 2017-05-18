using System.Threading.Tasks;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Xamarin.Forms;

namespace Restaurant.Facades
{
    public class NavigationFacade : INavigationFacade
    {
        private INavigation Navigation => App.Current.MainPage.Navigation;

        public Task PushAsync(IViewFor page)
        {
            return Navigation.PushAsync(page as Page, true);
        }

        public Task PushModalAsync(IViewFor page)
        {
            return Navigation.PushModalAsync(page as Page, true);
        }
    }
}
