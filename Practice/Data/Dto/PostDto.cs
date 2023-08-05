namespace Practice.Data.Dto
{
    public class PostDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int ReadTime { get; set; }
        public int LikesCount { get; set; }
        public bool HasLike { get; set; }
        public List<TagDto> TagList { get; set; }
    }
}