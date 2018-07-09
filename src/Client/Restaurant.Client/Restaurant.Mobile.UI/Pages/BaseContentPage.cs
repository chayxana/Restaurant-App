using ReactiveUI;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Pages
{
    public abstract class BaseContentPage<T> : MainBaseContentPage, IViewFor<T> where T : class
    {
        public T ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (T) value;
        }
    }

    public abstract class MainBaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnLoaded();
        }

        protected abstract void OnLoaded();

        protected virtual void UnLoad()
        {
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UnLoad();
        }
    }

    public interface ITransparentActionBarPage
    {
        bool IsTransparentActionBar { get; }
    }
}