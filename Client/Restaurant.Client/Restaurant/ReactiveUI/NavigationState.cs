using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Restaurant.ReactiveUI
{
    /// <summary>
    /// RoutingState manages the ViewModel Stack and allows ViewModels to
    /// navigate to other ViewModels.
    /// </summary>
    [DataContract]
    public class NavigationState : ReactiveObject, ISupportsActivation
    {
        static NavigationState()
        {
        }

        public ViewModelActivator Activator
        {
            get;
        }

        public NavigationState()
        {
            _NavigationStack = new ReactiveList<INavigatableViewModel>();
            setupRx();
            Activator = new ViewModelActivator();
        }

        [DataMember]
        ReactiveList<INavigatableViewModel> _NavigationStack;

        /// <summary>
        /// Represents the current navigation stack, the last element in the
        /// collection being the currently visible ViewModel.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveList<INavigatableViewModel> NavigationStack
        {
            get { return _NavigationStack; }
            protected set { _NavigationStack = value; }
        }

        /// <summary>
        /// Navigates back to the previous element in the stack.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<Unit> NavigateBack { get; protected set; }

        /// <summary>
        /// Navigates to the a new element in the stack - the Execute parameter
        /// must be a ViewModel that implements IRoutableViewModel.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<INavigatableViewModel> Navigate { get; protected set; }

        /// <summary>
        /// Navigates to the a new element in the stack - the Execute parameter
        /// must be a ViewModel that implements IRoutableViewModel.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<INavigatableViewModel> NavigateToMainPage { get; protected set; }


        /// <summary>
        /// Navigates to a new element and resets the navigation stack (i.e. the
        /// new ViewModel will now be the only element in the stack) - the
        /// Execute parameter must be a ViewModel that implements
        /// IRoutableViewModel.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<INavigatableViewModel> NavigateAndReset { get; protected set; }

        [IgnoreDataMember]
        public IObservable<INavigatableViewModel> CurrentViewModel { get; protected set; }

        void setupRx()
        {
            var countAsBehavior = Observable.Concat(
                Observable.Defer(() => Observable.Return(_NavigationStack.Count)),
                NavigationStack.CountChanged);

            NavigateBack = ReactiveCommand.CreateAsyncObservable(countAsBehavior.Select(x => x > 1), _ => {
                NavigationStack.RemoveAt(NavigationStack.Count - 1);
                return Observable.Return(Unit.Default);
            });

            Navigate = new ReactiveCommand<INavigatableViewModel>(Observable.Return(true), x => {
                var vm = x as INavigatableViewModel;
                if (vm == null)
                {
                    throw new Exception("Navigate must be called on an INavigatableViewModel");
                }

                NavigationStack.Add(vm);
                return Observable.Return<INavigatableViewModel>(vm);
            });
            NavigateToMainPage = new ReactiveCommand<INavigatableViewModel>(Observable.Return(true), x => 
            {
                var vm = x as INavigatableViewModel;
                if (vm == null)
                {
                    throw new Exception("Navigate must be called on an INavigatableViewModel");
                }
                NavigationStack.Clear();
                return Observable.Return(vm);
            });

            NavigateAndReset = new ReactiveCommand<INavigatableViewModel>(Observable.Return(true), x => {
                NavigationStack.Clear();
                return Navigate.ExecuteAsync(x);
                //return Observable.Return(x as INavigatableViewModel);
            });

            CurrentViewModel = Observable.Concat(
                Observable.Defer(() => Observable.Return(NavigationStack.LastOrDefault())),
                NavigationStack.Changed.Select(_ => NavigationStack.LastOrDefault()));
        }

    }
    public static class RoutingStateMixins
    {
        /// <summary>
        /// Locate the first ViewModel in the stack that matches a certain Type.
        /// </summary>
        /// <returns>The matching ViewModel or null if none exists.</returns>
        public static T FindViewModelInStack<T>(this NavigationState This)
            where T : INavigatableViewModel
        {
            return This.NavigationStack.Reverse().OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Returns the currently visible ViewModel
        /// </summary>
        public static INavigatableViewModel GetCurrentViewModel(this NavigationState This)
        {
            return This.NavigationStack.LastOrDefault();
        }

        /// <summary>
        /// Creates a ReactiveCommand which will, when invoked, navigate to the 
        /// type specified by the type parameter via looking it up in the
        /// Dependency Resolver.
        /// </summary>
        public static IReactiveCommand NavigateCommandFor<T>(this RoutingState This)
            where T : IRoutableViewModel, new()
        {
            var ret = new ReactiveCommand<object>(This.Navigate.CanExecuteObservable, x => Observable.Return(x));
            ret.Select(_ => (IRoutableViewModel)Locator.Current.GetService<T>() ?? new T()).InvokeCommand(This.Navigate);

            return ret;
        }
    }

}
