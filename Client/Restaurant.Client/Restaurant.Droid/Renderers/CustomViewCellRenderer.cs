using Android.Content;
using Android.Views;
using Restaurant.Droid.Renderers;
using Restaurant.Mobile.UI.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]

namespace Restaurant.Droid.Renderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);
            cell.SetBackgroundResource(Resource.Drawable.ViewCellBackground);
            return cell;
        }
    }
}