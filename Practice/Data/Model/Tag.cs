namespace Practice.Data.Model
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        //Post
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}