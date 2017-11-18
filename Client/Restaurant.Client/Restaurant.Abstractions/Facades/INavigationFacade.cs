using System.Threading.Tasks;
using ReactiveUI;

namespace Restaurant.Abstractions.Facades
{
    public interface INavigationFacade
    {
        Task PushAsync(IViewFor page);

        Task PushModalAsync(IViewFor page);

        Task PopModalAsync(bool animated);

        Task NavigateToMainPage(IViewFor page);

        Task NavigateToMainPageContent(IViewFor page);

        Task NavigateToRoot();
    }
}