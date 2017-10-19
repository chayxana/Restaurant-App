using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using System;
using System.Diagnostics;

namespace Restaurant.Controls
{
    public class AnimatedImage : Image
    {
        public AnimatedImage()
        {
            WidthRequest = 20;
            HeightRequest = 20;
        }

        bool _addedAnimation;

        protected override void OnBindingContextChanged()
        {
            if (_addedAnimation || GestureRecognizers.Count == 0)
                return;

            _addedAnimation = true;

            var tapGesture = GestureRecognizers[0] as TapGestureRecognizer;
            if (tapGesture == null)
                return;


            Observable.FromEventPattern(tapGesture, "Tapped")
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(async x =>
                {
                    try
                    {
                        await this.ScaleTo(1.4, 75);
                        await this.ScaleTo(1.0, 75);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                });

        }
    }
}

