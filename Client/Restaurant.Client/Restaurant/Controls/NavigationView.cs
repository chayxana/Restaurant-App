using System;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class NavigationView : ContentView
    {
        public void OnNavigationItemSelected(NavigationItemSelectedEventArgs e)
        {
            NavigationItemSelected?.Invoke(this, e);
        }

        public event EventHandler<NavigationItemSelectedEventArgs> NavigationItemSelected;
    }

    [UsedImplicitly]
    public class NavigationItemSelectedEventArgs : EventArgs
    {
        public Type SelectedViewModel { get; set; }
    }
}
