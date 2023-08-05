using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class TagDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
    }
}
