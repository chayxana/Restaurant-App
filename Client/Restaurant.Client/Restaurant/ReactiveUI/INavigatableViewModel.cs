using ReactiveUI;

namespace Restaurant.ReactiveUI
{
    public interface INavigatableViewModel : IReactiveObject
    {
        /// <summary>
        /// The Screen that this ViewModels wiil be shown.
        /// </summary>
        INavigatableScreen NavigationScreen { get; }

        /// <summary>
        /// On the screen when its navigates being show on the Action Bar
        /// </summary>
        string Title { get; }
    }
}
