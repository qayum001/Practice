using Practice.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }


}