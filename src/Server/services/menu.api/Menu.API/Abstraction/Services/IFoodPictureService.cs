using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.API.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Abstraction.Services
{
    public interface IFoodPictureService
    {
         Task UploadAndCreatePictures(IList<IFormFile> files, string foodId);

         Task RemovePictures(IList<FoodPictureDto> pictures);
    }
}