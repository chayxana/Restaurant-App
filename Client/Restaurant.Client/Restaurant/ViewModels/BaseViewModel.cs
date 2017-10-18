using ReactiveUI;
using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
	public class BaseViewModel : ReactiveObject, INavigatableViewModel
	{
		private bool _isLoading;

		public bool IsLoading
		{
			get => _isLoading;
			set => this.RaiseAndSetIfChanged(ref _isLoading, value);
		}

		private string _title;
		public virtual string Title
		{
			get => _title;
			protected set => this.RaiseAndSetIfChanged(ref _title, value);
		}
	}
}
