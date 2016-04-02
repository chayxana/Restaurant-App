using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Restaurant.iOS.Controls
{

    public class MKNumberBadgeView : UIView
    {
        public int Value { get; set; }

        public bool Shadow { get; set; }

        public CGSize ShadowOffset { get; set; }

        public bool Shine { get; set; }

        public UIFont Font { get; set; }

        public UIColor BGColor { get; set; }

        public UIColor StrokeColor { get; set; }

        public UIColor TextColor { get; set; }

        public UITextAlignment Aligment { get; set; }

        public CGSize BadgeSize { get; set; }

        public int Pad { get; set; }

        public bool HideWhenZero { get; set; }

        public MKNumberBadgeView(NSCoder coder) : base(coder)
        {
        }
        public MKNumberBadgeView(CGRect frame) : base(frame)
        {
        }

        public MKNumberBadgeView()
        {
            Opaque = false;
            Pad = 2;
            Font = UIFont.BoldSystemFontOfSize(16);
            Shadow = true;
            ShadowOffset = new CGSize(0, 3);
            Shine = true;
            Aligment = UITextAlignment.Center;
            BGColor = UIColor.Red;
            StrokeColor = UIColor.White;
            TextColor = UIColor.White;
            HideWhenZero = false;
            UserInteractionEnabled = false;
            BackgroundColor = UIColor.Clear;
        }


        public void DrawRect(CGRect rect)
        {
            CGRect viewBounds = Bounds;
            CGContext currentContext = UIGraphics.GetCurrentContext();
            string numberString = Value.ToString();
            CGSize numberSize = numberString.StringSize(Font);
            CGRect badgeRect = new CGRect { Size = numberSize };
            badgeRect.X = 0;
            badgeRect.Y = 0;
            badgeRect.Width = new nfloat(Math.Ceiling(badgeRect.Size.Width));
            badgeRect.Height = new nfloat(Math.Ceiling(badgeRect.Size.Height));
            double lineWidth = 2.0;
            currentContext.SaveState();
            currentContext.SetLineWidth(new nfloat(lineWidth));
            currentContext.SetStrokeColor(StrokeColor.CGColor);
            currentContext.SetFillColor(BGColor.CGColor);
            badgeRect.Width += new nfloat(Math.Ceiling(lineWidth / 2));
            badgeRect.Height += new nfloat(Math.Ceiling(lineWidth / 2));
        }
    }


    public class CustomBadge : UIView
    {
        private String badgeText;
        private UIColor badgeTextColor;
        private bool badgeFrame;
        private UIColor badgeFrameColor;
        private UIColor badgeInsetColor;
        private double badgeCornerRoundness = 0.4;
        private double badgeScaleFactor;
        private bool badgeShining;
        private double initialSize = 25;

        public CustomBadge(NSCoder coder) : base(coder)
        {

        }

        public CustomBadge(CGRect rect) : base(rect)
        {

        }

        public CustomBadge(string badgeText
            , UIColor badgeTextColor, bool badgeFrame, UIColor badgeFrameColor
            , UIColor badgeInsetColor, double badgeScaleFactor, bool badgeShining) : base(frame: new CGRect(0, 0, 25, 25))
        {
            this.badgeText = badgeText;
            this.badgeTextColor = badgeTextColor;
            this.badgeFrame = badgeFrame;
            this.badgeFrameColor = badgeFrameColor;
            this.badgeInsetColor = badgeInsetColor;
            this.badgeScaleFactor = badgeScaleFactor;
            this.badgeShining = badgeShining;
            TranslatesAutoresizingMaskIntoConstraints = false;
            ContentScaleFactor = UIScreen.MainScreen.Scale;
            BackgroundColor = UIColor.Clear;
            InvalidateIntrinsicContentSize();
        }

        private double Padding(CGRect rect)
        {
            return rect.GetMaxY() * 0.10;
        }

        public override CGSize IntrinsicContentSize
        {
            get
            {
                CGSize size;
                CGSize stringSize = badgeText.StringSize(UIFont.BoldSystemFontOfSize(12));

                if (badgeText.Length >= 2)
                {
                    double flexSpace = (double)badgeText.Length;
                    double rectWidth = initialSize + (stringSize.Width + flexSpace);
                    double rectHeight = initialSize;
                    size = new CGSize(rectWidth * badgeScaleFactor, rectHeight * badgeScaleFactor);
                }
                else
                {
                    size = new CGSize(initialSize * badgeScaleFactor, initialSize * badgeScaleFactor);
                }
                return size;
            }
        }

        public override UIEdgeInsets AlignmentRectInsets
        {
            get
            {
                var puffer = new nfloat(Padding(Bounds));
                return new UIEdgeInsets(puffer, puffer, puffer, puffer);
            }
        }
        private void drawArc(CGContext context, CGRect rect)
        {
            var radius = rect.GetMaxY() * badgeCornerRoundness;
            var puffer = new nfloat(Padding(rect));
            var maxX = rect.GetMaxX() - puffer;
            var maxY = rect.GetMaxY() - puffer;
            var minX = rect.GetMinX() + puffer;
            var minY = rect.GetMinY() + puffer;
            double pi = Math.PI;
            context.AddArc(new nfloat(maxX - radius), new nfloat(minY + radius), new nfloat(radius), new nfloat(pi + (pi / 2)), 0, false);
            context.AddArc(new nfloat(maxX - radius), new nfloat(minY - radius), new nfloat(radius), 0, new nfloat(pi / 2), false);
        }
    }


   
}
