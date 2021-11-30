using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;

namespace Restaurant.Core.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly IOrdersApi _ordersApi;

	    internal OrdersViewModel()
	    {   
	    }

        public OrdersViewModel(IOrdersApi ordersApi)
        {
            _ordersApi = ordersApi;
        }
        private ObservableCollection<OrderDto> _orders;
        public ObservableCollection<OrderDto> Orders
        {
            get => _orders;
            set => this.RaiseAndSetIfChanged(ref _orders, value);
        }

        public async Task LoadOrders()
        {
            IsLoading = true;
            var items = await _ordersApi.GetAll();
            Orders = new ObservableCollection<OrderDto>(items);
            IsLoading = false;
        }
    }
}
