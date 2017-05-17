using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Legacy;
using Restaurant.Abstractions;

namespace Restaurant.ViewModels
{
    public class WelcomeViewModel : INavigatableViewModel
    {
        public string Title { get; }

        /// <summary>
        /// Gets and sets Open regester, 
        /// Command that opens regester page
        /// </summary>
        public ReactiveCommand<object> OpenRegester { get; set; }

        /// <summary>
        /// Gets and sets OpenLogin
        /// Command thats opens login page 
        /// </summary>
        public ReactiveCommand<object> OpenLogin { get; set; }
    }
}
