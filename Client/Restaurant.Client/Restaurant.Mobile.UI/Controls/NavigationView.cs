using System;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public class NavigationView : ContentView
    {
        public void OnNavigationItemSelected(NavigationItemSelectedEventArgs e)
        {
            NavigationItemSelected?.Invoke(this, e);
        }

        public event EventHandler<NavigationItemSelectedEventArgs> NavigationItemSelected;
    }
	
    public class NavigationItemSelectedEventArgs : EventArgs
    {
        public Type SelectedViewModel { get; set; }
    }
}
