using System.Threading.Tasks;
using Android.Widget;
using Restaurant.Droid.Effects;
using Restaurant.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using ListView = Android.Widget.ListView;

[assembly: ExportEffect(typeof(ListViewScrollEffect), "ListViewScrollEffect")]

namespace Restaurant.Droid.Effects
{
    public class ListViewScrollEffect : PlatformEffect
    {
        private ListView _control;
        private NavigationPage page;

        protected override void OnAttached()
        {
            _control = Control as ListView;
            if (_control != null)
                _control.Scroll += _control_Scroll;

            if (App.Current.MainPage is MasterDetailPage master)
                page = master.Detail as NavigationPage;
            else
                page = App.Current.MainPage as NavigationPage;
        }

        private async void _control_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            var c = _control.GetChildAt(0);
            var scrolly = -c.Top + _control.FirstVisiblePosition * c.Height;
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