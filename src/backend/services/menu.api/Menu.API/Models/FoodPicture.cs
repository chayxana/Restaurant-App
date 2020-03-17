using System;
using System.ComponentModel.DataAnnotations;

namespace Menu.API.Models
{
    public class FoodPicture
    {
        [Key]
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public long Length { get; set; }

        public string ContentType { get; set; }

        public Guid FoodId { get; set; }

        public virtual Food Food { get; set; }
    }
}