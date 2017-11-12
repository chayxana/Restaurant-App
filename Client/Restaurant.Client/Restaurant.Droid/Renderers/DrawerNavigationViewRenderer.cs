using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Restaurant.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Restaurant.Controls.NavigationView), typeof(DrawerNavigationViewRenderer))]
namespace Restaurant.Droid.Renderers
{
	public class DrawerNavigationViewRenderer : ViewRenderer<Controls.NavigationView, NavigationView>
	{
		NavigationView navView;
		ImageView profileImage;
		TextView profileName;

		protected override void OnElementChanged(ElementChangedEventArgs<Restaurant.Controls.NavigationView> e)
		{

			base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
				return;


			var view = Inflate(Forms.Context, Resource.Layout.nav_drawer, null);
			navView = view.JavaCast<NavigationView>();


			navView.NavigationItemSelected += NavView_NavigationItemSelected;

			SetNativeControl(navView);

			var header = navView.GetHeaderView(0);
			profileImage = header.FindViewById<ImageView>(Resource.Id.profile_image);
			profileName = header.FindViewById<TextView>(Resource.Id.profile_name);

			profileImage.Click += (sender, e2) => NavigateToLogin();
			profileName.Click += (sender, e2) => NavigateToLogin();

			UpdateName();
			UpdateImage();

			navView.SetCheckedItem(Resource.Id.nav_foods);
		}

		void NavigateToLogin()
		{
			//if (Settings.Current.IsLoggedIn)
			//    return;

		}

		void SettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//if (e.PropertyName == nameof(Settings.Current.Email))
			//{
			//    UpdateName();
			//    UpdateImage();
			//}
		}

		void UpdateName()
		{
			profileName.Text = "Jurabek";
		}

		void UpdateImage()
		{
			//Koush.UrlImageViewHelper.SetUrlDrawable(profileImage, Settings.Current.UserAvatar, Resource.Drawable.profile_generic);
		}

		public override void OnViewRemoved(Android.Views.View child)
		{
			base.OnViewRemoved(child);
			navView.NavigationItemSelected -= NavView_NavigationItemSelected;
		}

		IMenuItem previousItem;

		void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			if (previousItem != null)
				previousItem.SetChecked(false);

			navView.SetCheckedItem(e.MenuItem.ItemId);

			previousItem = e.MenuItem;

			int id = 0;
			switch (e.MenuItem.ItemId)
			{
				//break;
			}
			//this.Element.OnNavigationItemSelected(new XamarinEvolve.Clients.UI.NavigationItemSelectedEventArgs
			//    {

			//        Index = id
			//    });
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);
			
			Control.Layout(l, t - GetStatusBarHeight(), r, b + GetStatusBarHeight());
		}

		int _statusBarHeight = -1;
		private int GetStatusBarHeight()
		{
			if (_statusBarHeight >= 0)
				return _statusBarHeight;

			var result = 0;
			int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
			if (resourceId > 0)
				result = Resources.GetDimensionPixelSize(resourceId);
			return _statusBarHeight = result;
		}
	}
}

