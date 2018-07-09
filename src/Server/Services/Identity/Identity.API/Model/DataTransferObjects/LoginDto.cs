using Newtonsoft.Json;

namespace Identity.API.Model.DataTransferObjects
{
    public class LoginDto
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}