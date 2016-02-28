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
        AuthenticationResult Model;
        public MainViewModel(AuthenticationResult result)
        {
            Model = result;
        }
        public IScreen HostScreen { get; set; }

        public string UrlPathSegment
        {
            get { return Model.userName; }
        }
    }
}
