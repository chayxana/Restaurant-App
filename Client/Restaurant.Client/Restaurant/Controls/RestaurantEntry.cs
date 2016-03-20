using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class RestaurantEntry : Entry
    {
     
        /// <summary>
        /// The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(RestaurantEntry), Color.Default);

        /// <summary>
        /// Sets color for placeholder text
        /// </summary>
        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }


        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(RestaurantEntry), true);

        public bool HasBorder
        {
            get
            {
                return (bool)GetValue(HasBorderProperty);
            }
            set
            {
                SetValue(HasBorderProperty, value);
            }
        }

        public static readonly BindableProperty FontProperty =
            BindableProperty.Create("Font", typeof(Font), typeof(RestaurantEntry), new Font());

        public Font Font
        {
            get
            {
                return (Font)GetValue(FontProperty);
            }
            set
            {
                SetValue(FontProperty, value);
            }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(RestaurantEntry), null);

        public string FontFamily
        {
            get
            {
                return (string)this.GetValue(FontFamilyProperty);
            }
            set
            {
                this.SetValue(FontFamilyProperty, value);
            }
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength", typeof(int), typeof(RestaurantEntry), int.MaxValue);

        public int MaxLength
        {
            get
            {
                return (int)this.GetValue(MaxLengthProperty);
            }
            set
            {
                this.SetValue(MaxLengthProperty, value);
            }
        }

        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(RestaurantEntry), TextAlignment.Start);

        public TextAlignment XAlign
        {
            get
            {
                return (TextAlignment)GetValue(XAlignProperty);
            }
            set
            {
                SetValue(XAlignProperty, value);
            }
        }
    }
}
