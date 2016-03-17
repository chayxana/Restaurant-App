using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.ReactiveUI
{
    public class NavigationHost : NavigationPage, IActivatable
    {
        public static readonly BindableProperty RouterProperty = BindableProperty.Create<NavigationHost, NavigationState>(
           x => x.Router, null, BindingMode.OneWay);

        public NavigationState Router
        {
            get { return (NavigationState)GetValue(RouterProperty); }
            set { SetValue(RouterProperty, value); }
        }

        public NavigationHost()
        {
            SubscribeNavigationHosts();
        }

        public NavigationHost(ContentPage root) : base(root)
        {
            SubscribeNavigationHosts();
        }

        private void SubscribeNavigationHosts()
        {
            bool currentlyPopping = false;

            this.WhenAnyObservable(x => x.Router.NavigationStack.Changed)
                .Where(_ => Router.NavigationStack.IsEmpty)
                .SelectMany(_ => pageForViewModel(Router.GetCurrentViewModel()))
                .SelectMany(async x =>
                {
                    currentlyPopping = true;
                    await this.PopToRootAsync();
                    currentlyPopping = false;
                    return x;
                })
                .Subscribe();
            this.WhenAnyObservable(x => x.Router.NavigateAndChangeRoot)
                .SelectMany(x => pageForViewModel(x)).Subscribe((p) =>
                {
                    Navigation.PushModalAsync(p);
                });
            this.WhenAnyObservable(x => x.Router.Navigate)
                .SelectMany(_ => pageForViewModel(Router.GetCurrentViewModel()))
                .SelectMany(x => this.PushAsync(x).ToObservable())
                .Subscribe();

            this.WhenAnyObservable(x => x.Router.NavigateBack)
                .SelectMany(async x =>
                {
                    currentlyPopping = true;
                    await this.PopAsync();
                    currentlyPopping = false;

                    return x;
                })
                .Do(_ => ((IViewFor)this.CurrentPage).ViewModel = Router.GetCurrentViewModel())
                .Subscribe();

            var poppingEvent = Observable.FromEventPattern<NavigationEventArgs>(x => this.Popped += x, x => this.Popped -= x);

            // NB: Catch when the user hit back as opposed to the application
            // requesting Back via NavigateBack
            poppingEvent
                .Where(_ => !currentlyPopping && Router != null)
                .Subscribe(_ =>
                {
                    try
                    {
                        if(Router.NavigationStack.Count > 1)
                        {
                            Router.NavigationStack.RemoveAt(Router.NavigationStack.Count - 1);
                            ((IViewFor)this.CurrentPage).ViewModel = Router.GetCurrentViewModel();
                        }
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                });
           
                        
            Router = Locator.Current.GetService<INavigatableScreen>().Navigation;
            if (Router == null) throw new Exception("You *must* register an IScreen class representing your App's main Screen");
        }
        
        IObservable<Page> pageForViewModel(INavigatableViewModel vm)
        {
            if (vm == null) return Observable.Empty<Page>();

            var ret = ViewLocator.Current.ResolveView(vm);
            if (ret == null)
            {
                var msg = String.Format(
                    "Couldn't find a View for ViewModel. You probably need to register an IViewFor<{0}>",
                    vm.GetType().Name);

                return Observable.Throw<Page>(new Exception(msg));
            }

            ret.ViewModel = vm;

            var pg = (Page)ret;
            pg.Title = vm.Title;
            return Observable.Return(pg);
        }
    }
}
