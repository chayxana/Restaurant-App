using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.DataTransferObjects;
using Restaurant.Models;
using Splat;

namespace Restaurant.ViewModels
{
    public class FoodsViewModel : ReactiveObject, INavigatableViewModel
    {
        public FoodsViewModel()
        {
        }

        public ObservableCollection<FoodDto> Foods { get; private set; }

        public string Title => "Foods";

    }
}
