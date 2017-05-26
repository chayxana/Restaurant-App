using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DataTransferObjects;

namespace Restaurant.ViewModels
{
    public class FoodDetailViewModel : ViewModelBase
    {
        public FoodDto SelectedFood { get; }

        public FoodDetailViewModel(FoodDto selectedFood)
        {
            SelectedFood = selectedFood;
        }

        public override string Title => SelectedFood.Name;
    }
}
