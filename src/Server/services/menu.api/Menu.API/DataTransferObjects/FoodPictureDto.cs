using System;

namespace Menu.API.DataTransferObjects
{
    public class FoodPictureDto
    {
        public Guid Id { get; set; }

        public string FilePath { get; set; }
    }
}