using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.Subscribers;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class BasketViewModel : BaseViewModel, IBasketViewModel
    {
        private ObservableCollection<IBasketItemViewModel> _items = new ObservableCollection<IBasketItemViewModel>();
        private decimal _totalPrice;
        private string _ordersCount;

        public BasketViewModel(
            IOrdersApi ordersApi,
            IBasketItemViewModelSubscriber basketItemViewModelSubscriber,
            INavigationService navigationService,
            IOrderDtoAdapter orderDtoAdapter)
        {
            basketItemViewModelSubscriber.Handler
                .Subscribe(item =>
                {  
                    AddBasketItem(item);
                    OrdersCount = _items.Count.ToString();
                    TotalPrice = _items.Sum(i => i.TotalPrice);
                });
            
            CompleteOrder = ReactiveCommand.CreateFromTask(async () =>
            {
                var orderDto = orderDtoAdapter.GetOrderFromOrderViewModels(Items);
                await ordersApi.Create(orderDto);
                Items.Clear();
                await navigationService.NavigateToRoot();
            });
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set => this.RaiseAndSetIfChanged(ref _totalPrice, value);
        }

        public string OrdersCount
        {
            get => _ordersCount;
            set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
        }

        public ObservableCollection<IBasketItemViewModel> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public void AddBasketItem(IBasketItemViewModel basketItem)
        {
            var addedItem = _items.FirstOrDefault(x => x.Food.Id == basketItem.Food.Id);
            if (addedItem != null)
            {
                addedItem.Quantity += basketItem.Quantity;
            }
            else
            {
                _items.Add(basketItem);
            }
        }

        public ICommand CompleteOrder { get; }

        public override string Title => "Your basket";
    }
}