using Practice.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class EditUserDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}
