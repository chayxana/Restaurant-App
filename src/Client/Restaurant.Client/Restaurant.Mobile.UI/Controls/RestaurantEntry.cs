using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public class RestaurantEntry : Entry
    {
        /// <summary>
        ///     The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(RestaurantEntry), Color.Default);


        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(RestaurantEntry), true);

        public static readonly BindableProperty FontProperty =
            BindableProperty.Create("Font", typeof(Font), typeof(RestaurantEntry), new Font());

        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(RestaurantEntry), TextAlignment.Start);

        /// <summary>
        ///     Sets color for placeholder text
        /// </summary>
        public Color PlaceholderTextColor
        {
            get => (Color) GetValue(PlaceholderTextColorProperty);
            set => SetValue(PlaceholderTextColorProperty, value);
        }

        public bool HasBorder
        {
            get => (bool) GetValue(HasBorderProperty);
            set => SetValue(HasBorderProperty, value);
        }

        public Font Font
        {
            get => (Font) GetValue(FontProperty);
            set => SetValue(FontProperty, value);
        }

        public TextAlignment XAlign
        {
            get => (TextAlignment) GetValue(XAlignProperty);
            set => SetValue(XAlignProperty, value);
        }
    }
}