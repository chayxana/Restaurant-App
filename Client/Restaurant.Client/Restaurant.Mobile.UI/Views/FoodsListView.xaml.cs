using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Views
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class FoodsListView : ListView
	{
		public FoodsListView() : base(ListViewCachingStrategy.RecycleElement)
		{
			InitializeComponent();
		}
	}
}