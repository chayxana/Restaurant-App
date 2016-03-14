using ReactiveUI;
using Restaurant.ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class FoodsViewModel : ReactiveObject, INavigatableViewModel
    {
        public INavigatableScreen NavigationScreen
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                return "Foods";
            }
        }
    }
}
