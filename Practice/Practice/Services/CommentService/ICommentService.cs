using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<CommentDto>?> GetComments(Guid id);
        Task<Response> CommentPost(Guid postId, Guid userId, CommentCreateDto commentCreateDto);
        Task<Response> EditComment(Guid commentId, Guid userId, string editedText);
        Task<Response> DeleteComment(Guid commentId, Guid userId);
    }
}
