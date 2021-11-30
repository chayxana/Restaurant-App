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
        private readonly ToolbarItem toolBarItem;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            if (page is ITransparentActionBarPage transparentActionBarPage &&
                transparentActionBarPage.IsTransparentActionBar)
            {
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
                NavigationBar.TintColor = UIColor.White;

                UITextAttributes titleTextAttributes = UINavigationBar.Appearance.GetTitleTextAttributes();
                var stringAttributes = new UIStringAttributes
                {
                    Font = titleTextAttributes.Font,
                    ForegroundColor = UIColor.White
                };
                
                NavigationBar.TitleTextAttributes = stringAttributes;
                UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationBar.ShadowImage = new UIImage();
                NavigationBar.BackgroundColor = new UIColor(0, 0, 0, 0);
                NavigationBar.Translucent = true;
            }
            
            return base.OnPushAsync(page, animated);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            return base.OnPopViewAsync(page, animated);
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
            NavigationBar.TintColor = UIColor.Black;
        }
        
        

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}