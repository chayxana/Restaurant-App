using ReactiveUI;
using Restaurant.Models;
using Restaurant.ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class BasketViewModel : ReactiveObject, INavigatableViewModel
    {
        private ReactiveList<Order> orders;
        public ReactiveList<Order> Orders
        {
            get { return orders; }
            set { this.RaiseAndSetIfChanged(ref orders, value); }
        }

        private int ordersCount;
        public int OrdersCount
        {
            get { return ordersCount; }
            set { this.RaiseAndSetIfChanged(ref ordersCount, value); }
        }

        public INavigatableScreen NavigationScreen { get; }

        public string Title { get { return "Your basket"; } }

        public BasketViewModel()
        {
            Orders = new ReactiveList<Order>();
            this.WhenAnyValue(x => x.Orders.Count)
                .Subscribe(x =>
                {
                    OrdersCount = x;
                    var a = Orders.GroupBy(o => o.Food);
                });
        }
    }
}
