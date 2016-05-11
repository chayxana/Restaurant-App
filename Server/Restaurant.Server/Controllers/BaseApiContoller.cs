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
        /// Return _dbContext
        /// </summary>
        private ApplicationDbContext _dbContext;

        protected ApplicationDbContext DbContext => _dbContext ?? (_dbContext = new ApplicationDbContext());

        /// <summary>
        /// Returns current _user info
        /// </summary>
        private User _user;

        protected User CurrentUser
        {
            get
            {
                if (_user == null)
                {
                    var identity = User.Identity as ClaimsIdentity;
                    Claim identityClaim = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    _user = DbContext.Users.FirstOrDefault(u => u.Id == identityClaim.Value);
                    return _user;
                }
                return _user;
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