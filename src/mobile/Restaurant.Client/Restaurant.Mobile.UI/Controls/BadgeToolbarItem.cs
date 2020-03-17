using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public class BadgeToolbarItem : ToolbarItem
    {
        private const string DefaultBadgeText = default(string);
        private static readonly Color DefaultBadgeColor = Color.Red;
        private static readonly Color DefaultBadgeTextColor = Color.White;
        private static readonly Color DefaultBadgePressedColor = Color.Red;

        public static readonly BindableProperty BadgeTextProperty =
            BindableProperty.Create(nameof(BadgeText), typeof(string), typeof(BadgeToolbarItem), DefaultBadgeText);

        public static readonly BindableProperty BadgeColorProperty =
            BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(BadgeToolbarItem), DefaultBadgeColor);

        public static readonly BindableProperty BadgeTextColorProperty =
            BindableProperty.Create(nameof(BadgeTextColor), typeof(Color), typeof(BadgeToolbarItem), Color.White);

        public static readonly BindableProperty BadgePressedColorProperty =
            BindableProperty.Create(nameof(BadgePressedColor), typeof(Color), typeof(BadgeToolbarItem), Color.Red);

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

        public int UniqId => GetHashCode();
    }
}