using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    return "http://192.168.163.1:13900/";
                }
                else if(Device.OS == TargetPlatform.Android)
                {
                    return "http://10.71.34.1:13900/";
                }
                return "http://localhost:13900";
            } 
        }        
    }
}
