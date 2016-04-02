using Microsoft.Phone.Controls;
using NControl.Controls.WP80;

namespace Restaurant.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            NControls.Init();

            //global::Xamarin.Forms.Forms.Init();
            //LoadApplication(new Restaurant.App());
        }
    }
}
