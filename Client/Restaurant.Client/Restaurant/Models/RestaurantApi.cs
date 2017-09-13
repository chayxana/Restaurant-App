using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.Model
{
    public interface IRestaurantApi
    {
        [Post("/api/Account/Register")]
        Task<object> RegesterRaw([Body] RegisterDto registerDto);

        //[Post("/Token")]
        //[Headers("Content-Type: application/x-www-form-urlencoded")]
        //Task<AuthenticationResult> GetTokenRaw([Body] LoginDto loginDto);

        [Get("/api/Values")]
        Task<object> GetValues([Header("Authorization: bearer")] string accessToken);

        [Get("/api/Account/UserInfo")]
        Task<UserInfoDto> GetUserInfoRaw([Header("Authorization: bearer")] string accessToken);

        [Get("/api/foods")]
        Task<IEnumerable<FoodDto>> GetFoods();

    }

    public static class RestaurantApiExtensions
    {
        //public static Task<object> Regester(this IRestaurantApi This, string name, string email, string password, string confirmPassword)
        //{
        //    var dict = new Dictionary<string, string>
        //    {
        //        { "Name", name },
        //        { "Email", email },
        //        { "Password", password},
        //        { "ConfirmPassword", confirmPassword }
        //    };
        //    return This.RegesterRaw(dict);
        //}

        //public static Task<AuthenticationResult> GetToken(this IRestaurantApi This, string email, string password)
        //{
        //    var dict = new Dictionary<string, string>
        //    {
        //        { "grant_type", "password"},
        //        { "username", email },
        //        { "password", password}
        //    };

        //    return This.GetTokenRaw(dict);
        //}

        //public static Task<object> Values(this IRestaurantApi This)
        //{
        //    return This.GetValues();
        //}

        //public static Task<UserInfoDto> GetUserInfo(this IRestaurantApi This)
        //{
        //    return This.GetUserInfoRaw();
        //}

        //public static Task<List<FoodDto>> GetFoods(this IRestaurantApi This)
        //{
        //    return This.GetFoods();
        //}
    }
}
