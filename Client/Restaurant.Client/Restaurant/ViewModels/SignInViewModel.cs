using ReactiveUI;
using ReactiveUI.Legacy;
using Refit;
using Restaurant.Model;
using Restaurant.Models;
using Splat;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using JetBrains.Annotations;

namespace Restaurant.ViewModels
{
    [UsedImplicitly]
    public class SignInViewModel : ReactiveObject, ISignInViewModel
    {   
        /// <summary>
        /// Gets and sets login command
        /// Command that logins to service
        /// </summary>
        public ICommand Login { get; set; }
        

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
        
        

        /// <summary>
        /// Gets view model title
        /// </summary>
        public string Title => "Authentication";
       

        public SignInViewModel()
        {
            var canLogin = this.WhenAny(x => x.Email, x => x.Password,
                (e, p) => !string.IsNullOrEmpty(e.Value) && !string.IsNullOrEmpty(p.Value));


            Login = ReactiveUI.Legacy.ReactiveCommand
                .CreateAsyncTask(canLogin, async _ =>
                 {
                     IsBusy = true;
                     Debug.WriteLine(Helper.Address);
                     var client = new HttpClient()
                     {
                         BaseAddress = new Uri(Helper.Address)
                     };
                     var api = RestService.For<IRestaurantApi>(client);
                     var token = await api.GetToken(Email, Password);
                     Global.AuthenticationManager.AuthenticatedClient = new HttpClient(new AuthenticatedHttpClientHandler(token.access_token))
                     {
                         BaseAddress = new Uri(Helper.Address)  
                     };
                     var info = await Global.AuthenticationManager.AuthenticatedApi.GetUserInfo();
                     return info;
                 });
            

            //Login.
            //    Subscribe(x => IsBusy = false);

            //Login
            //    .ThrownExceptions
            //    .Subscribe(ex =>
            //    {
            //        Debug.WriteLine("Error! - " + ex.Message);
            //        IsBusy = false;
            //    });

   

        }

        public void NavigateToMainPage(UserInfo user)
        {
            //NavigationScreen.Navigation.NavigateToMainPage.Execute(mainViewModel);
        }
    }
}
