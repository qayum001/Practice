using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostDto>> GetPostDtoList()
        {
            var postList = _context.Post
                .Include(e => e.Tags)
                .Include(e => e.Likes)
                .ToList();

            var dtoList = new List<PostDto>();

            foreach (var post in postList)
            {
                var postDtoList = new PostDto
                {
                    PostId = post.Id,
                    UserId = post.UserId,
                    Title = post.Title,
                    Body = post.Text,
                    ReadTime = post.ReadingTime,
                    LikesCount = post.Likes.Count,
                    TagList = await GetTagDtoList(post.Tags.ToList())
                };

                dtoList.Add(postDtoList);
            }

            return dtoList;
        }

        public async Task<Response?> DisLikePost(Guid userId, Guid postId)
        {
            var post = await _context.Post
                .Include(e => e.Likes)
                .FirstOrDefaultAsync(e => e.Id == postId);

            if (post == null) return null;

            var user = await _context.User
                .Include(e => e.Likes)
                .FirstAsync(e => e.Id == userId);

            var LikedPost = await _context.Like.FirstAsync(e => e.UserId == userId && e.PostId == postId);

            if (LikedPost == null) return null;

            _context.Like.Remove(LikedPost);

            await _context.SaveChangesAsync();

            var responce = new Response
            {
                Stasus = "Success",
                Message = "Like has been removed"
            };

            return responce;
        }

        public async Task<PostDto?> GetPostDtoById(Guid id)
        {
            var post = await _context.Post
                .Include(e => e.Tags)
                .Include(e => e.Likes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (post == null) return null;

            var postDto = new PostDto
            {
                PostId = id,
                UserId = post.UserId,
                Title = post.Title,
                Body = post.Text,
                ReadTime = post.ReadingTime,
                LikesCount = post.Likes.Count,
                TagList = await GetTagDtoList(post.Tags.ToList())
            };

            return postDto;
        }

        public async Task<Response?> LikePost(Guid userId, Guid postId)
        {
            var post = await _context.Post
                .Include(e => e.Likes)
                .FirstOrDefaultAsync(e => e.Id == postId);

            if (post == null) return null;

            var user = await _context.User
                .Include(e => e.Likes)
                .FirstAsync(e => e.Id == userId);

            var LikedPost = user.Likes.FirstOrDefault(e => e.PostId == postId);

            var response = new Response();

            if (LikedPost != null)
            {
                response.Stasus = "Fail";
                response.Message = "Already liked";

                return response;
            }

            var like = new Like
            {
                UserId = userId,
                PostId = postId,
                CreateTime = DateTime.Now,
                User = user,
                Post = post,
            };

            user.Likes.Add(like);
            post.Likes.Add(like);
            
            await _context.SaveChangesAsync();

            response.Stasus = "Success";
            response.Message = "Post has been liked";

            return response;
        }

        private static Task<List<TagDto>> GetTagDtoList(List<Tag> tags)
        {
            var result = new List<TagDto>();

            foreach (var tag in tags)
            {
                result.Add(new TagDto()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    CreateTime = tag.CreatedTime
                });
            }

            return Task.FromResult(result);
        }
    }
}