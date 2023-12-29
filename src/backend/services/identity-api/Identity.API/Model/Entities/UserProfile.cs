using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Model.Entities
{
    public class UserProfile
    {
        [Key]
        public virtual Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }
    }
}