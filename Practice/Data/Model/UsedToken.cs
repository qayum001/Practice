namespace Practice.Data.Model
{
    public class UsedToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
