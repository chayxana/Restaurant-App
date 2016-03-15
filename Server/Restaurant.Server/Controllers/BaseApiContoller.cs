using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    public class BaseApiContoller : ApiController
    {
        /// <summary>
        /// Return context
        /// </summary>
        private ApplicationDbContext context;
        public ApplicationDbContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new ApplicationDbContext();
                }
                return context;
            }
        }

        /// <summary>
        /// Returns current user info
        /// </summary>
        private User user;
        public User CurrentUser
        {
            get
            {
                if (user == null)
                {
                    var identity = User.Identity as ClaimsIdentity;
                    Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    user = Context.Users.FirstOrDefault(u => u.Id == identityClaim.Value);
                    return user;
                }
                return user;
            }
        }
    }

    /// <summary>
    /// Helper class to return the result of the action
    /// </summary>
    internal class ResultResponce : IHttpActionResult
    {
        public bool IsSucceeded { get; set; }

        public Dictionary<string, Exception> Errors { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
      
    }
}