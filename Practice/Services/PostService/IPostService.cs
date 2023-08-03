using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.PostService
{
    public interface IPostService
    {
        Task<List<PostDto>?> GetPostDtoList(Pagination pagination);
        Task<PostWithCommentsDto?> GetPostDtoById(Guid id);
        Task<Response?> LikePost(Guid userId, Guid postId);
        Task<Response?> DisLikePost(Guid userId, Guid postId);
    }
}
