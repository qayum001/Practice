namespace Practice.Data.Dto
{
    public class PostEditDto
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int ReadTime { get; set; }
        public List<Guid> AddTagGuidList { get; set; }
        public List<Guid> DeleteTagGuidList { get; set; }

    }
}
