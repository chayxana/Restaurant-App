using Microsoft.Phone.Controls;

namespace Restaurant.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            //global::Xamarin.Forms.Forms.Init();
            //LoadApplication(new Restaurant.App());
        }
    }
}
