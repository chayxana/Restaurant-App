using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions.Adapters;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class BasketViewModel : BaseViewModel, IBasketViewModel
    {
        private ReactiveList<IOrderViewModel> _orders = new ReactiveList<IOrderViewModel> { ChangeTrackingEnabled = true };
        private readonly ObservableAsPropertyHelper<decimal> _totalPrice;
        private readonly ObservableAsPropertyHelper<string> _ordersCount;

        public BasketViewModel(
            IOrdersApi ordersApi,
            INavigationService navigationService,
            IOrderDtoAdapter orderDtoAdapter)
        {
           _ordersCount = this.WhenAnyValue(x => x.Orders.Count)
                .Select(count => count == 0 ? null : count.ToString())
                .ToProperty(this, x => x.OrdersCount);

            _totalPrice = this.WhenAnyObservable(vm => vm.Orders.ItemChanged)
                .Select(x => Orders.Sum(o => o.TotalPrice))
                .ToProperty(this, x => x.TotalPrice);

            CompleteOrder = ReactiveCommand.Create(() =>
            {
                var orderDto = orderDtoAdapter.GetOrderFromOrderViewModels(Orders);
                ordersApi.Create(orderDto);
                Orders.Clear();
                navigationService.NavigateToRoot();
            });
        }

        public decimal TotalPrice => _totalPrice.Value;

        public string OrdersCount => _ordersCount.Value;

        public ReactiveList<IOrderViewModel> Orders
        {
            get => _orders;
            set => this.RaiseAndSetIfChanged(ref _orders, value);
        }

        public void AddOrder(IOrderViewModel order)
        {
            _orders.Add(order.Clone());

            var groupedOrders = _orders
                .GroupBy(x => x.Food.Id)
                .Select(orders => new OrderViewModel(orders.Select(x => x.Food).FirstOrDefault(x => x.Id == orders.Key), orders.Sum(s => s.Quantity)));

            Orders = new ReactiveList<IOrderViewModel>(groupedOrders) { ChangeTrackingEnabled = true };
        }

        public void RaiseOrdersCount()
        {
            this.RaisePropertyChanged(nameof(OrdersCount));
        }

        public ICommand CompleteOrder { get; }

        public override string Title => "Your basket";
    }
}