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
            HostScreen = (screen ?? Locator.Current.GetService<IScreen>());
            
            var canLogin = this.WhenAny(x => x.Email, x => x.Password, (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));
            Login = ReactiveCommand.CreateAsyncTask(canLogin, async _ =>
            {
                Debug.WriteLine(Helper.Address);
                var client = new HttpClient(NetCache.UserInitiated)
                {
                    BaseAddress = new Uri(Helper.Address)
                };

                var api = RestService.For<IRestaurantApi>(client);
                var token = await api.GetToken(Email, Password);
                return token;
            });

            Login.Subscribe(l => 
            {
                //(HostScreen as AppBotstrapper).RouterHost.PopToRoot.Execute(new MainViewModel(l));
                HostScreen.Router.NavigateAndReset.Execute(new MainViewModel(l));
            });

            Login.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw("Invalid login or password!");
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
                    HostScreen.Router.Navigate.Execute(regViewModel);
                    //(HostScreen as AppBotstrapper).RouterHost.PushAsync.Execute(regViewModel);
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
