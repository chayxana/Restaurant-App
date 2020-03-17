using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Restaurant.iOS.Controls
{
    public class UIBadgeLabel : UILabel
    {
        private UIColor badgeColor;

        private UIColor borderColor;

        private double borderWidth;


        private CGSize insets;

        private UIColor shadowColorBadge;

        private CGSize shadowOffsetBadge;

        private double shadowOpacityBadge;

        private double shadowRadiusBadge;

        public UIBadgeLabel()
        {
            Initialize();
            Setup();
        }

        public UIBadgeLabel(NSCoder coder) : base(coder)
        {
            Initialize();
            Setup();
        }

        public UIBadgeLabel(CGRect rect) : base(rect)
        {
            Initialize();
            Setup();
        }

        public UIColor BadgeColor
        {
            get => badgeColor;
            set
            {
                badgeColor = value;
                InvalidateIntrinsicContentSize();
            }
        }

        public double BorderWidth
        {
            get => borderWidth;
            set
            {
                borderWidth = value;
                InvalidateIntrinsicContentSize();
            }
        }

        public UIColor BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                InvalidateIntrinsicContentSize();
            }
        }

        public CGSize Insets
        {
            get => insets;
            set
            {
                insets = value;
                InvalidateIntrinsicContentSize();
            }
        }

        public double ShadowOpacityBadge
        {
            get => shadowOpacityBadge;
            set
            {
                shadowOpacityBadge = value;
                Layer.ShadowOpacity = (float) value;
                SetNeedsDisplay();
            }
        }

        public double ShadowRadiusBadge
        {
            get => shadowRadiusBadge;
            set
            {
                shadowRadiusBadge = value;
                Layer.ShadowRadius = new nfloat(value);
                SetNeedsDisplay();
            }
        }

        public UIColor ShadowColorBadge
        {
            get => shadowColorBadge;
            set
            {
                shadowColorBadge = value;
                Layer.ShadowColor = value.CGColor;
                SetNeedsDisplay();
            }
        }

        public CGSize ShadowOffsetBadge
        {
            get => shadowOffsetBadge;
            set
            {
                shadowOffsetBadge = value;
                Layer.ShadowOffset = value;
                SetNeedsDisplay();
            }
        }

        public override string Text
        {
            get => base.Text;

            set
            {
                base.Text = value;
                if (Text.Length >= 2)
                {
                    InvalidateIntrinsicContentSize();
                    Font = Font.WithSize(10);
                    //CGSize stringSize = Text.StringSize(UIFont.BoldSystemFontOfSize(12));
                    //this.Frame = new CGRect(Frame.X - Frame.X, Frame.Y, Frame.Width * stringSize.Width, Frame.Height);
                }
            }
        }

        public override CGSize IntrinsicContentSize => base.IntrinsicContentSize;

        private void Initialize()
        {
            BadgeColor = UIColor.Red;
            BorderWidth = 0;
            BorderColor = UIColor.White;
            Insets = new CGSize(5, 2);
            ShadowOpacityBadge = .5;
            ShadowColorBadge = UIColor.Black;
            ShadowOffsetBadge = new CGSize(0, 0);
        }

        public override CGRect TextRectForBounds(CGRect bounds, nint numberOfLines)
        {
            var rect = base.TextRectForBounds(bounds, numberOfLines);
            var insetsWithBorder = ActualInsetsWithBorder();

            var rectWithDefaultInsets = CGRect.Inflate(rect, -insetsWithBorder.Width, -insetsWithBorder.Height);
            // If width is less than height
            // Adjust the width insets to make it look round
            if (rectWithDefaultInsets.Width < rectWithDefaultInsets.Height)
                insetsWithBorder.Width = (rectWithDefaultInsets.Height - rect.Width) / 2;
            var result = CGRect.Inflate(rect, -insetsWithBorder.Width, -insetsWithBorder.Height);
            return result;
        }

        public override void DrawText(CGRect rect)
        {
            Layer.CornerRadius = rect.Height / 2;
            var insetsWithBorder = ActualInsetsWithBorder();
            var insets = new UIEdgeInsets(
                insetsWithBorder.Height,
                insetsWithBorder.Width,
                insetsWithBorder.Height,
                insetsWithBorder.Width);

            var rectWithoutInsets = insets.InsetRect(rect);
            base.DrawText(rectWithoutInsets);
        }

        public override void Draw(CGRect rect)
        {
            var rectInset = CGRect.Inflate(rect, new nfloat(BorderWidth / 2), new nfloat(BorderWidth / 2));

            var path = UIBezierPath.FromRoundedRect(rectInset, rect.Height / 2);

            badgeColor.SetFill();

            path.Fill();

            if (BorderWidth > 0)
            {
                borderColor.SetStroke();
                path.LineWidth = new nfloat(BorderWidth);
                path.Stroke();
            }
            base.Draw(rect);
        }

        private void Setup()
        {
            TextAlignment = UITextAlignment.Center;
            ClipsToBounds = false;
        }

        private CGSize ActualInsetsWithBorder()
        {
            return new CGSize(Insets.Width + borderWidth, Insets.Height + borderWidth);
        }

        public override void PrepareForInterfaceBuilder()
        {
            base.PrepareForInterfaceBuilder();
            Setup();
            SetNeedsDisplay();
        }
    }
}