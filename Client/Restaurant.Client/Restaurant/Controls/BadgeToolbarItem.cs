using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Controls
{
	public class BadgeToolbarItem : ToolbarItem
	{
		public static readonly BindableProperty BadgeTextProperty =
			BindableProperty.Create("BadgeText", typeof(string), typeof(BadgeToolbarItem), default(string));

		public static readonly BindableProperty BadgeColorProperty =
			BindableProperty.Create("BadgeColor", typeof(Color), typeof(BadgeToolbarItem), Color.Red);

		public static readonly BindableProperty BadgeTextColorProperty =
			BindableProperty.Create("BadgeTextColor", typeof(Color), typeof(BadgeToolbarItem), Color.White);

		public string BadgeText
		{
			get => (string)GetValue(BadgeTextProperty);
			set => SetValue(BadgeTextProperty, value);
		}

		public Color BadgeColor
		{
			get => (Color)GetValue(BadgeColorProperty);
			set => SetValue(BadgeColorProperty, value);
		}

		public Color BadgeTextColor
		{
			get => (Color)GetValue(BadgeTextColorProperty);
			set => SetValue(BadgeTextColorProperty, value);
		}

		public BadgeToolbarItem(string name, string icon, Action activated, ToolbarItemOrder order = ToolbarItemOrder.Default, int priority = 0)
			// ReSharper disable once VirtualMemberCallInConstructor
			: base(name, icon, activated, order, priority) => UniqId = GetHashCode();

		// ReSharper disable once VirtualMemberCallInConstructor
		public BadgeToolbarItem() => UniqId = GetHashCode();

		internal int UniqId { get; }

		internal bool HasInitialized { get; set; }
	}
}
