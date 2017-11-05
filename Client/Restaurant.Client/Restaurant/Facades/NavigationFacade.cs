using System.Threading.Tasks;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Xamarin.Forms;

namespace Restaurant.Facades
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
	}
}