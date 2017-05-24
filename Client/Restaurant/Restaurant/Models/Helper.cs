using Xamarin.Forms;

namespace Restaurant.Models
{
    public class Helper
    {
        public static string Address
        {
            get
            {
                if(Device.OS == TargetPlatform.iOS)
                {
                    return "http://192.168.127.1:8080/";
                }
                if(Device.OS == TargetPlatform.Android)
                {
                    return "http://10.71.34.1:8080/";
                }
                return "http://localhost:8080/";
            } 
        }        
    }
}
