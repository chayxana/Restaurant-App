using Xamarin.Forms;

namespace Restaurant.Views
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