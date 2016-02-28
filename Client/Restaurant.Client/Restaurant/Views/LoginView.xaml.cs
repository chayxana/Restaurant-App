using ReactiveUI;
using ReactiveUI.XamForms;
using Restaurant.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Views
{
    public partial class LoginView : ContentPage, IViewFor<LoginViewModel>
    {
        public LoginView()
        {
            InitializeComponent();
            //BackgroundColor = Color.FromHex("#BBDEFB");
             
            ViewModel = Locator.Current.GetService<LoginViewModel>();
            this.Bind(ViewModel, vm => vm.Email, v => v.Email.Text);
            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);
            this.BindCommand(ViewModel, vm => vm.OpenRegester, v => v.OpenRegester);
            //(ViewModel.HostScreen as AppBotstrapper).RouterHost.PopToRoot.Subscribe((viewModel) => 
            //{
            //    var mainView = new MainView(viewModel as MainViewModel);
            //    Locator.CurrentMutable.Register(() => mainView, typeof(IViewFor<MainViewModel>));

            //    Navigation.PushAsync(mainView);
            //});
        }

        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<LoginView, LoginViewModel>(x => x.ViewModel, default(LoginViewModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }

            set { ViewModel = (LoginViewModel)value; }
        }
    }
}
