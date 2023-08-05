using Practice.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class AuthorDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public int Posts { get; set; }
        public int Likes { get; set; }
        public DateTime Created { get; set; }
    }
}