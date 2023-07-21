using Practice.Data.Dto;

namespace Practice.Services.TagService
{
    public interface ITagService
    {
        Task<List<TagDto>> GetTagList();
    }
}