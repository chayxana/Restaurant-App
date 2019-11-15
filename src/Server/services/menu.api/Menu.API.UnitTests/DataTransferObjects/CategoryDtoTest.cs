using Albedo;
using AutoFixture.Idioms;
using BaseUnitTests;
using Menu.API.DataTransferObjects;
using Xunit;

namespace Menu.API.UnitTests.DataTransferObjects
{
    
    public class CategoryDtoTest
    {
        [Theory, AutoDomainData]
        public void Tets_Getter_And_Setters_CategoryDto(WritablePropertyAssertion assertion) 
        {
            assertion.Verify(new Properties<CategoryDto>().Select(c => c.Id));
            assertion.Verify(new Properties<CategoryDto>().Select(c => c.Name));
            assertion.Verify(new Properties<CategoryDto>().Select(c => c.Color));
        }
    }
}