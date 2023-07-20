namespace Practice.Data.Model
{
    public class ParentComment
    {
        public Guid Id { get; set; }
        public Guid ChildId { get; set; }
        public Comment ChildComment { get; set; }
    }
}
