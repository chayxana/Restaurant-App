using Android.Content;

namespace Restaurant.Droid.Providers
{
    public class ActionBarFacade
    {
        private readonly StatusBarProvider _statusBarProvider;
        private readonly ActionBarProvider _actionBarProvider;
        
        public ActionBarFacade(Context context)
        {   
            _statusBarProvider = new StatusBarProvider(context);
            _actionBarProvider = new ActionBarProvider(context);
        }
        
        internal (int actionBarHeight, int statusBarHeight) GetBarHeights()
        {
            var barHeight = _actionBarProvider.GetActionBarHeight();
            var statusBarHeight = _statusBarProvider.GetStatusBarHeight();
            return (barHeight, statusBarHeight);
        }
    }
}