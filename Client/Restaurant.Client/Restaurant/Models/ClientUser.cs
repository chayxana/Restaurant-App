using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class ClientUser
    {
        //
        // Summary:
        //     A Microsoft Azure Mobile Services authentication token for the user given by
        //     the Microsoft.WindowsAzure.MobileServices.MobileServiceUser.UserId. If non-null,
        //     the authentication token will be included in all requests made to the Microsoft
        //     Azure Mobile Service, allowing the client to perform all actions on the Microsoft
        //     Azure Mobile Service that require authenticated-user level permissions.
        public string MobileServiceAuthenticationToken { get; set; }
        //
        // Summary:
        //     Gets or sets the user id of a successfully authenticated user.
        public string UserId { get; set; }
    }
}
