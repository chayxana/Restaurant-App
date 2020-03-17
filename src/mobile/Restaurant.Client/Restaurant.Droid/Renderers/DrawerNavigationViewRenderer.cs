using System.ComponentModel;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restaurant.Abstractions.Enums;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Square.Picasso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Uri = Android.Net.Uri;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(NavigationView), typeof(DrawerNavigationViewRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class DrawerNavigationViewRenderer : ViewRenderer<NavigationView, Android.Support.Design.Widget.NavigationView>
    {
	    public DrawerNavigationViewRenderer(Context context) : base(context)
	    {	
	    }

        private Android.Support.Design.Widget.NavigationView _navView;

        private IMenuItem _previousItem;
        private ImageView _profileImage;
        private TextView _profileName;

        private int _statusBarHeight = -1;

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;


            var view = Inflate(Context, Resource.Layout.nav_drawer, null);
            _navView = view.JavaCast<Android.Support.Design.Widget.NavigationView>();

            _navView.NavigationItemSelected += NavView_NavigationItemSelected;

            SetNativeControl(_navView);

            var header = _navView.GetHeaderView(0);
            _profileImage = header.FindViewById<ImageView>(Resource.Id.profile_image);
            _profileName = header.FindViewById<TextView>(Resource.Id.profile_name);
           
            _profileImage.Click += (sender, e2) => NavigateToProfileDetail();
            _profileName.Click += (sender, e2) => NavigateToProfileDetail();

            _navView.SetCheckedItem(Resource.Id.nav_foods);
        }

	    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
		    base.OnElementPropertyChanged(sender, e);

		    if (Element == null)
			    return;

			if (e.PropertyName == NavigationView.UserEmailProperty.PropertyName)
				UpdateName();
			if(e.PropertyName == NavigationView.UserPictureProperty.PropertyName)
				UpdateImage();
	    }

	    private void NavigateToProfileDetail()
        {
			Element.OnNavigateUserProfileDetails();
        }
		
        private void UpdateName()
        {
	        _profileName.Text = Element.UserEmail;
        }

        private void UpdateImage()
        {
			Picasso.With(Context)
				.Load(Uri.Parse(Element.UserPicture))
				.Into(_profileImage);

			_profileImage.SetScaleType(ImageView.ScaleType.CenterCrop);
		}

		public override void OnViewRemoved(View child)
        {
            base.OnViewRemoved(child);
            _navView.NavigationItemSelected -= NavView_NavigationItemSelected;
        }

        private void NavView_NavigationItemSelected(object sender,
            Android.Support.Design.Widget.NavigationView.NavigationItemSelectedEventArgs e)
        {
            _previousItem?.SetChecked(false);

            _navView.SetCheckedItem(e.MenuItem.ItemId);

            _previousItem = e.MenuItem;

            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_foods:
                    Element.OnNavigationItemSelected(
                        new NavigationItemSelectedEventArgs { SelectedViewModel = NavigationItem.Foods });
                    break;
                case Resource.Id.nav_orders:
                    Element.OnNavigationItemSelected(
                        new NavigationItemSelectedEventArgs { SelectedViewModel = NavigationItem.Orders });
                    break;
                case Resource.Id.nav_chat:
                    Element.OnNavigationItemSelected(
                        new NavigationItemSelectedEventArgs { SelectedViewModel = NavigationItem.Chat });
                    break;
                case Resource.Id.nav_settings:
                    Element.OnNavigationItemSelected(
                        new NavigationItemSelectedEventArgs { SelectedViewModel = NavigationItem.Settings });
                    break;
                case Resource.Id.nav_about:
                    Element.OnNavigationItemSelected(
                        new NavigationItemSelectedEventArgs { SelectedViewModel = NavigationItem.About });
                    break;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            Control.Layout(l, t - GetStatusBarHeight(), r, b + GetStatusBarHeight());
        }

        private int GetStatusBarHeight()
        {
            if (_statusBarHeight >= 0)
                return _statusBarHeight;

            var result = 0;
            var resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
                result = Resources.GetDimensionPixelSize(resourceId);
            return _statusBarHeight = result;
        }
    }
}