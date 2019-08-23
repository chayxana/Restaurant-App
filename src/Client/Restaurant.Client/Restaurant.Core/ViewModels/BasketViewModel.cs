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
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class BasketViewModel : BaseViewModel, IBasketViewModel
    {
        private readonly ObservableAsPropertyHelper<decimal> _totalPrice;

        public BasketViewModel(
            IBasketItemsService basketItemsService,
            IOrdersApi ordersApi,
            INavigationService navigationService,
            IOrderDtoAdapter orderDtoAdapter)
        {
            Items = basketItemsService.Items;

            _totalPrice = basketItemsService.Items.ToObservableChangeSet()
                .AutoRefresh(x => x.Quantity)
                .ToCollection()
                .Select(x => x.Sum(i => i.TotalPrice))
                .ToProperty(this, x => x.TotalPrice);

            CompleteOrder = ReactiveCommand.CreateFromTask(async () =>
            {
                var orderDto = orderDtoAdapter.GetOrderFromOrderViewModels(Items);
                basketItemsService.Clear();

                await ordersApi.Create(orderDto);
                await navigationService.NavigateToRoot();
            });
        }

        public ICommand CompleteOrder { get; }

        public decimal TotalPrice => _totalPrice.Value;

        public ObservableCollection<IBasketItemViewModel> Items { get; }

        public override string Title => "Your basket";
    }
}