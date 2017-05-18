using System.Threading.Tasks;
using ReactiveUI;

namespace Restaurant.Abstractions.Services
{
    public interface INavigationService
    {
        IViewFor CurrentPage { get; }

        Task NavigateAsync(INavigatableViewModel viewModel);

        Task NavigateModalAsync(INavigatableViewModel viewModel);
    }
}
