using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Restaurant.iOS.Controls
{
    public class BadgeBarButtonItem : UIBarButtonItem
    {
        private UIColor badgeBGColor;

        private UIFont badgeFont;

        private nfloat badgeMinSize;

        private nfloat badgeOriginX;

        private nfloat badgeOriginY;
        private nfloat badgePadding;

        private UIColor badgeTextColor;

        private string badgeValue;

        public BadgeBarButtonItem(UIButton customButton)
        {
            InitializeValues();
            // ReSharper disable once VirtualMemberCallInConstructor
            CustomView = customButton;
        }

        public BadgeBarButtonItem()
        {
            InitializeValues();
            // ReSharper disable once VirtualMemberCallInConstructor
            CustomView.ClipsToBounds = false;
        }

        public UILabel Badge { get; set; }

        public string BadgeValue
        {
            get => badgeValue;
            set
            {
                badgeValue = value;
                if ((badgeValue == "" || badgeValue == "0") && ShouldHideBadgeAtZero)
                {
                    RemoveBadge();
                }
                else if (Badge == null)
                {
                    Badge = new UILabel(new CGRect(BadgeOriginX, BadgeOriginY, 20, 20))
                    {
                        TextColor = BadgeTextColor,
                        BackgroundColor = BadgeBGColor,
                        Font = BadgeFont,
                        TextAlignment = UITextAlignment.Center
                    };
                    CustomView.AddSubview(Badge);
                    UpdateBadgeValueAnimated(false);
                }
                else
                {
                    UpdateBadgeValueAnimated(true);
                }
            }
        }

        public UIColor BadgeBGColor
        {
            get => badgeBGColor;
            set
            {
                badgeBGColor = value;
                RefreshBadge();
            }
        }

        public UIColor BadgeTextColor
        {
            get => badgeTextColor;
            set
            {
                badgeTextColor = value;
                RefreshBadge();
            }
        }

        public UIFont BadgeFont
        {
            get => badgeFont;
            set
            {
                badgeFont = value;
                RefreshBadge();
            }
        }

        public nfloat BadgePadding
        {
            get => badgePadding;
            set
            {
                badgePadding = value;
                UpdateBadgeFrame();
            }
        }

        public nfloat BadgeMinSize
        {
            get => badgeMinSize;
            set
            {
                badgeMinSize = value;
                UpdateBadgeFrame();
            }
        }

        public nfloat BadgeOriginX
        {
            get => badgeOriginX;
            set
            {
                badgeOriginX = value;
                UpdateBadgeFrame();
            }
        }

        public nfloat BadgeOriginY
        {
            get => badgeOriginY;
            set
            {
                badgeOriginY = value;
                UpdateBadgeFrame();
            }
        }

        public bool ShouldHideBadgeAtZero { get; set; }

        public bool ShouldAnimate { get; set; }

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
        ///     Change new attributes
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
        ///     When the value changes the badge could need to get bigger
        ///     Calculate expected size to fit new value
        ///     Use an intermediate label to get expected size thanks to sizeToFit
        ///     We don't call sizeToFit on the true label to avoid bad display
        /// </summary>
        private void UpdateBadgeFrame()
        {
            if (Badge == null) return;

            var frameLabel = DuplicateLabel(Badge);
            frameLabel.SizeToFit();
            var expectedLabelSize = frameLabel.Frame.Size;
            var minHeight = expectedLabelSize.Height;
            minHeight = minHeight < BadgeMinSize ? BadgeMinSize : expectedLabelSize.Height;
            var minWidth = expectedLabelSize.Width;
            var padding = BadgePadding;
            minWidth = minWidth < minHeight ? minHeight : expectedLabelSize.Width;
            Badge.Frame = new CGRect(BadgeOriginX, BadgeOriginY, minWidth + padding, minHeight + padding);
            Badge.Layer.CornerRadius = (minHeight + padding) / 2;
            Badge.Layer.MasksToBounds = true;
        }

        public void UpdateBadgeValueAnimated(bool animated)
        {
            // Bounce animation on badge if value changed and if animation authorized
            if (animated && ShouldAnimate && Badge.Text != BadgeValue)
            {
                var animation = new CABasicAnimation
                {
                    KeyPath = "transform.scale",
                    From = NSNumber.FromDouble(1.5),
                    To = NSNumber.FromDouble(1),
                    Duration = .2,
                    TimingFunction = CAMediaTimingFunction.FromControlPoints((float) 0.4, (float) .3, 1, 1)
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
            var duplicateLabel = new UILabel(labelToCopy.Frame);
            duplicateLabel.Text = labelToCopy.Text;
            duplicateLabel.Font = labelToCopy.Font;
            return duplicateLabel;
        }

        public void RemoveBadge()
        {
            if (Badge != null)
                UIView.Animate(0.2,
                    () => { Badge.Transform = new CGAffineTransform(0, 0, 0, 0, 0, 0); },
                    () =>
                    {
                        Badge.RemoveFromSuperview();
                        Badge = null;
                    });
        }
    }
}