using ReactiveUI;
using Restaurant.Common.DataTransferObjects;

namespace Restaurant.ViewModels
{
    public class OrderViewModel : ReactiveObject
    {
        public FoodDto Food { get;  }

        public OrderViewModel(FoodDto food)
        {
            Food = food;
        }

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

        public decimal TotalPrice => Quantity * Food.Price;
    }
}
