using System;

namespace Identity.API.Model.DataTransferObjects
{
    public class UserProfileDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; }
        public string Thumbnail { get; set; }
    }
}