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

        private ColorTheme theme;

        public ColorTheme Theme
        {
            get { return theme; }
            set { this.RaiseAndSetIfChanged(ref theme, value); }
        }


        public ReactiveCommand<object> Login { get; set; }
        //public ReactiveCommand<AuthenticationResult> Login { get; set; }

        public ReactiveCommand<object> OpenRegester { get; set; }

        public ReactiveCommand<object> OpenLogin { get; set; }

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

        private string authenticationStatus;

        public string AuthenticationStatus
        {
            get { return authenticationStatus; }
            set { this.RaiseAndSetIfChanged(ref authenticationStatus, value); }
        }

        public INavigatableScreen NavigationScreen { get; protected set; }

        public string Title
        {
            get { return "Authentication"; }
        }

        public AuthenticationViewModel(INavigatableScreen screen = null)
        {
            NavigationScreen = (screen ?? Locator.Current.GetService<INavigatableScreen>());

            var canLogin = this.WhenAny(x => x.Email, x => x.Password,
                (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));


            //Login = ReactiveCommand.CreateAsyncTask(canLogin, async _ =>
            // {
            //     Debug.WriteLine(Helper.Address);
            //     var client = new HttpClient(NetCache.UserInitiated)
            //     {
            //         BaseAddress = new Uri(Helper.Address)
            //     };
            //     var api = RestService.For<IRestaurantApi>(client);
            //     var token = await api.GetToken(Email, Password);
            //     return token;
            // });

            //Login.Subscribe(token =>
            //{
            //    var mainViewModel = new MainViewModel(new ClientUser(null));
            //    NavigationScreen.Navigation.NavigateAndChangeRoot.Execute(mainViewModel);
            //});

            Login = ReactiveCommand.Create();
            Login.Subscribe(_ => 
            {
                var mainViewModel = new MainViewModel(new ClientUser(null));
                NavigationScreen.Navigation.NavigateAndChangeRoot.Execute(mainViewModel);
            });
            Login.ThrownExceptions.Subscribe(ex =>
            {
                UserError.Throw("Invalid login or password!");
                Debug.WriteLine("Error! - " + ex.Message);
            });


            #region OpenRegester
            OpenRegester = ReactiveCommand.Create();
            OpenRegester.Subscribe(x =>
            {
                var viewModel = Locator.Current.GetService<SignUpViewModel>();
                if (viewModel == null)
                {
                    var regViewModel = new SignUpViewModel(NavigationScreen);
                    Locator.CurrentMutable.RegisterConstant(regViewModel, typeof(SignUpViewModel));
                    NavigationScreen.Navigation.Navigate.Execute(regViewModel);
                }
                else
                {
                    NavigationScreen.Navigation.Navigate.Execute(viewModel);
                }
            });


            OpenLogin = ReactiveCommand.Create();
            OpenLogin.Subscribe(x =>
            {
                var authenViewModel = Locator.Current.GetService<AuthenticationViewModel>();
                NavigationScreen.Navigation.Navigate.Execute(authenViewModel);
            });
            #endregion

        }
    }
}
