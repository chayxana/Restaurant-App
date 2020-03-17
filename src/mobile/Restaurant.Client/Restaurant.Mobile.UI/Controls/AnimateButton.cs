using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Controls
{
    public class AnimateButton : FlatButton
    {
        public AnimateButton()
        {
            const int animationTime = 75;
            Clicked += async (sender, e) =>
            {
                try
                {
                    var btn = (AnimateButton) sender;
                    await btn.ScaleTo(1.2, animationTime);
                    await btn.ScaleTo(1, animationTime);
                    //await Task.Delay(400);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            };
        }
    }
}