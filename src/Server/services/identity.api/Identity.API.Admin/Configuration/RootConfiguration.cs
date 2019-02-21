using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Identity.API.Admin.Configuration.Interfaces;

namespace Identity.API.Admin.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public IAdminConfiguration AdminConfiguration { get; set; }

        public RootConfiguration(IOptions<AdminConfiguration> adminConfiguration)
        {
            AdminConfiguration = adminConfiguration.Value;
        }
    }
}
