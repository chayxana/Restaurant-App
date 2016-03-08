using ReactiveUI;
using Restaurant.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Restaurant.Views
{
    public partial class RegesterView : ContentPage, IViewFor<RegesterViewModel>
    {
        public RegesterView()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<RegesterViewModel>();
            //ViewModel.WhenAnyValue(x => x.IsLoading).Subscribe((x) => 
            //{
            //    if (x)
            //    {
            //        loadingLayout.IsVisible = true;
            //        regesterStack.IsVisible = false;
            //    }
            //    else
            //    {
            //        loadingLayout.IsVisible = false;
            //        regesterStack.IsVisible = true;
            //    }
            //});
            
            BindingContext = ViewModel;
        }

        public RegesterViewModel ViewModel
        {
            get { return (RegesterViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<RegesterView, RegesterViewModel>(x => x.ViewModel, default(RegesterViewModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }

            set { ViewModel = (RegesterViewModel)value; }
        }
    }
}
