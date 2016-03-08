using Fusillade;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using Refit;
using Restaurant.Model;
using Restaurant.Models;
using Restaurant.ReactiveUI;
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
    public class AuthenticationViewModel : ReactiveObject, INavigatableViewModel
    {

        private App.ColorTheme theme;

        public App.ColorTheme Theme
        {
            get { return theme; }
            set { this.RaiseAndSetIfChanged(ref theme, value); }
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

        public INavigatableScreen NavigationScreen { get; protected set; }

        public string Title
        {
            get { return "Login"; }
        }

        public AuthenticationViewModel(INavigatableScreen screen = null)
        {
            NavigationScreen = (screen ?? Locator.Current.GetService<INavigatableScreen>());

            var canLogin = this.WhenAny(x => x.Email, x => x.Password, (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));
            Login = ReactiveCommand.CreateAsyncTask<AuthenticationResult>(canLogin, async _ =>
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

            //Login = ReactiveCommand.Create();

            //Login.Subscribe(l =>
            //{
            //    //(HostScreen as AppBotstrapper).RouterHost.PopToRoot.Execute(new MainViewModel(l));
            //    NavigationScreen.Navigation.NavigateAndChangeRoot.Execute(new MainViewModel());
            //});

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
                    var regViewModel = new RegesterViewModel(NavigationScreen);
                    Locator.CurrentMutable.RegisterConstant(regViewModel, typeof(RegesterViewModel));
                    NavigationScreen.Navigation.Navigate.Execute(regViewModel);
                    //(HostScreen as AppBotstrapper).RouterHost.PushAsync.Execute(regViewModel);
                }
                else
                {
                    NavigationScreen.Navigation.Navigate.Execute(viewModel);
                }
            });
            OpenRegester.ThrownExceptions.Subscribe(ex =>
            {

            });

        }
    }
}
