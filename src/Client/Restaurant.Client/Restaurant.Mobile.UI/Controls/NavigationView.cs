using System;
using Restaurant.Abstractions.Enums;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public class NavigationView : ContentView
    {
	    public static readonly BindableProperty UserPictureProperty =
		    BindableProperty.Create("UserPicture", typeof(string), typeof(NavigationView), default(string));

	    public static readonly BindableProperty UserEmailProperty =
		    BindableProperty.Create("UserEmail", typeof(string), typeof(NavigationView), default(string));
		

	    public string UserPicture
	    {
		    get => (string)GetValue(UserPictureProperty);
		    set => SetValue(UserPictureProperty, value);
	    }

	    public string UserEmail
	    {
		    get => (string)GetValue(UserEmailProperty);
		    set => SetValue(UserEmailProperty, value);
	    }

		public event EventHandler<NavigationItemSelectedEventArgs> NavigationItemSelected;

		public void OnNavigationItemSelected(NavigationItemSelectedEventArgs e)
		{
			NavigationItemSelected?.Invoke(this, e);
		}

	    public event EventHandler<EventArgs> NavigateUserProfileDetails;

	    public void OnNavigateUserProfileDetails()
	    {
		    NavigateUserProfileDetails?.Invoke(this, EventArgs.Empty);
	    }
    }

    public class NavigationItemSelectedEventArgs : EventArgs
    {
        public NavigationItem SelectedViewModel { get; set; }
    }
}