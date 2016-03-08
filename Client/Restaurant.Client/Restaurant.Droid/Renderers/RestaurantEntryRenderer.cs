using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Restaurant.Controls;
using Android.Text;

[assembly: ExportRenderer(typeof(RestaurantEntry), typeof(Restaurant.Droid.Renderers.RestaurantEntryRenderer))]
namespace Restaurant.Droid.Renderers
{
    public class RestaurantEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (RestaurantEntry)Element;

            SetFont(view);
            SetPlaceholderTextColor(view);
            SetTextAlignment(view);
            SetMaxLength(view);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (RestaurantEntry)Element;

            if (e.PropertyName == RestaurantEntry.FontProperty.PropertyName)
                SetFont(view);

            if (e.PropertyName == RestaurantEntry.XAlignProperty.PropertyName)
                SetTextAlignment(view);

            if (e.PropertyName == RestaurantEntry.PlaceholderTextColorProperty.PropertyName)
            {
                SetPlaceholderTextColor(view);
            }
        }

        void SetTextAlignment(RestaurantEntry view)
        {
            switch (view.XAlign)
            {
                case Xamarin.Forms.TextAlignment.Center:
                    Control.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    Control.Gravity = GravityFlags.End;
                    break;
                case Xamarin.Forms.TextAlignment.Start:
                    Control.Gravity = GravityFlags.Start;
                    break;
            }
        }

        void SetFont(RestaurantEntry view)
        {
            if (view.Font != Font.Default)
            {
                Control.TextSize = view.Font.ToScaledPixel();
            }
        }

        void SetMaxLength(RestaurantEntry view)
        {
            Control.SetFilters(new IInputFilter[] {
                new InputFilterLengthFilter(view.MaxLength)
            });
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(RestaurantEntry view)
        {
            if (view.PlaceholderTextColor != Color.Default)
            {
                Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}