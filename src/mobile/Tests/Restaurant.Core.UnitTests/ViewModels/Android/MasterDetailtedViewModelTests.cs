using NUnit.Framework;
using Restaurant.Core.ViewModels.Android;

namespace Restaurant.Core.UnitTests.ViewModels.Android
{
    public class MasterDetailtedViewModelTests : BaseAutoMockedTest<MasterDetailedMainViewModel>
    {
		[Test]
	    public void Test_MasterDetailedViewModel_auto_properties()
	    {
		    var masterDetailtedViewModel = ClassUnderTest;
		    masterDetailtedViewModel.IsNavigated = true;

			Assert.That(masterDetailtedViewModel.IsNavigated, Is.True);
			Assert.That(masterDetailtedViewModel.MasterViewModel, Is.Not.Null);
	    }
    }
}
