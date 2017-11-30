using System.Net;
using Newtonsoft.Json;

namespace Restaurant.Common.DataTransferObjects
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
		
        public string TokenType { get; set; }

        public string RefreshToken { get; set; }
		
        public long ExpiresIn { get; set; }

        public bool IsError { get; set; }
		
        public string Error { get; set; }
		
        public string ErrorDescription { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}