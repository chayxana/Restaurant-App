using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions;

namespace Restaurant.Core.ViewModels
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseViewModel : ReactiveObject, INavigatableViewModel
    {
        private bool _isLoading;

        private string _title;

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
    }
}