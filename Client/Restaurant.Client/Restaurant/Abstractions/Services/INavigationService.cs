using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;

namespace Restaurant.Abstractions.Services
{
    public interface INavigationService
    {
        IViewFor CurrentPage { get; }

        Task NavigateAsync(INavigatableViewModel viewModel);

        Task NavigateModalAsync(INavigatableViewModel viewModel);
    }
}
