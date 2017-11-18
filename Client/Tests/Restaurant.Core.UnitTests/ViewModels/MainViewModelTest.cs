using NUnit.Framework;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class MainViewModelTest : BaseAutoMockedTest<MainViewModel>
    {
        [Test]
        public void Title_should_be_main()
        {
            Assert.That(ClassUnderTest.Title, Is.EqualTo("Main"));
        }
    }
}
