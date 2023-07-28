namespace Practice.Data.Dto
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<Guid> TagGuidList { get; set; }
        public long ReadTime { get; set; }
    }
}
