using System.Diagnostics.CodeAnalysis;
using ReactiveUI;
using Restaurant.Abstractions;

namespace Restaurant.Core.ViewModels
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseViewModel : ReactiveObject, IRouteViewModel
    {
        private bool _isLoading;

        private string _title;

        private string _route;

        /// <summary>
        /// Get and sets IsLoading
        /// When current ViewModel is busy (e.g Connection with network, Navigation, Calculating)
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        /// <summary>
        /// Get and set Title and provides page title for UI
        /// </summary>
        public virtual string Title
        {
            get => _title;
            protected set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Route
        {
            get => _route;
            protected set => this.RaiseAndSetIfChanged(ref _route, value);
        }
    }
}