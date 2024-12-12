namespace Practice.Data.Model
{
    public class Comment
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime DelitedDate { get; set; }
        public string Text { get; set; }
        //User    
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string AuthorName { get; set; }
        //Comment
        public Guid? ParentCommentId { get; set; }
        public ICollection<ChildCommentId>? ChildComments { get; set; } = new List<ChildCommentId>();
        //Post
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
