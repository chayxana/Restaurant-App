using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Identity.API.Model.DataTransferObjects;
using Xunit;

namespace Identity.API.UnitTests.DataTransferObjects
{
    public class UserDtoTests
    {
        [Theory, AutoDomainData]
        public void Test_auto_properties(WritablePropertyAssertion assertion) 
        {
            assertion.Verify(new Properties<UserDto>().Select(x => x.Email));
            assertion.Verify(new Properties<UserDto>().Select(x => x.Profile));
        }
    }
}