using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace Restaurant.Droid.Renderers
{
    public class MenuClickListener : Object, IMenuItemOnMenuItemClickListener
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
    }
}