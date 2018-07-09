using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

namespace Restaurant.Droid
{
    public class BadgeDrawable : Drawable
    {
        private const string BadgeValueOverflow = "*";

        private Paint _badgeBackground;
        private Paint _badgeText;
        private Rect _textRect = new Rect();

        private string _badgeValue = "";
        private bool _shouldDraw=true;
        Context _context;

        public override int Opacity => (int)Format.Unknown;

        public BadgeDrawable(Context context, Color backgroundColor, Color textColor)
        {
            
            _context = context;
            float textSize = context.Resources.GetDimension(Resource.Dimension.textsize_badge_count);
            _badgeBackground = new Paint();
            _badgeBackground.Color = backgroundColor;
            _badgeBackground.AntiAlias = true;
            _badgeBackground.SetStyle(Paint.Style.Fill);

            _badgeText = new Paint();
            _badgeText.Color = textColor;
            _badgeText.SetTypeface(Typeface.Default);
            _badgeText.TextSize =textSize;
            _badgeText.AntiAlias = true;
            _badgeText.TextAlign =Paint.Align.Center;
        }
       

        public override void Draw(Canvas canvas)
        {
            if (!_shouldDraw)
            {
                return;
            }
            Rect bounds = Bounds;
            float width = bounds.Right - bounds.Left;
            float height = bounds.Bottom - bounds.Top;
            float oneDp = 1 * _context.Resources.DisplayMetrics.Density;

            // Position the badge in the top-right quadrant of the icon.
            float radius = ((Java.Lang.Math.Max(width, height) / 2)) / 2;
            float centerX = (width - radius - 1) + oneDp * 2;
            float centerY = radius - 2 * oneDp;
            canvas.DrawCircle(centerX, centerY, (int)(radius + oneDp * 5), _badgeBackground);

            // Draw badge count message inside the circle.
            _badgeText.GetTextBounds(_badgeValue, 0, _badgeValue.Length, _textRect);
            float textHeight = _textRect.Bottom - _textRect.Top;
            float textY = centerY + (textHeight / 2f);
            canvas.DrawText(_badgeValue.Length > 2 ? BadgeValueOverflow : _badgeValue,
                    centerX, textY, _badgeText);
        }

        // Sets the text to display. Badge displays a '*' if more than 2 characters
        private void SetBadgeText(string text)
        {
            _badgeValue = text;

            // Only draw a badge if the value isn't a zero
            _shouldDraw = !text.Equals("0");
            InvalidateSelf();
        }

        public override void SetAlpha(int alpha)
        {
            // do nothing
        }

        public override void SetColorFilter(ColorFilter cf)
        {
            // do nothing
        }
        public static void SetBadgeCount(Context context, IMenuItem item, int count, Color backgroundColor, Color textColor)
        {
            SetBadgeText(context, item, $"{count}", backgroundColor,textColor);
        }

        // Max of 2 characters
        public static void SetBadgeText(Context context, IMenuItem item, string text, Color backgroundColor, Color textColor)
        {

          
            if (item.Icon == null)
            {
                return;
            }

            BadgeDrawable badge=null;
            Drawable icon = item.Icon;

            
            if (item.Icon is LayerDrawable)
            {

                LayerDrawable lDrawable = item.Icon as LayerDrawable;

                if (string.IsNullOrEmpty(text) || text == "0")
                {
                    icon = lDrawable.GetDrawable(0);
                    lDrawable.Dispose();
                }
                else
                {
                    for (var i = 0; i < lDrawable.NumberOfLayers; i++)
                    {
                        if (lDrawable.GetDrawable(i) is BadgeDrawable)
                        {
                            badge = lDrawable.GetDrawable(i) as BadgeDrawable;
                            break;
                        }

                    }

                    if (badge == null)
                    {
                        badge = new BadgeDrawable(context, backgroundColor, textColor);
                        icon = new LayerDrawable(new Drawable[] { item.Icon, badge });
                    }
                }

            }else
            {
                badge = new BadgeDrawable(context, backgroundColor, textColor);
                icon = new LayerDrawable(new Drawable[] { item.Icon, badge });
            }
         
            badge?.SetBadgeText(text);

            item.SetIcon(icon);
            icon.Dispose();
        }

    }
}