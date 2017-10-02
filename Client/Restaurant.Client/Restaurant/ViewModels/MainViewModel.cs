using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.Repositories;
using Restaurant.Abstractions.Services;
using Restaurant.Model;
using Restaurant.Models;
using Restaurant.Pages;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.ViewModels
{
    public class MainViewModel : ReactiveObject, IMainViewModel, IDetailedViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly INavigationService _navigationService;

        public MainViewModel(
            IUserRepository userRepository,
            IMasterViewModel masterViewModel,
            INavigationService navigationService)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;
            MasterViewModel = masterViewModel;

            OnLoad();
        }
		
        [UsedImplicitly]
        public IMasterViewModel MasterViewModel { get; }

        public string Title => "Main";

        private async void OnLoad()
        {
            //User = await _userRepository.GetUserInfo();
            MasterViewModel
                .SelectedMasterItem
                .Where(x => x != null)
                .Subscribe(async masterItem => await _navigationService.NavigateAsync(masterItem.NavigationType));
        }
    }

    public class MasterViewModel : ReactiveObject, IMasterViewModel
    {
        public MasterViewModel()
        {
            var items = new ObservableCollection<MasterItem>
            {
                new MasterItem
                {
                    Title = "Foods",
                    IconSource = "ic_restaurant_menu_black.png",
                    NavigationType = typeof(FoodsPage)
                },
                new MasterItem
                {
                    Title = "Orders",
                    IconSource = "ic_basket.png",
                    NavigationType = typeof(FoodsPage)
                },
                new MasterItem
                {
                    Title = "Chat",
                    IconSource = "ic_wechat.png",
                    NavigationType = typeof(FoodsPage)
                },
                new MasterItem
                {
                    Title = "Settings",
                    IconSource = "ic_settings.png",
                    NavigationType = typeof(FoodsPage)
                },
                new MasterItem
                {
                    Title = "About",
                    IconSource = "ic_alert_circle_outline.png",
                    NavigationType = typeof(FoodsPage)
                }
            };
            Items = items;
            SelectedMasterItem = this.WhenAnyValue(x => x.SelectedItem);
        }

        private ObservableCollection<MasterItem> _items;
        public ObservableCollection<MasterItem> Items
        {
            get => _items;
            private set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        private MasterItem _selecteItem;
        public MasterItem SelectedItem
        {
            get => _selecteItem;
            set => this.RaiseAndSetIfChanged(ref _selecteItem, value);
        }

        public IObservable<MasterItem> SelectedMasterItem { get; }
    }

    public interface IDetailedViewModel
    {
        IMasterViewModel MasterViewModel { get; }
    }

    public interface IMasterViewModel
    {
        ObservableCollection<MasterItem> Items { get; }
        IObservable<MasterItem> SelectedMasterItem { get; }
    }
}
