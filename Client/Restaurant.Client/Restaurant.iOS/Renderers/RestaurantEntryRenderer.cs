using System;
using System.ComponentModel;
using Foundation;
using Restaurant.iOS.Renderers;
using Restaurant.Mobile.UI.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RestaurantEntry), typeof(RestaurantEntryRenderer))]

namespace Restaurant.iOS.Renderers
{
    public class RestaurantEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            var view = (RestaurantEntry) Element;

            if (view != null)
            {
                SetBorder(view);
                SetFont(view);
                SetFontFamily(view);
                SetMaxLength(view);
                SetTextAlignment(view);
                SetPlaceholderTextColor(view);
            }
        }

        private void SetFont(RestaurantEntry view)
        {
            UIFont uiFont;
            if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
                Control.Font = uiFont;
            else if (view.Font == Font.Default)
                Control.Font = UIFont.SystemFontOfSize(17f);
        }

        private void SetFontFamily(RestaurantEntry view)
        {
            UIFont uiFont;

            if (!string.IsNullOrWhiteSpace(view.FontFamily) && (uiFont = view.Font.ToUIFont()) != null)
            {
                var ui = UIFont.FromName(view.FontFamily, (nfloat) (view.Font != null ? view.Font.FontSize : 17f));
                Control.Font = uiFont;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (RestaurantEntry) Element;

            if (e.PropertyName == RestaurantEntry.FontProperty.PropertyName)
                SetFont(view);

            if (e.PropertyName == Entry.FontFamilyProperty.PropertyName)
                SetFontFamily(view);

            if (e.PropertyName == RestaurantEntry.HasBorderProperty.PropertyName)
                SetBorder(view);

            if (e.PropertyName == RestaurantEntry.MaxLengthProperty.PropertyName)
                SetMaxLength(view);

            if (e.PropertyName == RestaurantEntry.XAlignProperty.PropertyName)
                SetTextAlignment(view);

            if (e.PropertyName == RestaurantEntry.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);
        }

        private void SetMaxLength(RestaurantEntry view)
        {
            Control.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= view.MaxLength;
            };
        }

        private void SetBorder(RestaurantEntry view)
        {
            Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        private void SetTextAlignment(RestaurantEntry view)
        {
            switch (view.XAlign)
            {
                case TextAlignment.Center:
                    Control.TextAlignment = UITextAlignment.Center;
                    break;
                case TextAlignment.End:
                    Control.TextAlignment = UITextAlignment.Right;
                    break;
                case TextAlignment.Start:
                    Control.TextAlignment = UITextAlignment.Left;
                    break;
            }
        }

        /// <summary>
        ///     Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(RestaurantEntry view)
        {
            /*
UIColor *color = [UIColor lightTextColor];
YOURTEXTFIELD.attributedPlaceholder = [[NSAttributedString alloc] initWithString:@"PlaceHolder Text" attributes:@{NSForegroundColorAttributeName: color}];
            */
            if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderTextColor != Color.Default)
            {
                var placeholderString = new NSAttributedString(view.Placeholder,
                    new UIStringAttributes {ForegroundColor = view.PlaceholderTextColor.ToUIColor()});
                Control.AttributedPlaceholder = placeholderString;
            }
        }
    }
}