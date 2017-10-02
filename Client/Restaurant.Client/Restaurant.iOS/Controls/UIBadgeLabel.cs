using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Restaurant.iOS.Controls
{
    public class UIBadgeLabel : UILabel
    {
        private UIColor badgeColor;
        public UIColor BadgeColor
        {
            get => badgeColor;
	        set { badgeColor = value; InvalidateIntrinsicContentSize(); }
        }

        private double borderWidth;
        public double BorderWidth
        {
            get => borderWidth;
	        set { borderWidth = value; InvalidateIntrinsicContentSize(); }
        }

        private UIColor borderColor;

        public UIColor BorderColor
        {
            get => borderColor;
	        set { borderColor = value; InvalidateIntrinsicContentSize(); }
        }


        private CGSize insets;

        public CGSize Insets
        {
            get => insets;
	        set { insets = value; InvalidateIntrinsicContentSize(); }
        }

        private double shadowOpacityBadge;

        public double ShadowOpacityBadge
        {
            get => shadowOpacityBadge;
	        set { shadowOpacityBadge = value; Layer.ShadowOpacity = (float)value; SetNeedsDisplay(); }
        }

        private double shadowRadiusBadge;

        public double ShadowRadiusBadge
        {
            get => shadowRadiusBadge;
	        set { shadowRadiusBadge = value; Layer.ShadowRadius = new nfloat(value); SetNeedsDisplay(); }
        }

        private UIColor shadowColorBadge;

        public UIColor ShadowColorBadge
        {
            get => shadowColorBadge;
	        set { shadowColorBadge = value; Layer.ShadowColor = value.CGColor; SetNeedsDisplay(); }
        }

        private CGSize shadowOffsetBadge;

        public CGSize ShadowOffsetBadge
        {
            get => shadowOffsetBadge;
	        set { shadowOffsetBadge = value; Layer.ShadowOffset = value; SetNeedsDisplay(); }
        }

        private void Initialize()
        {
            BadgeColor = UIColor.Red;
            BorderWidth = 0;
            BorderColor = UIColor.White;
            Insets = new CGSize(width: 5, height: 2);
            ShadowOpacityBadge = .5;
            ShadowColorBadge = UIColor.Black;
            ShadowOffsetBadge = new CGSize(0, 0);
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

        public override CGRect TextRectForBounds(CGRect bounds, nint numberOfLines)
        {
            var rect = base.TextRectForBounds(bounds, numberOfLines);
            var insetsWithBorder = ActualInsetsWithBorder();

            var rectWithDefaultInsets = CGRect.Inflate(rect, -insetsWithBorder.Width, -insetsWithBorder.Height);
            // If width is less than height
            // Adjust the width insets to make it look round
            if (rectWithDefaultInsets.Width < rectWithDefaultInsets.Height)
            {
                insetsWithBorder.Width = (rectWithDefaultInsets.Height - rect.Width) / 2;
            }
            var result = CGRect.Inflate(rect, -insetsWithBorder.Width, -insetsWithBorder.Height);
            return result;
        }

        public override void DrawText(CGRect rect)
        {
            Layer.CornerRadius = rect.Height / 2;
            var insetsWithBorder = ActualInsetsWithBorder();
            var insets = new UIEdgeInsets(
                top: insetsWithBorder.Height,
                left: insetsWithBorder.Width,
                bottom: insetsWithBorder.Height,
                right: insetsWithBorder.Width);

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
        public override CGSize IntrinsicContentSize => base.IntrinsicContentSize;
    }
}
