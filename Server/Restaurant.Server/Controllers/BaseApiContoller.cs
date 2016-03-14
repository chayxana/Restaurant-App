using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    public class BaseApiContoller : ApiController
    {
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
    }
}