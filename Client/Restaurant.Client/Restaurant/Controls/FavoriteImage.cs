using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    public class FavoriteImage : Image
    {
        bool addedAnimation;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (addedAnimation || GestureRecognizers.Count == 0)
                return;

            addedAnimation = true;

            var tapGesture = GestureRecognizers[0] as TapGestureRecognizer;
            if (tapGesture == null)
                return;

            tapGesture.Tapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() => Grow());
            };

        }

        /// <summary>
        /// Play animation to grow and shrink
        /// </summary>
        public async Task Grow()
        {

            await this.ScaleTo(1.4, 75);
            await this.ScaleTo(1.0, 75);

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    this.ScaleTo(1.4, 75).ContinueWith((t) =>
                        {
                            try
                            {
                                this.ScaleTo(1.0, 75);
                            }
                            catch
                            {
                            }
                        },
                        scheduler: TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch
                {
                }
            });
        }
    }
}

