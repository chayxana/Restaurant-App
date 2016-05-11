using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(Restaurant.Droid.Renderers.AppMasterDetailRenderer))]
namespace Restaurant.Droid.Renderers
{
    public class AppMasterDetailRenderer : Xamarin.Forms.Platform.Android.AppCompat.MasterDetailPageRenderer
    {
        protected override void OnElementChanged(VisualElement oldElement, VisualElement newElement)
        {
            //base.OnElementChanged(oldElement, newElement);

            //if (newElement == null)
            //{
            //    return;
            //}

            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            //{
            //    var drawer = GetChildAt(1);
            //    var detail = GetChildAt(0);

            //    var padding = detail.GetType().GetRuntimeProperty("TopPadding");
            //    int value = (int)padding.GetValue(detail);

            //    padding.SetValue(drawer, value);
            //}
        }
    }
}