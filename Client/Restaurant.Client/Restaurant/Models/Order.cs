using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Legacy;
using Restaurant.DataTransferObjects;
using Restaurant.ViewModels;
using Splat;
using ReactiveCommand = ReactiveUI.Legacy.ReactiveCommand;

namespace Restaurant.Models
{
    public class Order : ReactiveObject
    {
        public Guid Id { get; set; }

        public FoodDto Food { get; set; }

        private decimal _quntity = .5M;

        public decimal Quantity
        {
            get => _quntity;
            set
            {
                if (value > 0.5M)
                {
                    value = (int)value;
                }
                this.RaiseAndSetIfChanged(ref _quntity, value);
                this.RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        private bool _isOrdered;
        public bool IsOrdered
        {
            get => _isOrdered;
            set => this.RaiseAndSetIfChanged(ref _isOrdered, value);
        }

        public decimal TotalPrice => Quantity * Food.Price;

        public ReactiveCommand<object> BeginOrder { get; set; }

        public ReactiveCommand<object> ApplyOrder { get; set; }


        public Order()
        {
            var mainViewModel = Locator.Current.GetService<MainViewModel>();
            BeginOrder = ReactiveCommand.Create();
            ApplyOrder = ReactiveCommand.Create();

            BeginOrder.Subscribe(_ => { IsOrdered = true; });
            ApplyOrder.Do(_ => 
            {
                IsOrdered = false;
                mainViewModel.OrderViewModel.Orders.Add(this);
            }).Subscribe();
        }
    }
}
