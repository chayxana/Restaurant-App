using Fusillade;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using Refit;
using Restaurant.Model;
using Restaurant.Models;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; set; }

        public string UrlPathSegment
        {
            get { return "Login"; }
        }
        public ReactiveCommand<AuthenticationResult> Login { get; set; }

        public ReactiveCommand<object> OpenRegester { get; set; }

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
            
            var canLogin = this.WhenAny(x => x.Email, x => x.Password, (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));
            Login = ReactiveCommand.CreateAsyncTask(canLogin, async _ =>
            {
                var client = new HttpClient(NetCache.UserInitiated)
                {
                    BaseAddress = new Uri(Helper.address)
                };

                var api = RestService.For<IRestaurantApi>(client);
                var token = await api.GetToken(Email, Password);
                return token;
            });

            Login.Subscribe(l => Helper.Token = l.access_token);

            Login.ThrownExceptions.Subscribe(ex =>
            {
                Debug.WriteLine("Error! - " + ex.Message);
            });




            OpenRegester = ReactiveCommand.Create();
            OpenRegester.Subscribe(x =>
            {
                var viewModel = Locator.Current.GetService<RegesterViewModel>();
                if (viewModel == null)
                {
                    var regViewModel = new RegesterViewModel(HostScreen);
                    Locator.CurrentMutable.RegisterConstant(regViewModel, typeof(RegesterViewModel));
                    HostScreen.Router.Navigate.Execute(viewModel);
                }
                else
                {
                    HostScreen.Router.Navigate.Execute(viewModel);
                }
            });
            OpenRegester.ThrownExceptions.Subscribe(ex =>
            {

            });

        }
    }
}
