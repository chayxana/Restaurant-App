using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Restaurant.iOS.Helpers;
using Restaurant.iOS.Renderers;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace Restaurant.iOS.Renderers
{
	public class CustomSearchBarRenderer : Xamarin.Forms.Platform.iOS.SearchBarRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
		{
			base.OnElementChanged(e);

			Control.SearchBarStyle = UISearchBarStyle.Minimal;

			var textField = Control.FindDescendantView<UITextField>();
			textField.BackgroundColor = Xamarin.Forms.Color.FromHex("#ECF0F1").ToUIColor();
		}
	}
}