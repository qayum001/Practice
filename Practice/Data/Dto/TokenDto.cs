using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class TokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}
