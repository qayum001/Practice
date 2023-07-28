using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.TagService;
using Practice.Services.UserService;

namespace Practice.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;

        public AuthorService(AppDbContext context,
            ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }

        public async Task<AuthorDto?> GetAuthorDto(Guid id)
        {
            var author = await _context.User
                .Include(e => e.Posts)
                .Include(e => e.Likes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (author == null || !author.IsAuthor) return null;

            var authorDto = new AuthorDto()
            {
                Id = author.Id,
                FullName = author.FullName,
                CreateTime = author.Created,
                BirthDate = author.BirthDay,
                Likes = author.Likes.Count,
                Posts = author.Posts.Count,
                Gender = author.Gender,
            };

            return authorDto;
        }

        public async Task<Response> CreatePost(Guid AuthorId, PostCreateDto postDto)
        {
            var user = await _context.User.Include(e => e.Posts).FirstAsync(e => e.Id == AuthorId);

            var tagList = await _tagService.GetTagListByIdList(postDto.TagGuidList);

            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Text = postDto.Body,
                UserId = user.Id,
                User = user,
                ReadingTime = new TimeSpan(postDto.ReadTime * 1000 * 60),
                Tags = tagList,
            };

            if (!user.IsAuthor)
                user.IsAuthor = true;

            await _context.Post.AddAsync(post);

            _context.User.OrderBy(e => e.Posts.Count);

            await _context.SaveChangesAsync();

            var responce = new Response()
            {
                Stasus = "Created",
                Message = $"Created post {postDto.Title}"
            };

            return responce;
        }

        public Task<List<AuthorDto>> GetAuthorList()
        {
            var authorList = _context.User
                .Include(e => e.Posts)
                .Include(e => e.Likes).Where(User => User.IsAuthor == true).ToList();

            var authorDtoList = new List<AuthorDto>();

            foreach(var author in authorList)
            {
                var authorDto = new AuthorDto()
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    BirthDate = author.BirthDay,
                    CreateTime = author.Created,
                    Gender = author.Gender,
                    Posts = author.Posts.Count,
                    Likes = author.Likes.Count,
                };

                authorDtoList.Add(authorDto);
            }

            return Task.FromResult(authorDtoList);
        }

        public async Task<Response> DeletePost(Guid userId, Guid postId)
        {
            var post = await _context.Post.FirstAsync(e => e.Id == postId);

            var respose = new Response();

            if(post.UserId != userId)
            {
                respose.Stasus = "Unsuccess deleting";
                respose.Message = "User is not author of this post";

                return respose;
            }

            _context.Post.Remove(post);

            await _context.SaveChangesAsync();

            respose.Stasus = "Success";
            respose.Message = "Post has beed deleted";

            return respose;
        }

        public async Task<Response> EditPost(Guid userId, PostEditDto postEditDto)
        {
            var post = await _context.Post.Include(e => e.Tags).FirstAsync(e => e.Id == postEditDto.PostId);

            var respose = new Response();

            if (post.UserId != userId)
            {
                respose.Stasus = "Unsuccess editing";
                respose.Message = "User is not author of this post";

                return respose;
            }

            post.Title = string.IsNullOrEmpty(postEditDto.Title) ? post.Title : postEditDto.Title;
            post.Text = string.IsNullOrEmpty(postEditDto.Text) ? post.Text : postEditDto.Text;
            post.ReadingTime = postEditDto.ReadTime == 0 ? post.ReadingTime : new TimeSpan(postEditDto.ReadTime * 60 * 1000);

            //do not save _context
            await _tagService.AddTagsToPost(post, postEditDto.AddTagGuidList);
            await _tagService.DeleteTagsFromPost(post, postEditDto.DeleteTagGuidList);

            await _context.SaveChangesAsync();

            respose.Stasus = "Success";
            respose.Message = "Post has been edited";

            return respose;
        }
    }
}