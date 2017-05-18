using System;
using System.Net.Http;
using System.Windows.Input;
using JetBrains.Annotations;
using ReactiveUI;
using Refit;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Model;
using Restaurant.Models;

namespace Restaurant.ViewModels
{
    [UsedImplicitly]
    public class SignUpViewModel : ViewModelBase, ISignUpViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _email;

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public ICommand Regester { get; }



        public override string Title => "Sign Up";

        public SignUpViewModel()
        {
            var canRegester = this.WhenAny(x => x.Name, x => x.Email, x => x.Password,
                x => x.ConfirmPassword, (n, e, p, cp) => !string.IsNullOrEmpty(n.Value));
            
            //Creating reactive command for regester
            Regester = ReactiveCommand
                .CreateFromTask(async _ =>
                {
                    IsBusy = true;
                    var client = new HttpClient // NetCache.UserInitiated)
                    {
                        BaseAddress = new Uri(Helper.Address)
                    };
                    var api = RestService.For<IRestaurantApi>(client);
                    var result = await api.Regester(Name, Email, Password, ConfirmPassword);
                    return result;
                }, canRegester);

            //When regstir command executing sets true for IsLoading property
            //Regester
            //    .IsExecuting
            //    .Subscribe(x => IsLoading = true);

            //Raises when completes regester command
            //Regester
            //    .Subscribe(r => 
            //    {
            //        MessageBus.Current.SendMessage("User regestred!");
            //        Debug.WriteLine("Complete!");
            //        IsLoading = false;
            //    });

            ////Raises when regester throws any exception
            //Regester
            //    .ThrownExceptions
            //    .Subscribe(ex =>
            //    {
            //        Debug.WriteLine("Error!");
            //        IsLoading = false;
            //    });
        }
    }
}
