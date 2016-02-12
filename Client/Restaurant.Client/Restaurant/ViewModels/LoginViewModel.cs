using Fusillade;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using Refit;
using Restaurant.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        string address = "http://192.168.56.1:13900/";

        public IScreen HostScreen { get; set; }

        public string UrlPathSegment
        {
            get { return "Login"; }
        }
        public ReactiveCommand<string> Login { get; set; }

        private string email;

        public string Email
        {
            get { return email; }
            set { this.RaiseAndSetIfChanged(ref email, value); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value); }
        }
        public LoginViewModel(IScreen screen = null)
        {

            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
            var canLogin = this.WhenAny(x => x.Password, x => !string.IsNullOrEmpty(x.Value));
            Login = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                //var api = RestService.For<IRestaurantApi>(new HttpClient(new AuthenticatedHttpClientHandler(GetToken)) { BaseAddress = new Uri(address) });

                var client = new HttpClient(NetCache.UserInitiated)
                {
                    BaseAddress = new Uri(address)
                };

                var api = RestService.For<IRestaurantApi>(client);
                var a = await api.GetToken(Email, Password);
                //var result = await GetToken();
                return "";
            });
            Login.Subscribe(l =>
            {
                Debug.WriteLine("Bla bla!");
            });

            Login.ThrownExceptions.Subscribe(ex =>
            {
                Debug.WriteLine("Error! - " + ex.Message);
            });
        }

        public async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Email),
                    new KeyValuePair<string, string>("password", Password),
                });

                //send request
                HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);

                //get access token from response body
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseJson);
                return jObject.GetValue("access_token").ToString();
            }
        }
    }

    class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly Func<Task<string>> getToken;

        public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
        {
            if (getToken == null) throw new ArgumentNullException("getToken");
            this.getToken = getToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var token = await getToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
