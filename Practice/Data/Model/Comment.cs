namespace Practice.Data.Model
{
    public class Comment
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime DelitedDate { get; set; }
        public string Text { get; set; }
        public int SubCommentsCount { get; set; }
        //User    
        public Guid UserId { get; set; }
        public User User { get; set; }
        //Comment
        public Guid? ParentCommentId { get; set; }
        public ParentComment? ParentComment { get; set; }
        public Guid? ChildCommentId { get; set; }
        public ICollection<ChildComment>? ChildComments { get; set; } = new List<ChildComment>();
        //Post
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
