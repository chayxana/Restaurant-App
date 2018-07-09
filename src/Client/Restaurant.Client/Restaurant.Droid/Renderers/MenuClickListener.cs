using System;
using Android.Views;
using Com.Mikepenz.Actionitembadge.Library;
using Object = Java.Lang.Object;

namespace Restaurant.Droid.Renderers
{
    public class MenuClickListener : Object, IMenuItemOnMenuItemClickListener, ActionItemBadge.IActionItemBadgeListener
    {
        private readonly Action _callback;

        public MenuClickListener(Action callback)
        {
            _callback = callback;
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            _callback();
            return true;
        }

        public bool OnOptionsItemSelected(IMenuItem p0)
        {
            _callback();
            return true;
        }
    }
}