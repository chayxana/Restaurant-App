using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Abstractions.Repositories;
using Restaurant.Model;

namespace Restaurant.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRestaurantApi _api;

        public UserRepository(IRestaurantApi api)
        {
            _api = api;
        }
        public Task<UserInfoDto> GetUserInfo()
        {
            return _api.GetUserInfoRaw("");
        }
    }
}
