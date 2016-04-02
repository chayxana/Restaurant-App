namespace Restaurant.Models
{
    public class ClientUser
    {
        public ClientUser(string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
        }
        public string AuthenticationToken { get; set; }

        public string UserId { get; set; }
    }
}
