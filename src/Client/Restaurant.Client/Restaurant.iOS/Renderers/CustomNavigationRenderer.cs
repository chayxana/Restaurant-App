using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Restaurant.iOS.Renderers;
using Restaurant.Mobile.UI.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]
namespace Restaurant.iOS.Renderers
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        private readonly Dictionary<UIButton, ICommand> buttonCommands = new Dictionary<UIButton, ICommand>();
        private ToolbarItem toolBarItem;

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            //ChangeTheme(page);
            return base.OnPushAsync(page, animated);
        }

        public override UIViewController PopViewController(bool animated)
        {
            //var obj = Element.GetType().InvokeMember("StackCopy", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, Element, null);
            //if (obj != null)
            //{
            //    var pages = obj as Stack<Page>;
            //    if (pages != null && pages.Count >= 2)
            //    {
            //        var copy = new Page[pages.Count];
            //        pages.CopyTo(copy, 0);

            //        var prev = copy[1];
            //        ChangeTheme(prev);
            //    }
            //}
            return base.PopViewController(animated);
        }

        public override void ViewWillAppear(bool animated)
        {
            if (toolBarItem != null)
            {
                //MainViewModel viewModel = (toolBarItem.BindingContext as FoodsViewModel)?.MainViewModel;
                //UIButton button = UIButton.FromType(UIButtonType.Custom);
                //button.Frame = new CGRect(0, -5, 25, 25);
                //button.SetImage(UIImage.FromFile("ic_shopping_cart_white_2x.png"), UIControlState.Normal);
                //button.TouchUpInside += (s, e) =>
                //{
                //    var tool = s as UIButton;
                //    var command = buttonCommands[tool];
                //    command.Execute(null);
                //};
                //buttonCommands.Add(button, toolBarItem.Command);
                //BadgeBarButtonItem barButtonItem = new BadgeBarButtonItem(button);
                //NavigationBar.Items[0].RightBarButtonItem = barButtonItem;
                //viewModel.OrderViewModel.WhenAnyValue(x => x.OrdersCount).Subscribe(x =>
                //{
                //    barButtonItem.BadgeValue = x.ToString();
                //});
            }
            base.ViewWillAppear(animated);
        }

        private void ChangeTheme(Page page)
        {
            var item = page.ToolbarItems.FirstOrDefault(t => t.ClassId == "basket");
            if (item != null)
            {
                toolBarItem = item;
                page.ToolbarItems.Remove(item);
            }
            var basePage = page as MainBaseContentPage;
            if (basePage != null)
            {
                //NavigationBar.BarTintColor = basePage.ActionBarBackgroundColor.ToUIColor();
                //NavigationBar.TintColor = basePage.ActionBarTextColor.ToUIColor();
                //UINavigationBar.Appearance.ShadowImage = new UIImage();
                //UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);

                //var titleAttributes = new UIStringAttributes();
                //titleAttributes.Font = UIFont.FromName("SegoeUI", 22);
                //titleAttributes.ForegroundColor = basePage.ActionBarTextColor == Color.Default
                //    ? titleAttributes.ForegroundColor ?? UINavigationBar.Appearance.TintColor
                //    : basePage.ActionBarTextColor.ToUIColor();
                //NavigationBar.TitleTextAttributes = titleAttributes;

                //UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            }
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}