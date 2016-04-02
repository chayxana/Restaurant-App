using ReactiveUI;
using Restaurant.ViewModels;
using Splat;
using System;
using System.Reactive.Linq;

namespace Restaurant.Models
{
    public class Order : ReactiveObject
    {
        public Guid Id { get; set; }

        public Food Food { get; set; }

        private decimal quntity = .5M;

        public decimal Quantity
        {
            get { return quntity; }
            set
            {
                if (value > 0.5M)
                {
                    value = (int)value;
                }
                this.RaiseAndSetIfChanged(ref quntity, value);
                this.RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        private bool isOrdered;
        public bool IsOrdered
        {
            get { return isOrdered; }
            set { this.RaiseAndSetIfChanged(ref isOrdered, value); }
        }

        public decimal TotalPrice
        {
            get
            {
                return Quantity * Food.Price;
            }
        }

        public ReactiveCommand<object> BeginOrder { get; set; }

        public ReactiveCommand<object> ApplyOrder { get; set; }


        public Order()
        {
            var mainViewModel = Locator.Current.GetService<MainViewModel>();
            BeginOrder = ReactiveCommand.Create();
            ApplyOrder = ReactiveCommand.Create();

            BeginOrder.Subscribe(_ => { IsOrdered = true; });
            ApplyOrder
                .Do(_ => 
                {
                    IsOrdered = false;
                    mainViewModel.BasketViewModel.Orders.Add(this);
                }).Subscribe();
        }
    }
}
