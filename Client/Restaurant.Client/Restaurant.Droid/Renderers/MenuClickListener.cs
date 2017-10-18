using System;
using Android.Views;

namespace Restaurant.Droid.Renderers
{
	public class MenuClickListener : Java.Lang.Object, IMenuItemOnMenuItemClickListener
	{
		readonly Action _callback;

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