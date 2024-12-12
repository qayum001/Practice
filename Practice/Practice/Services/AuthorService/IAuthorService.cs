using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<AuthorDto?> GetAuthorDto(Guid id);
        Task<Response> CreatePost(Guid AuthorId, PostCreateDto postDto);
        Task<List<AuthorDto>> GetAuthorList();
        Task<Response> EditPost(Guid userId, PostEditDto postEditDto);
        Task<Response> DeletePost(Guid userId, Guid postId);
    }
}