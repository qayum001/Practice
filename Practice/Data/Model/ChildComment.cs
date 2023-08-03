namespace Practice.Data.Model
{
    public class ChildCommentId
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid CommentId { get; set; }
    }
}
