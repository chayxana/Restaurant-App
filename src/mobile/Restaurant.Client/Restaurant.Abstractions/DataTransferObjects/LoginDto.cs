using Newtonsoft.Json;

namespace Restaurant.Abstractions.DataTransferObjects
{
    public class LoginDto
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}