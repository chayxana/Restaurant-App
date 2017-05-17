using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;

namespace Restaurant.Abstractions.Facades
{
    public interface INavigationFacade
    {
        Task PushAsync(IViewFor page);

        Task PushModalAsync(IViewFor page);
    }
}
