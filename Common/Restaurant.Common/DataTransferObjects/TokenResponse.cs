using System.Net;
using Newtonsoft.Json;

namespace Restaurant.Common.DataTransferObjects
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        public bool IsError { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}