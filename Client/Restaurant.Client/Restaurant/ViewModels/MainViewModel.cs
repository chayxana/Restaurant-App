using ReactiveUI;
using Restaurant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        public MainViewModel(AuthenticationResult result)
        {

        }
        public IScreen HostScreen { get; set; }

        public string UrlPathSegment
        {
            get { return "Restaurant"; }
        }
    }
}
