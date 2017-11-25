using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace Restaurant.Abstractions.Services
{
    public interface INavigationService
    {
        IViewFor CurrentView { get; }

        Task NavigateAsync(INavigatableViewModel viewModel);

        Task NavigateAsync(Type viewModelType);

        Task NavigateModalAsync(INavigatableViewModel viewModel);

        Task NavigateModalAsync(Type viewModelType);

        Task CloseModalAsync(bool animated);

        Task NavigateToMainPage(INavigatableViewModel viewModel);

        Task NavigateToMainPage(Type viewModelType);

        Task NavigateToMainPageContent(INavigatableViewModel viewModel);

        Task NavigateToRoot();
    }
}