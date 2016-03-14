using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Model
{
    class RestaurantResponses
    {

    }

    public class AuthenticationResult
    {
        public bool ok { get; set; }
        public string access_token { get; set; }
        public string userName { get; set; }
        public string token_type { get; set; }
        public DateTime issued { get; set; }
        public DateTime expires { get; set; }
    }
}
