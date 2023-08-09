using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CommentService(AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> CommentPost(Guid postId, Guid userId, CommentCreateDto commentCreateDto)
        {
            var comment = await CreateComment(userId, postId, commentCreateDto);

            if(comment == null) return new Response { Stasus = "Fail", Message = "Post not found"};
            
            await _context.Comment.AddAsync(comment);

            if (Guid.TryParse(commentCreateDto.ParentId, out Guid parentGuid))
            {
                var parentComment = await _context.Comment.FirstOrDefaultAsync(e => e.Id == parentGuid);

                if (parentComment != null)
                {
                    var childComment = await CreateChildComment(comment, parentGuid);
                    if(childComment != null) await _context.ChildComment.AddAsync(childComment);
                }
            }
            
            await _context.SaveChangesAsync();

            return new Response { Stasus = "Success", Message = "Comment added" };
        }

        public async Task<Response> DeleteComment(Guid commentId, Guid userId)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null) return new Response { Stasus = "Fail", Message = "Not Found" };

            if (comment.UserId != userId) return new Response { Stasus = "Fail", Message = "User not allowed" };

            comment.DelitedDate = DateTime.UtcNow;
            comment.AuthorName = "[Comment deleted]";
            comment.Text = "[Comment deleted]";

            await _context.SaveChangesAsync();

            return new Response { Stasus = "Success", Message = "CommentDeleted"};
        }

        public async Task<Response> EditComment(Guid commentId, Guid userId, string editedText)
        {
            var comment = await _context.Comment.FindAsync(commentId);

            if (comment == null) return new Response { Stasus = "Fail", Message = "Comment not found" };

            var isDeleted = comment.DelitedDate.Year != 1;

            if (isDeleted) return new Response { Stasus = "Fail", Message = "Comment Deleted" };

            if (comment.UserId != userId) return new Response { Stasus = "Fail", Message = "User not allowed" };

            comment.Text = editedText;
            comment.ModifiedTime = DateTime.Now;

            await _context.SaveChangesAsync();

            return new Response { Stasus = "Succes", Message = "Comment edited" };
        }

        public async Task<List<CommentDto>?> GetComments(Guid id)
        {
            /*
             * Рекурсивные сущности и запросы к ним это не самая простая тема.
             * Решение здесь абсолюно подходит.
             *
             * Так как кол-во комментариев условно бесконтрольно растут и получение комментов одна из самых тяжелых задач производительности
             * Необходимо прикрутить паггинацию, причем однозначного решения я не вижу
             *      можно грузить N комментов и все дерево ответов
             *      можно грузить по уровням вложенности и пагинацию внутри них
             *      или чего еще
             *
             * И нужно оптимизировать кол-во запросов к базе 
             */

            var comment = await _context.Comment
                .Include(e => e.ChildComments)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (comment == null) return null;

            var res = new List<CommentDto>();

            if (comment.ChildComments != null)
            {
                foreach (var subCommentId in comment.ChildComments)
                {
                    var subComment = await _context.Comment.Include(e => e.ChildComments).FirstAsync(e => e.Id ==  subCommentId.CommentId);

                    res.Add(_mapper.Map<CommentDto>(subComment)); 
                }
            }

            return res;
        }

        private async Task<Comment?> CreateComment(Guid userId, Guid postId, CommentCreateDto commentCreateDto)
        {
            var comment = new Comment();

            var post = await _context.Post.FirstOrDefaultAsync(e => e.Id == postId);
            if (post == null) return null;

            var user = await  _context.User.FirstAsync(e => e.Id == userId);

            comment.Id = Guid.NewGuid();
            comment.PostId = postId;
            comment.Post = post;
            comment.UserId = userId;
            comment.User = user;
            comment.CreateTime = DateTime.Now;
            comment.Text = commentCreateDto.Comment;
            comment.AuthorName = user.FullName;

            return comment;
        }

        private Task<ChildCommentId> CreateChildComment(Comment comment, Guid parentId)
        {
            return Task.FromResult(new ChildCommentId { Id = Guid.NewGuid(), CommentId = comment.Id, ParentId = parentId });
        }
    }
}