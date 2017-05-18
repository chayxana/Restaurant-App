using System.Threading.Tasks;
using ReactiveUI;

namespace Restaurant.Abstractions.Facades
{
    public interface INavigationFacade
    {
        Task PushAsync(IViewFor page);

        Task PushModalAsync(IViewFor page);
    }
}
