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
using Restaurant.Controls;
using Restaurant.Droid.Renderers;
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
            return base.GetCellCore(item, convertView, parent, context);
            //cell.SetBackgroundResource(Resource.Drawable.ViewCellBackground);
            //return cell;
        }
    }
}