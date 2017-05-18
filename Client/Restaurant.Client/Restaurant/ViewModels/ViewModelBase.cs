using JetBrains.Annotations;
using ReactiveUI;

namespace Restaurant.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        private bool _isBusy;
        private string _status;

        /// <summary>
        /// Gets and sets IsBusy
        /// When some command executing we will provide this property some value 
        /// for control UI
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        /// <summary>
        /// Gets view model title
        /// </summary>
        [UsedImplicitly]
        public virtual string Title { get; }
    }
}