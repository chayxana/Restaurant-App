using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Pages.MainPages
{
    public partial class MasterPage : ContentPage
    {
        public MainViewModel ViewModel { get; set; }

        public MasterPage(MainViewModel viewModel)
        {
            InitializeComponent();
        }
    }
}
