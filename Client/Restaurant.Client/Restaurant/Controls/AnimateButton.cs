using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class AnimateButton : Button
    {
        public AnimateButton() : base()
        {
            const int _animationTime = 100;
            Clicked += async (sender, e) =>
            {
                var btn = (AnimateButton)sender;
                await btn.ScaleTo(1.2, _animationTime);
                await btn.ScaleTo(1, _animationTime);
                await Task.Delay(400);
            };
        }
    }
}
