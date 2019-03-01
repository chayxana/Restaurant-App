using System;
using System.Reactive.Linq;
using ReactiveUI;
using Restaurant.Core.ViewModels.Android;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Pages.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MenuPage : ContentPage, IViewFor<MasterViewModel>
    {

		public MenuPage()
        {
            InitializeComponent();
        }

	   
		object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MasterViewModel) value;
        }

        public MasterViewModel ViewModel { get; set; }

        protected override async void OnAppearing()
        {
            Observable.FromEventPattern<EventHandler<NavigationItemSelectedEventArgs>, NavigationItemSelectedEventArgs>(
                    e => NavigationView.NavigationItemSelected += e, e => NavigationView.NavigationItemSelected -= e)
                .Select(x => x.EventArgs.SelectedViewModel)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(item => { ViewModel.SelectedNavigationItem = item; });

	        await ViewModel.LoadUserInfo();
        }
    }
}