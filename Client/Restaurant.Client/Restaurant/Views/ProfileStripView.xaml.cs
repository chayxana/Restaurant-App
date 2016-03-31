using Restaurant.ViewModels;
using Splat;
using Xamarin.Forms;

namespace Restaurant.Views
{
    public partial class ProfileStripView : ContentView
    {
        public ProfileStripView()
        {
            InitializeComponent();
            root.BindingContext = Locator.Current.GetService<MainViewModel>();
        }
    }
}
