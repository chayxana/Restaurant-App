using Fusillade;
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
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class RegesterViewModel : ReactiveObject, IRoutableViewModel
    {
        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set { this.RaiseAndSetIfChanged(ref isLoading, value); }
        }

        public IScreen HostScreen { get; }

        public string UrlPathSegment
        {
            get
            {
                return "Regester";
            }
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

        public RegesterViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

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
                await Task.Delay(4000);
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
