namespace Practice.Data.Dto
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int SubCommentsCount { get; set; }
    }
}
