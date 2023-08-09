namespace Practice.Data.Dto
{
    public class CommentCreateDto
    {
        // если ожидается Guid то почему string 
        public string ParentId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
