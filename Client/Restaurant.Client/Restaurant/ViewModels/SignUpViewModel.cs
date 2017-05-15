using System;
using System.Diagnostics;
using System.Net.Http;
using ReactiveUI;
using Refit;
using Restaurant.Model;
using Restaurant.Models;
using Splat;

namespace Restaurant.ViewModels
{
    public class SignUpViewModel : ReactiveObject, INavigatableViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { this.RaiseAndSetIfChanged(ref name, value); }
        }


        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set { this.RaiseAndSetIfChanged(ref isLoading, value); }
        }

        private string email;

        public string RegesterEmail
        {
            get { return email; }
            set { this.RaiseAndSetIfChanged(ref email, value); }
        }

        private string password;

        public string RegesterPassword
        {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value); }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref confirmPassword, value);
            }
        }

        public ReactiveUI.Legacy.ReactiveCommand<object> Regester { get; set; }

        public INavigatableScreen NavigationScreen { get; private set; }

        public string Title => "Sign Up";

        public SignUpViewModel(INavigatableScreen screen = null)
        {
            //Gets intance of INavigatableScreen instance
            NavigationScreen = screen ?? Locator.Current.GetService<INavigatableScreen>();

            //Observable for all properties and it will be true when
            //all this properties will be not empty
            var canRegester = this.WhenAny(
                                    x => x.Name,
                                    x => x.RegesterEmail,
                                    x => x.RegesterPassword,
                                    x => x.ConfirmPassword,
                                    (n,e, p, cp) => !string.IsNullOrEmpty(n.Value)
                                                  && !string.IsNullOrEmpty(e.Value)
                                                  && !string.IsNullOrEmpty(p.Value)
                                                  && !string.IsNullOrEmpty(cp.Value) 
                                                  && cp.Value == p.Value

                                );

            //Creating reactive command for regester
            Regester = ReactiveUI.Legacy. ReactiveCommand
                .CreateAsyncTask(canRegester, async _ => 
                {
                    IsLoading = true;
                var client = new HttpClient() // NetCache.UserInitiated)
                    {
                        BaseAddress = new Uri(Helper.Address)
                    };
                    var api = RestService.For<IRestaurantApi>(client);
                    var result = await api.Regester(Name, RegesterEmail, RegesterPassword, ConfirmPassword);
                    return result;
                });

            //When regstir command executing sets true for IsLoading property
            //Regester
            //    .IsExecuting
            //    .Subscribe(x => IsLoading = true);
            
            //Raises when completes regester command
            Regester
                .Subscribe(r => 
                {
                    MessageBus.Current.SendMessage("User regestred!");
                    Debug.WriteLine("Complete!");
                    IsLoading = false;
                });

            //Raises when regester throws any exception
            Regester
                .ThrownExceptions
                .Subscribe(ex =>
                {
                    Debug.WriteLine("Error!");
                    IsLoading = false;
                });
        }
    }
}
