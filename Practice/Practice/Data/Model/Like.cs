using Microsoft.Extensions.Hosting;

namespace Practice.Data.Model
{
    public class Like
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime DeliteTime { get; set; }
        //User    
        public Guid UserId { get; set; }
        public User User { get; set; }
        //Post    
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
