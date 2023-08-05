using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Dto
{
    public class CommentDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime DelitedDate { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int SubCommentsCount { get; set; }
    }
}