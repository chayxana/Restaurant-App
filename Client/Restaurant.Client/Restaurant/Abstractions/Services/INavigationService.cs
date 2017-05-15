using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Restaurant.Abstractions.Services
{
    public interface INavigationService
    {
        Task NavigateAsync(INavigatableViewModel viewModel);

        Task NavigateModalAsync(INavigatableViewModel viewModel);
    }
}
