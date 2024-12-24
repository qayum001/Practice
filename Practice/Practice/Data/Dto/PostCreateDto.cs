namespace Practice.Data.Dto
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Guid> TagGuidList { get; set; }
        public int ReadTime { get; set; }
    }
}
