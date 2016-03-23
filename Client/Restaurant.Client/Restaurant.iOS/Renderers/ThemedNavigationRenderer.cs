using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using System.Windows.Input;
using CoreGraphics;
using Restaurant.ViewModels;
using Restaurant.iOS.Controls;
using ReactiveUI;
using System.Reactive.Linq;

[assembly: ExportRenderer(typeof(Restaurant.ThemedNavigationPage), typeof(Restaurant.iOS.Renderers.ThemedNavigationRenderer))]
namespace Restaurant.iOS.Renderers
{
    /// <summary>
    /// This custom NavigationRender is only necessary on iOS so we can change the navigation bar color *prior* to navigating instead of after
    /// Forms currently doesn't give us a lifecycle event before the navigation takes place
    /// This isn't an issue on Android
    /// </summary>
    public class ThemedNavigationRenderer : NavigationRenderer
    {
        public override void PushViewController(UIViewController viewController, bool animated)
        {
            base.PushViewController(viewController, animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            ChangeTheme(page);
            return base.OnPushAsync(page, animated);
        }

        public override UIViewController PopViewController(bool animated)
        {
            var obj = Element.GetType().InvokeMember("StackCopy", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, Element, null);
            if (obj != null)
            {
                var pages = obj as Stack<Page>;
                if (pages != null && pages.Count >= 2)
                {
                    var copy = new Page[pages.Count];
                    pages.CopyTo(copy, 0);

                    var prev = copy[1];
                    ChangeTheme(prev);
                }
            }
            return base.PopViewController(animated);
        }
        Dictionary<UIButton, ICommand> buttonCommands = new Dictionary<UIButton, ICommand>();
        ToolbarItem toolBarItem;
        public override void ViewWillAppear(bool animated)
        {
            if (toolBarItem != null)
            {
                var tools = new List<UIBarButtonItem>();
                MainViewModel viewModel = (toolBarItem.BindingContext as FoodsViewModel)?.MainViewModel as MainViewModel;
                var badge = new UIBadgeLabel
                {
                    Text = "0",
                    TextColor = UIColor.White,
                    Hidden = true,
                    Frame = new CGRect(20, -6, 20, 20),
                    ShadowOpacityBadge = 0,
                    ShadowColorBadge = UIColor.Red,                                       
                };
                badge.Font = badge.Font.WithSize(12);
                viewModel.BasketViewModel
                    .WhenAnyValue(x => x.OrdersCount)
                    .Subscribe(x => 
                    {
                        if (x > 0)
                            badge.Hidden = false;
                        badge.Text = x.ToString();
                    });
                UIButton button = UIButton.FromType(UIButtonType.Custom);
                button.Frame = new CGRect(0, 0, 30, 30);
                button.SetImage(UIImage.FromFile("ic_shopping_cart_white_2x.png"), UIControlState.Normal);
                button.AddSubview(badge);
                button.TouchUpInside += (s, e) =>
                {
                    var tool = s as UIButton;
                    var command = buttonCommands[tool];
                    command.Execute(null);
                };
                buttonCommands.Add(button, toolBarItem.Command);
                NavigationBar.Items[0].RightBarButtonItem = new UIBarButtonItem(button);
            }
            base.ViewWillAppear(animated);
        }

        void ChangeTheme(Page page)
        {
            var item = page.ToolbarItems.Where(t => t.ClassId == "basket").FirstOrDefault();
            if (item != null)
            {
                toolBarItem = item;
                page.ToolbarItems.Remove(item);
            }
            var basePage = page as MainBaseContentPage;
            if (basePage != null)
            {
                NavigationBar.BarTintColor = basePage.ActionBarBackgroundColor.ToUIColor();
                NavigationBar.TintColor = basePage.ActionBarTextColor.ToUIColor();
                UINavigationBar.Appearance.ShadowImage = new UIImage();
                UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);

                var titleAttributes = new UIStringAttributes();
                titleAttributes.Font = UIFont.FromName("SegoeUI", 22);
                titleAttributes.ForegroundColor = basePage.ActionBarTextColor == Color.Default ? titleAttributes.ForegroundColor ?? UINavigationBar.Appearance.TintColor : basePage.ActionBarTextColor.ToUIColor();
                NavigationBar.TitleTextAttributes = titleAttributes;

                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            }
        }
    }
}
