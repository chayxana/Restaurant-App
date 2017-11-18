using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AutoMapper;
using ReactiveUI;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class BasketViewModel : BaseViewModel, IBasketViewModel
    {
        private readonly INavigationService _navigationService;
        private ReactiveList<IOrderViewModel> _orders = new ReactiveList<IOrderViewModel>();
        private string _ordersCount;

        public BasketViewModel(
            IOrdersApi ordersApi,
            IAutoMapperFacade mapperFacade,
            INavigationService navigationService)
        {
            _navigationService = navigationService;
            this.WhenAnyValue(x => x.Orders.Count).Subscribe(x => { OrdersCount = x == 0 ? null : x.ToString(); });

            CompleteOrder = ReactiveCommand.Create(() =>
            {
                var orderItems = mapperFacade.Map<IEnumerable<OrderItemDto>>(Orders);
                ordersApi.Create(new OrderDto()
                {
                    DateTime = DateTime.Now,
                    OrderItems = new List<OrderItemDto>(orderItems)
                });
                Orders.Clear();
                _navigationService.NavigateToRoot();
            });
        }

        public ReactiveList<IOrderViewModel> Orders
        {
            get => _orders;
            set => this.RaiseAndSetIfChanged(ref _orders, value);
        }

        public string OrdersCount
        {
            get => _ordersCount;
            set => this.RaiseAndSetIfChanged(ref _ordersCount, value);
        }

        public void AddOrder(IOrderViewModel order)
        {
            _orders.Add(order.Clone());

            var groupedOrders = _orders
                .GroupBy(x => x.Food.Id)
                .Select(orders => new OrderViewModel(orders.Select(x => x.Food).FirstOrDefault(x => x.Id == orders.Key), orders.Sum(s => s.Quantity)));

            Orders = new ReactiveList<IOrderViewModel>(groupedOrders);
        }

        public ICommand CompleteOrder { get; }

        public override string Title => "Your basket";
    }
}