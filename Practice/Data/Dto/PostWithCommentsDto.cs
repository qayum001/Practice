namespace Practice.Data.Dto
{
    public class PostWithCommentsDto
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime Created { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int ReadTime { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public bool HasLike { get; set; }
        public List<TagDto> TagList { get; set; }
        public List<CommentDto> CommentList { get; set;}
    }
}
