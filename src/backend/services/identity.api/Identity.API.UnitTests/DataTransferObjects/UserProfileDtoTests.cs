using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Identity.API.Model.DataTransferObjects;
using Xunit;

namespace Identity.API.UnitTests.DataTransferObjects
{
    public class UserProfileDtoTests
    {
        [Theory, AutoDomainData]
        public void Test_auto_properties(WritablePropertyAssertion assertion)
        {
            assertion.Verify(new Properties<UserProfileDto>().Select(x => x.BirthDate));
            assertion.Verify(new Properties<UserProfileDto>().Select(x => x.LastName));
            assertion.Verify(new Properties<UserProfileDto>().Select(x => x.Name));
            assertion.Verify(new Properties<UserProfileDto>().Select(x => x.Picture));
            assertion.Verify(new Properties<UserProfileDto>().Select(x => x.Thumbnail));
        }
    }
}