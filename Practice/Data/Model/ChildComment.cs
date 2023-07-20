namespace Practice.Data.Model
{
    public class ChildComment
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Comment ParentComment { get; set; }
    }
}
