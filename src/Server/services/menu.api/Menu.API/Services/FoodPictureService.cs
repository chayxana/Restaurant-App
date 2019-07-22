using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.API.Abstraction.Managers;
using Menu.API.Abstraction.Repositories;
using Menu.API.Abstraction.Services;
using Menu.API.DataTransferObjects;
using Menu.API.Models;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Services
{
    public class FoodPictureService : IFoodPictureService
    {
        private readonly IFileUploadManager _fileUploadManager;
        private readonly IRepository<FoodPicture> _pictureRepository;

        public FoodPictureService(
            IFileUploadManager fileUploadManager,
            IRepository<FoodPicture> pictureRepository)
        {
            _fileUploadManager = fileUploadManager ?? throw new ArgumentNullException(nameof(fileUploadManager));
            _pictureRepository = pictureRepository ?? throw new ArgumentNullException(nameof(pictureRepository));
        }
        public async Task UploadAndCreatePictures(IList<IFormFile> files, string foodId) 
        {
            foreach (var file in files)
            {
                var fileName = await _fileUploadManager.Upload(file);

                var foodPicture = new FoodPicture()
                {
                    Id = Guid.NewGuid(),
                    FoodId = Guid.Parse(foodId),
                    OriginalFileName = file.FileName,
                    FileName = fileName,
                    Length = file.Length,
                    ContentType = file.ContentType
                };

                _pictureRepository.Create(foodPicture);
                await _pictureRepository.Commit();
            }
        }

        public Task RemovePictures(IList<FoodPictureDto> pictures) 
        {
            foreach (var picture in pictures)
            {
                _fileUploadManager.Remove(picture.FilePath);
                var pictureForDelete = _pictureRepository.Get(picture.Id);
                _pictureRepository.Delete(pictureForDelete);
            }

            return Task.CompletedTask;
        }
    }
}