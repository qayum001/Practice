namespace Practice.Data.Model
{
    public class Pagination
    {
        public List<Guid>? TagGuidList { get; set; }
        public string? AuthorName { get; set; }
        public int MinReadTime { get; set; }
        public int MaxReadTime { get; set; }
        public Sort Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PostCount { get; set; } = 5;
    }
}