namespace Practice.Data.Model
{
    /*
     * По сути бесполезная псевдо сущность
     * 1 ей не нужен свой ключ, можно юзать композитный
     * 2 для твоей схемы достаточно просто запихнуть опциональный ParentId 
     */
    public class ChildCommentId
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid CommentId { get; set; }
    }
}
