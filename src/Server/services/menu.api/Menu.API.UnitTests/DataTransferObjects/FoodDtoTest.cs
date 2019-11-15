using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Menu.API.DataTransferObjects;
using Xunit;

namespace Menu.API.UnitTests.DataTransferObjects
{
    public class FoodDtoTest
    {
        [Theory, AutoDomainData]
        public void Test_Getter_And_Setters_FoodDto(WritablePropertyAssertion assertion)
        {
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Id));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Name));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Category));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.CategoryId));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Currency));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.DeletedPictures));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Description));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Pictures));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Price));
        }
    }
}