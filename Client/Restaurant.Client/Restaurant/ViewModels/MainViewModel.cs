using ReactiveUI;
using Restaurant.Model;
using Restaurant.ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class MainViewModel : ReactiveObject, INavigatableViewModel
    {
        public MainViewModel()
        {
        }

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
                return "Main";
            }
        }
    }
}
