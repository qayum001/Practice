using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.TagService
{
    public interface ITagService
    {
        Task<List<TagDto>> GetTagDtoList();
        Task<List<Tag>> GetTagListByIdList(List<Guid> guids);
        Task AddTagsToPost(Post post, List<Guid> tagGuidList);
        Task DeleteTagsFromPost(Post post, List<Guid> tagGuidList);
    }
}