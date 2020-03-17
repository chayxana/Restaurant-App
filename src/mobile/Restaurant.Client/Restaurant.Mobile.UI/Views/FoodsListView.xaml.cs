using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Views
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class FoodsListView : CollectionView
    {
        public FoodsListView()
        {
            InitializeComponent();
//            ItemSelected += (_, __) => SelectedItem = null;
        }
    }
}