namespace Practice.Data.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime Created { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Post
        public bool IsAuthor { get; set; } = false;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        //Like
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        //Comment
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
