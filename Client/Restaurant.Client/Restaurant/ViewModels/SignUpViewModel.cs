using Fusillade;
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
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class SignUpViewModel : ReactiveObject, INavigatableViewModel
    {
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

        public ReactiveCommand<object> Regester { get; set; }

        public INavigatableScreen NavigationScreen { get; private set; }

        public string Title
        {
            get
            {
                return "Sign Up";
            }
        }

        public SignUpViewModel(INavigatableScreen screen = null)
        {
            NavigationScreen = screen ?? Locator.Current.GetService<INavigatableScreen>();

            var canRegester = this.WhenAny(
                                    x => x.RegesterEmail,
                                    x => x.RegesterPassword,
                                    x => x.ConfirmPassword,
                                    (e, p, cp) => !string.IsNullOrEmpty(e.Value)
                                                  && !string.IsNullOrEmpty(p.Value)
                                                  && !string.IsNullOrEmpty(cp.Value)

                                );

            Regester = ReactiveCommand.CreateAsyncTask(canRegester, async _ => 
            {
                var client = new HttpClient(NetCache.UserInitiated)
                {
                    BaseAddress = new Uri(Helper.Address)
                };
                var api = RestService.For<IRestaurantApi>(client);
                IsLoading = true;
                //var result = await api.Regester(this.RegesterEmail, this.RegesterPassword, this.ConfirmPassword);
                await Task.Delay(7000);
                IsLoading = false;
                return new object();
            });

            Regester.Subscribe(r => 
            {
                MessageBus.Current.SendMessage("User regestred!");
                Debug.WriteLine("Complete!");
            });

            Regester.ThrownExceptions.Subscribe(ex =>
            {
                IsLoading = false;
                Debug.WriteLine("Error!");
            });
        }
    }
}
