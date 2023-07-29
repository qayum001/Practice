namespace Practice.Data.Dto
{
    public class PostDto
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public TimeSpan ReadTime { get; set; }
        public int LikesCount { get; set; }
        public List<TagDto> TagList { get; set; }
    }
}