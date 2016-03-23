using ReactiveUI;
using Restaurant.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Food : ReactiveObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

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
                return Quantity * Price;
            }
        }

        public ReactiveCommand<object> BeginOrder { get; set; }

        public ReactiveCommand<object> ApplyOrder { get; set; }

        public ReactiveCommand<object> CancelOrder { get; set; }

        public MainViewModel MainViewModel { get; set; }

        public Food()
        {
            MainViewModel = Locator.Current.GetService<MainViewModel>();

            BeginOrder = ReactiveCommand.Create();
            BeginOrder.Subscribe(_ => { IsOrdered = true; });

            ApplyOrder = ReactiveCommand.Create();
            ApplyOrder.Subscribe(_ =>
            {
                IsOrdered = false;
                MainViewModel.BasketViewModel.Orders.Add(new Order { Food = this, Id = Guid.NewGuid() });
            });
        }

    }
}
