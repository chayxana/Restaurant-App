using ReactiveUI;
using Restaurant.Model;
using Restaurant.Models;
using Restaurant.Pages.MainPages;
using Restaurant.ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class MainViewModel : ReactiveObject, INavigatableViewModel
    {
        public ClientUser User { get; set; }

        public MainViewModel(ClientUser user)
        {
            User = user;
        }

        public INavigatableScreen NavigationScreen
        {
            get;
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
