using NUnit.Framework;
using Restaurant.Core.ViewModels.iOS;

namespace Restaurant.Core.UnitTests.ViewModels.iOS
{
    public class TabbedViewModelTests : BaseAutoMockedTest<TabbedMainViewModel>
    {
		[Test]
	    public void Test_auto_properties()
		{
			var result = ClassUnderTest;
		}
    }
}
