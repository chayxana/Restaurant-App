using ReactiveUI;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.ViewModels;

namespace Restaurant.Core.ViewModels
{
    public class OrderViewModel : ReactiveObject, IOrderViewModel
    {
        private decimal _quntity = 1;

        public OrderViewModel(FoodDto food)
        {
            Food = food;
        }

        public OrderViewModel(FoodDto food, decimal quntity)
        {
            Food = food;
            Quantity = quntity;
        }

        public FoodDto Food { get; }

        public decimal Quantity
        {
            get => _quntity;
            set
            {
                this.RaiseAndSetIfChanged(ref _quntity, value);
                this.RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        public decimal TotalPrice => Quantity * Food.Price;

        public IOrderViewModel Clone()
        {
            return (IOrderViewModel) MemberwiseClone();
        }
    }
}