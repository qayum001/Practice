using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;

namespace Practice.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<PostDto>> GetPostDtoList()
        {
            var postList = _context.Post.Include(e => e.Tags).ToList();

            var dtoList = new List<PostDto>();

            foreach (var post in postList)
            {
                var postDtoList = new PostDto()
                {
                    PostId = post.Id,
                    UserId = post.UserId,
                    Title = post.Title,
                    Body = post.Text,
                    ReadTime = post.ReadingTime,
                };

                var tagDtoList = new List<TagDto>();

                foreach (var tag in post.Tags)
                {
                    tagDtoList.Add(new TagDto()
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        CreateTime = tag.CreatedTime
                    });
                }
                postDtoList.TagList = tagDtoList;

                dtoList.Add(postDtoList);
            }

            return Task.FromResult(dtoList);
        }
    }
}