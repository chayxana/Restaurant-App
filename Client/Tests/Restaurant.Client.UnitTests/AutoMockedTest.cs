using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using Restaurant.Mobile.UI;

namespace Restaurant.Client.UnitTests
{
    public abstract class BaseAutoMockedTest<T> 
		where T : class
    {
	    protected virtual T ClassUnderTest => Mocker.Create<T>();

	    protected AutoMock Mocker { get; private set; }
		
	    [SetUp]
	    public virtual void Init()
	    {
	        Mocker = AutoMock.GetLoose();
		    Bootstrapper.MockData = false;
	    }

		[TearDown]
	    public void TearDown()
	    {
		    Mocker?.Dispose();
	    }
	    
		protected Mock<TDepend> GetMock<TDepend>()
		    where TDepend : class
	    {
		    return Mocker.Mock<TDepend>();
	    }
    }
}
