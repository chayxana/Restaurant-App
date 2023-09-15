using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Identity.API.Model.DataTransferObjects;
using Xunit;

namespace Identity.API.UnitTests.DataTransferObjects
{
    public class RegisterDtoUnitTests
    {
        [Theory, AutoDomainData]
        public void Test_auto_properties(WritablePropertyAssertion assertion)
        {
            assertion.Verify(new Properties<RegisterDto>().Select(d => d.Email));
            assertion.Verify(new Properties<RegisterDto>().Select(d => d.Password));
        }
    }
}