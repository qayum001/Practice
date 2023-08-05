using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class PostDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int ReadTime { get; set; }
        [Required]
        public int LikesCount { get; set; }
        [Required]
        public bool HasLike { get; set; }
        public List<TagDto> TagList { get; set; }
    }
}