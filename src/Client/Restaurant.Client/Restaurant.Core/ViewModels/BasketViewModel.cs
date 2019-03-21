using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
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
        private readonly ObservableAsPropertyHelper<decimal> _totalPrice;
        private readonly ObservableAsPropertyHelper<string> _ordersCount;

        public BasketViewModel(
            IOrdersApi ordersApi,
            IBasketItemViewModelSubscriber basketItemViewModelSubscriber,
            INavigationService navigationService,
            IOrderDtoAdapter orderDtoAdapter)
        {
            
            basketItemViewModelSubscriber.Handler
                .Subscribe(item =>
                {
                    var addedItem = Items.FirstOrDefault(x => x.Food.Id == item.Food.Id);
                    if (addedItem != null)
                        addedItem.Quantity += item.Quantity;
                    else
                        Items.Add(item);
                });

            _totalPrice = Items.ToObservableChangeSet()
                .AutoRefresh(x => x.Quantity)
                .ToCollection()
                .Select(x => x.Sum(i => i.TotalPrice))
                .ToProperty(this, x => x.TotalPrice);

            _ordersCount = Items.ToObservableChangeSet()
                .ToCollection()
                .Select(x => x.Count.ToString())
                .ToProperty(this, x => x.OrdersCount);
                    
            CompleteOrder = ReactiveCommand.CreateFromTask(async () =>
            {
                var orderDto = orderDtoAdapter.GetOrderFromOrderViewModels(Items);
                Items.Clear();
                
                await ordersApi.Create(orderDto);
                await navigationService.NavigateToRoot();
            });
        }

        public decimal TotalPrice => _totalPrice.Value;

        public string OrdersCount => _ordersCount.Value;

        public ObservableCollection<IBasketItemViewModel> Items { get; } = new ObservableCollection<IBasketItemViewModel>();

        public ICommand CompleteOrder { get; }

        public override string Title => "Your basket";
    }
}