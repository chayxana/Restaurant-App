using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class FloatingActionButton : Button
    {
        public static BindableProperty ButtonColorProperty = BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(FloatingActionButton), Color.Accent);
        public Color ButtonColor
        {
            get
            {
                return (Color)GetValue(ButtonColorProperty);
            }
            set
            {
                SetValue(ButtonColorProperty, value);
            }
        }
        public FloatingActionButton()
        {
        }
    }
}
