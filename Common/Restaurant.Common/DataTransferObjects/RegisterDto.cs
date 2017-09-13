using Newtonsoft.Json;

namespace Restaurant.Common.DataTransferObjects
{
    public class RegisterDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("confirm_password")]
        public string ConfirmPassword { get; set; }
    }
}
