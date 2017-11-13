using System;

namespace Restaurant.Abstractions.Factories
{
    public interface IViewModelFactory
    {
        INavigatableViewModel GetViewModel(Type viewModelType);
    }
}
