using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace Restaurant.Server.Api.IdentityServer
{
    public static class DefaultUsers
    {
        public static List<TestUser> Get()
        {
            var users = new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "9aeb893d-0515-4f00-91b5-3fcf7a0899b6",
                    Username = "user",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "994AE505-8929-46CC-858C-527EBE82D353",
                    Username = "admin",
                    Password = "adminPassword",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Role, "Admin")
                    }
                }
            };
            return users;
        }
    }
}