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
    }
}
