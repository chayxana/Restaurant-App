using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages.MainPages
{
    public partial class FoodsPage : FoodsXamlPage
    {
        public FoodsPage()
        {
            InitializeComponent();
        }
    }
    public class FoodsXamlPage : BaseContentPage<FoodsViewModel>
    {

    }
}
