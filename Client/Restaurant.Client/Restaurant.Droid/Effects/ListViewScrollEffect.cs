using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;
using ListViewScrollEffect = Restaurant.Droid.Effects.ListViewScrollEffect;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using System.Threading.Tasks;

[assembly: ExportEffect(typeof(ListViewScrollEffect), "ListViewScrollEffect")]
namespace Restaurant.Droid.Effects
{
	public class ListViewScrollEffect : PlatformEffect
	{
		private ListView _control;
		NavigationPage page;

		protected override void OnAttached()
		{
			_control = Control as ListView;
			if (_control != null)
				_control.Scroll += _control_Scroll;

			if (App.Current.MainPage is MasterDetailPage master)
			{
				page = master.Detail as NavigationPage;
			}
			else
			{
				page = App.Current.MainPage as NavigationPage;
			}
		}

		private async void _control_Scroll(object sender, AbsListView.ScrollEventArgs e)
		{
			var c = _control.GetChildAt(0);
			int scrolly = -c.Top + _control.FirstVisiblePosition * c.Height;
			if (scrolly < 20)
				await Task.Delay(250);
			
			// TODO: When scrolls to down we should animate toolbar to top together with layout 
			// NavigationPage.SetHasNavigationBar(page.CurrentPage, !(scrolly > 20));
		}

		protected override void OnDetached()
		{
			_control.Scroll -= _control_Scroll;
		}
	}
}