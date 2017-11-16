using System;

namespace Restaurant.Abstractions.Factories
{
    public interface IViewModelFactory
    {
        INavigatableViewModel GetViewModel(Type viewModelType);

        INavigatableViewModel GetMainViewModel(Type viewModelType, string platform);
    }
}