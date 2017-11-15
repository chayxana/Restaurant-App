using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions.ViewModels;

namespace Restaurant.Core.ViewModels
{
    [UsedImplicitly]
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        public virtual string Title => "Main";
    }
}