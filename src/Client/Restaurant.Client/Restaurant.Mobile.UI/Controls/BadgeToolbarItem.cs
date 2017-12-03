using System;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public class BadgeToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty BadgeTextProperty =
            BindableProperty.Create("UserPicture", typeof(string), typeof(BadgeToolbarItem), default(string));

        public static readonly BindableProperty BadgeColorProperty =
            BindableProperty.Create("BadgeColor", typeof(Color), typeof(BadgeToolbarItem), Color.Red);

        public static readonly BindableProperty BadgeTextColorProperty =
            BindableProperty.Create("BadgeTextColor", typeof(Color), typeof(BadgeToolbarItem), Color.White);

        public static readonly BindableProperty BadgePressedColorProperty =
            BindableProperty.Create("BadgePressedColor", typeof(Color), typeof(BadgeToolbarItem), Color.Red);

        public BadgeToolbarItem(string name, string icon, Action activated,
                ToolbarItemOrder order = ToolbarItemOrder.Default,
                int priority = 0)
            // ReSharper disable once VirtualMemberCallInConstructor
            : base(name, icon, activated, order, priority)
        {
            UniqId = GetHashCode();
        }

        // ReSharper disable once VirtualMemberCallInConstructor
        public BadgeToolbarItem()
        {
            UniqId = GetHashCode();
        }

        public string BadgeText
        {
            get => (string) GetValue(BadgeTextProperty);
            set => SetValue(BadgeTextProperty, value);
        }

        public Color BadgeColor
        {
            get => (Color) GetValue(BadgeColorProperty);
            set => SetValue(BadgeColorProperty, value);
        }

        public Color BadgePressedColor
        {
            get => (Color) GetValue(BadgePressedColorProperty);
            set => SetValue(BadgePressedColorProperty, value);
        }

        public Color BadgeTextColor
        {
            get => (Color) GetValue(BadgeTextColorProperty);
            set => SetValue(BadgeTextColorProperty, value);
        }

        public int UniqId { get; }

        public bool HasInitialized { get; set; }
    }
}