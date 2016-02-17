using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Model
{
    public interface IRestaurantApi
    {
        [Post("/api/Account/Register")]
        Task<object> RegesterRaw([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> form);
        
        [Post("/Token")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<AuthenticationResult> GetTokenRaw([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> form);

        [Get("/api/Values")]
        [Headers("Authorization: Bearer")]
        Task<object> GetValues();
    }

    public static class RestaurantApiExtensions
    {
        public static Task<object> Regester(this IRestaurantApi This, string email, string password, string confirmPassword)
        {
            var dict = new Dictionary<string, string>
            {
                { "Email", email },
                { "Password", password},
                { "ConfirmPassword", confirmPassword }
            };
            return This.RegesterRaw(dict);
        }

        public static Task<AuthenticationResult> GetToken(this IRestaurantApi This, string email, string password)
        {
            var dict = new Dictionary<string, string>
            {
                { "grant_type", "password"},
                { "username", email },
                { "password", password}
            };

            return This.GetTokenRaw(dict);
        }        

        public static Task<object> Values(this IRestaurantApi This)
        {
            return This.GetValues();
        }
    }
}
