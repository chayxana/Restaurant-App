using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Identity.API.Model.DataTransferObjects;
using Xunit;

namespace Identity.API.UnitTests.DataTransferObjects
{
    public class LoginDtoUnitTests
    {
        [Theory, AutoDomainData]
        public void Test_user_auto_properties(WritablePropertyAssertion assertion)
        {
            assertion.Verify(new Properties<LoginDto>().Select(d => d.Login));
            assertion.Verify(new Properties<LoginDto>().Select(d => d.Password));
        }
    }
}