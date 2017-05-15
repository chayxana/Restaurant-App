using ReactiveUI;
using ReactiveUI.Legacy;
using Refit;
using Restaurant.Model;
using Restaurant.Models;
using Splat;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace Restaurant.ViewModels
{
    public class AuthenticationViewModel : ReactiveObject
    {   
        /// <summary>
        /// Gets and sets login command
        /// Command that logins to service
        /// </summary>
        public ReactiveCommand<UserInfo> Login { get; set; }

        /// <summary>
        /// Gets and sets Open regester, 
        /// Command that opens regester page
        /// </summary>
        public ReactiveCommand<object> OpenRegester { get; set; }

        /// <summary>
        /// Gets and sets OpenLogin
        /// Command thats opens login page 
        /// </summary>
        public ReactiveCommand<object> OpenLogin { get; set; }


        private bool isBusy;
        /// <summary>
        /// Gets and sets IsBusy
        /// When some command executing we will provide this property some value 
        /// for control UI
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { this.RaiseAndSetIfChanged(ref isBusy, value); }
        }

        private string email;
        /// <summary>
        /// Gets and sets user Email
        /// </summary>
        public string Email
        {
            get { return email; }
            set { this.RaiseAndSetIfChanged(ref email, value); }
        }

        private string password;
        /// <summary>
        /// Gets and sets non encrypted user passwords
        /// </summary>
        public string Password
        {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value); }
        }

        private string authenticationStatus;
        /// <summary>
        /// Gets and sets status of authentication
        /// </summary>
        public string AuthenticationStatus
        {
            get { return authenticationStatus; }
            set { this.RaiseAndSetIfChanged(ref authenticationStatus, value); }
        }

        /// <summary>
        /// Gets and sets Navigation Screeen
        /// instance that provide navigation beetween view models
        /// </summary>
        //public INavigatableScreen NavigationScreen { get; protected set; }

        /// <summary>
        /// Gets view model title
        /// </summary>
        public string Title => "Authentication";
       

        public AuthenticationViewModel()
        {
            //NavigationScreen = (screen ?? Locator.Current.GetService<INavigatableScreen>());

            var canLogin = this.WhenAny(x => x.Email, x => x.Password,
                (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));


            Login = ReactiveUI.Legacy.ReactiveCommand
                .CreateAsyncTask(canLogin, async _ =>
                 {
                     IsBusy = true;
                     AuthenticationStatus = "started logging...";
                     Debug.WriteLine(Helper.Address);
                     var client = new HttpClient()
                     {
                         BaseAddress = new Uri(Helper.Address)
                     };
                     var api = RestService.For<IRestaurantApi>(client);
                     var token = await api.GetToken(Email, Password);
                     AuthenticationStatus = "started authentication...";
                     Global.AuthenticationManager.AuthenticatedClient = new HttpClient(new AuthenticatedHttpClientHandler(token.access_token))
                     {
                         BaseAddress = new Uri(Helper.Address)
                     };
                     var info = await Global.AuthenticationManager.AuthenticatedApi.GetUserInfo();
                     return info;
                 });
            

            Login.
                Subscribe(x => IsBusy = false);

            Login
                .ThrownExceptions
                .Subscribe(ex =>
                {
                    UserError.Throw("Invalid login or password!");
                    Debug.WriteLine("Error! - " + ex.Message);
                    IsBusy = false;
                });


            #region OpenRegester
            OpenRegester = ReactiveUI.Legacy.ReactiveCommand.Create();
            OpenRegester.Subscribe(x =>
            {
                var viewModel = Locator.Current.GetService<SignUpViewModel>();
                if (viewModel == null)
                {
                    //var regViewModel = new SignUpViewModel(NavigationScreen);
                    //Locator.CurrentMutable.RegisterConstant(regViewModel, typeof(SignUpViewModel));
                    //NavigationScreen.Navigation.Navigate.Execute(regViewModel);
                }
                else
                {
                    //NavigationScreen.Navigation.Navigate.Execute(viewModel);
                }
            });


            OpenLogin = ReactiveUI.Legacy.ReactiveCommand.Create();
            OpenLogin.Subscribe(x =>
            {
                var authenViewModel = Locator.Current.GetService<AuthenticationViewModel>();
                //NavigationScreen.Navigation.Navigate.Execute(authenViewModel);
            });
            #endregion

        }

        public void NavigateToMainPage(UserInfo user)
        {
            user.Picture = Helper.Address + "/" + user.Picture;
            var mainViewModel = new MainViewModel(user);
            //NavigationScreen.Navigation.NavigateToMainPage.Execute(mainViewModel);
        }
    }
}
