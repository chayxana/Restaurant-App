using System.Linq;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Effects
{
    public static class NumberOfLinesEffect
    {
        public static readonly BindableProperty ApplyNumberOfLinesProperty =
            BindableProperty.CreateAttached("ApplyNumberOfLines", typeof(bool), typeof(NumberOfLinesEffect), false,
                propertyChanged: OnApplyNumberOfLinesChanged);

        public static readonly BindableProperty NumberOfLinesProperty =
            BindableProperty.CreateAttached("NumberOfLines", typeof(int), typeof(NumberOfLinesEffect), 1);

        public static bool GetApplyNumberOfLines(BindableObject view)
        {
            return (bool) view.GetValue(ApplyNumberOfLinesProperty);
        }

        public static void SetApplyNumberOfLines(BindableObject view, bool value)
        {
            view.SetValue(ApplyNumberOfLinesProperty, value);
        }

        private static void OnApplyNumberOfLinesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
                return;

            var hasShadow = (bool) newValue;
            if (hasShadow)
            {
                view.Effects.Add(new MaxLinesEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is MaxLinesEffect);
                if (toRemove != null)
                    view.Effects.Remove(toRemove);
            }
        }

        public static int GetNumberOfLines(BindableObject view)
        {
            return (int) view.GetValue(NumberOfLinesProperty);
        }

        public static void SetNumberOfLines(BindableObject view, int value)
        {
            view.SetValue(NumberOfLinesProperty, value);
        }
    }

    internal class MaxLinesEffect : RoutingEffect
    {
        public MaxLinesEffect() : base("Restaurant.MaxLinesEffect")
        {
        }
    }
}