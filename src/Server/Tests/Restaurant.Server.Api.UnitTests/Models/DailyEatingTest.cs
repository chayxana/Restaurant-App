using Albedo;
using AutoFixture.Idioms;
using Restaurant.Server.Api.Models;
using Xunit;

namespace Restaurant.Server.Api.UnitTests.Models
{
    public class DailyEatingTest
    {
		[Theory, AutoDomainData]
	    public void Test_daily_eating_auto_properties(WritablePropertyAssertion assertion)
	    {  
			assertion.Verify(new Properties<DailyEating>().Select(d => d.DateTime));
			assertion.Verify(new Properties<DailyEating>().Select(d => d.AdditionalAmount));
			assertion.Verify(new Properties<DailyEating>().Select(d => d.Amount));
			assertion.Verify(new Properties<DailyEating>().Select(d => d.Decsription));
			assertion.Verify(new Properties<DailyEating>().Select(d => d.Reciept));
		}
    }
}
