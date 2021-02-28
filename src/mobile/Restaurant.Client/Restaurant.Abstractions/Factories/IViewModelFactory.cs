using System;

namespace Restaurant.Abstractions.Factories
{
    public interface IViewModelFactory
    {
        IRouteViewModel GetViewModel(Type viewModelType);

        IRouteViewModel GetMainViewModel(Type viewModelType, string platform);
    }
}