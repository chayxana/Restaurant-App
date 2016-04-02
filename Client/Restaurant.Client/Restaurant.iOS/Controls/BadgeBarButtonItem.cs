using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Restaurant.iOS.Controls
{
    public class BadgeBarButtonItem : UIBarButtonItem
    {
        private UILabel badge;
        public UILabel Badge
        {
            get { return badge; }
            set { badge = value; }
        }

        private string badgeValue;
        public string BadgeValue
        {
            get { return badgeValue; }
            set
            {
                badgeValue = value;
                if ((badgeValue == "" || badgeValue == "0") && ShouldHideBadgeAtZero)
                    RemoveBadge();
                else if (Badge == null)
                {
                    Badge = new UILabel(new CGRect(BadgeOriginX, BadgeOriginY, 20, 20));
                    Badge.TextColor = BadgeTextColor;
                    Badge.BackgroundColor = BadgeBGColor;
                    Badge.Font = BadgeFont;
                    Badge.TextAlignment = UITextAlignment.Center;
                    CustomView.AddSubview(Badge);
                    UpdateBadgeValueAnimated(false);
                }
                else
                    UpdateBadgeValueAnimated(true);
            }
        }

        private UIColor badgeBGColor;
        public UIColor BadgeBGColor
        {
            get { return badgeBGColor; }
            set
            {
                badgeBGColor = value;
                RefreshBadge();
            }
        }

        private UIColor badgeTextColor;
        public UIColor BadgeTextColor
        {
            get { return badgeTextColor; }
            set
            {
                badgeTextColor = value;
                RefreshBadge();
            }
        }

        private UIFont badgeFont;
        public UIFont BadgeFont
        {
            get { return badgeFont; }
            set
            {
                badgeFont = value;
                RefreshBadge();
            }
        }
        private nfloat badgePadding;
        public nfloat BadgePadding
        {
            get { return badgePadding; }
            set
            {
                badgePadding = value;
                UpdateBadgeFrame();
            }
        }

        private nfloat badgeMinSize;
        public nfloat BadgeMinSize
        {
            get { return badgeMinSize; }
            set
            {
                badgeMinSize = value;
                UpdateBadgeFrame();
            }
        }

        private nfloat badgeOriginX;
        public nfloat BadgeOriginX
        {
            get { return badgeOriginX; }
            set
            {
                badgeOriginX = value;
                UpdateBadgeFrame();
            }
        }

        private nfloat badgeOriginY;
        public nfloat BadgeOriginY
        {
            get { return badgeOriginY; }
            set
            {
                badgeOriginY = value;
                UpdateBadgeFrame();
            }
        }

        public bool ShouldHideBadgeAtZero { get; set; }

        public bool ShouldAnimate { get; set; }

        public BadgeBarButtonItem(UIButton customButton)
        {
            InitializeValues();
            this.CustomView = customButton;
        }

        public BadgeBarButtonItem()
        {
            InitializeValues();
            CustomView.ClipsToBounds = false;
        }

        private void InitializeValues()
        {
            BadgeBGColor = UIColor.Red;
            BadgeTextColor = UIColor.White;
            BadgeFont = UIFont.SystemFontOfSize(12);
            BadgePadding = 6;
            BadgeMinSize = 8;
            BadgeOriginX = 7;
            BadgeOriginY = -9;
            ShouldHideBadgeAtZero = true;
            ShouldAnimate = true;
            BadgeValue = "";
        }

        /// <summary>
        /// Change new attributes
        /// </summary>
        public void RefreshBadge()
        {
            if (Badge != null)
            {
                Badge.TextColor = BadgeTextColor;
                Badge.BackgroundColor = BadgeBGColor;
                Badge.Font = BadgeFont;
            }
        }

        /// <summary>
        /// When the value changes the badge could need to get bigger
        /// Calculate expected size to fit new value
        /// Use an intermediate label to get expected size thanks to sizeToFit
        /// We don't call sizeToFit on the true label to avoid bad display
        /// </summary>
        private void UpdateBadgeFrame()
        {
            if (Badge == null) return;

            UILabel frameLabel = DuplicateLabel(Badge);
            frameLabel.SizeToFit();
            CGSize expectedLabelSize = frameLabel.Frame.Size;
            nfloat minHeight = expectedLabelSize.Height;
            minHeight = minHeight < BadgeMinSize ? BadgeMinSize : expectedLabelSize.Height;
            nfloat minWidth = expectedLabelSize.Width;
            nfloat padding = BadgePadding;
            minWidth = (minWidth < minHeight) ? minHeight : expectedLabelSize.Width;
            Badge.Frame = new CGRect(BadgeOriginX, BadgeOriginY, minWidth + padding, minHeight + padding);
            Badge.Layer.CornerRadius = (minHeight + padding) / 2;
            Badge.Layer.MasksToBounds = true;
        }

        public void UpdateBadgeValueAnimated(bool animated)
        {
            // Bounce animation on badge if value changed and if animation authorized
            if (animated && ShouldAnimate && Badge.Text != BadgeValue)
            {
                CABasicAnimation animation = new CABasicAnimation()
                {
                    KeyPath = "transform.scale",
                    From = NSNumber.FromDouble(1.5),
                    To = NSNumber.FromDouble(1),
                    Duration = .2,
                    TimingFunction = CAMediaTimingFunction.FromControlPoints((float)0.4, (float).3, 1, 1)
                };
                Badge.Layer.AddAnimation(animation, "bounceAnimation");
            }
            // Set the new value
            Badge.Text = BadgeValue;
            // Animate the size modification if needed
            //NSTimeInterval duration = animated ? 0.2 : 0;
            //[UIView animateWithDuration:duration animations:^{
            UpdateBadgeFrame();
            //}]; // this animation breaks the rounded corners in iOS 9
        }

        private UILabel DuplicateLabel(UILabel labelToCopy)
        {
            UILabel duplicateLabel = new UILabel(labelToCopy.Frame);
            duplicateLabel.Text = labelToCopy.Text;
            duplicateLabel.Font = labelToCopy.Font;
            return duplicateLabel;
        }

        public void RemoveBadge()
        {
            if (Badge != null)
            {
                UIView.Animate(0.2,
                () =>
                {
                    Badge.Transform = new CGAffineTransform(0, 0, 0, 0, 0, 0);
                },
                () =>
                {
                    Badge.RemoveFromSuperview();
                    Badge = null;
                });
            }

        }
    }
}
