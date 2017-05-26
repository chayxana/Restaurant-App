using Xamarin.Forms;
using static Xamarin.Forms.Device;
using static Xamarin.Forms.TargetPlatform;

namespace Restaurant.Models
{
    public class Helper
    {
        public static string Address
        {
            get
            {
                if (RuntimePlatform == Device.iOS)
                {
                    return "http://192.168.127.1:8080/";
                }
                if (RuntimePlatform == Device.Android)
                {
                    return "http://10.71.34.1:8080/";
                }
                return "http://localhost:8080/";
            }
        }
    }
}
