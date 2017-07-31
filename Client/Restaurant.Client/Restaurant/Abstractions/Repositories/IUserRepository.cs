using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DataTransferObjects;
using Restaurant.Model;

namespace Restaurant.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserInfoDto> GetUserInfo();
    }
}
