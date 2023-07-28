using Practice.Data.Dto;

namespace Practice.Services.PostService
{
    public interface IPostService
    {
        Task<List<PostDto>> GetPostDtoList();
    }
}
