namespace Practice.Data.Dto
{
    public class CommentCreateDto
    {
        public string ParentId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
