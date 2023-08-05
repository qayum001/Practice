namespace Practice.Data.Dto
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime DelitedDate { get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int SubCommentsCount { get; set; }
    }
}