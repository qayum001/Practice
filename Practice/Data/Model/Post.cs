
namespace Practice.Data.Model
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public TimeSpan ReadingTime { get; set; }
        //User
        public Guid UserId { get; set; }
        public User User { get; set; }
        //Like
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        //Tag
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        //Comment
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
