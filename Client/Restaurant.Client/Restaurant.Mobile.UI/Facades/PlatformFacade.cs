using Restaurant.Abstractions.Facades;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Facades
{
    public class PlatformFacade : IPlatformFacade
    {
        public string RuntimePlatform => Device.RuntimePlatform;

        public string Android => Device.Android;

        public string iOS => Device.iOS;

        public string UWP => Device.UWP;
    }
}