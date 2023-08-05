using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PostService(AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PostDto>?> GetPostDtoList(Pagination pagination)
        {
            var pagePosts = await GetPagePosts(pagination);

            if(pagePosts == null) return null;

            var dtoList = new List<PostDto>();

            foreach (var post in pagePosts)
            {
                var postDto = _mapper.Map<PostDto>(post)
;
                postDto.TagList = await GetTagDtoList(post.Tags.ToList());
                postDto.AuthorName = _context.User.First(e => e.Id == post.UserId).FullName;

                dtoList.Add(postDto);
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

        public async Task<PostWithCommentsDto?> GetPostDtoById(Guid id)
        {
            var post = await _context.Post
                .Include(e => e.Tags)
                .Include(e => e.Likes)
                .Include(e => e.User)
                .Include(e => e.Comments)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (post == null) return null;

            var commentsList = new List<CommentDto>();

            foreach (var comment in post.Comments)
            {
                var subComments = await _context.Comment.Include(e => e.ChildComments).FirstAsync(e => e.Id == comment.Id);
                var subCommentsCount = subComments.ChildComments == null ? 0 : subComments.ChildComments.Count;

                if (comment.ParentCommentId != null) continue;

                var commentDto = _mapper.Map<CommentDto>(comment);

                commentDto.SubCommentsCount = subCommentsCount;

                commentsList.Add(commentDto);
            }

            var postDtoWC = _mapper.Map<PostWithCommentsDto>(post);

            postDtoWC.PostId = id;
            postDtoWC.TagList = await GetTagDtoList(post.Tags.ToList());
            postDtoWC.CommentList = commentsList;

            return postDtoWC;
        }

        private async Task<List<Post>?> GetPagePosts(Pagination pagination)
        {
            var postList = await SortSwitch(pagination.Sort);

            var nameSort = postList;

            if (pagination.AuthorName != null) nameSort = await SearchByAuthor(postList, pagination.AuthorName);

            var tagSorted = nameSort;

            if (pagination.TagGuidList != null && pagination.TagGuidList.Count != 0) tagSorted = await SortByTag(nameSort, pagination.TagGuidList);

            if (tagSorted.Count == 0) return null;

            var readTimeSorted = tagSorted;

            if (pagination.MinReadTime != 0 || pagination.MaxReadTime != 0)
                readTimeSorted = await ReadTimeSort(tagSorted, pagination.MinReadTime, pagination.MaxReadTime);

            if (readTimeSorted.Count == 0) return null;

            var pagePosts = await GetPagePost(readTimeSorted, pagination.Page, pagination.PostCount);

            return pagePosts;
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

        private Task<List<Post>> SortByTag(List<Post> postList, List<Guid> tagGuidList)
        {
            var tagsCount = tagGuidList.Count;

            var res = new List<Post>();

            foreach (var post in postList)
            {
                var tagsInPost = post.Tags.ToArray();

                var hasTags = false;


                foreach (var tagId in tagGuidList)
                {
                    hasTags = tagsInPost.Contains(_context.Tag.First(e => e.Id == tagId));
                }

                if (hasTags) res.Add(post);
            }

            return Task.FromResult(res);
        }

        private Task<List<Post>> SortSwitch(Sort sort) => sort switch
        {
            Sort.CreateAsc => Task.FromResult(_context.Post
                .Include(e => e.Tags)
                .Include(e => e.Likes)
                .Include(e => e.User)
                .OrderBy(e => e.Created)
                .ToList()),

            Sort.CreateDesc => Task.FromResult(_context.Post
                .Include(e => e.Tags)
                .Include(e => e.User)
                .Include(e => e.Likes)
                .OrderByDescending(e => e.Created)
                .ToList()),

            Sort.LikeAsc => Task.FromResult(_context.Post
                .Include(e => e.Tags)
                .Include(e => e.Likes)
                .Include(e => e.User)
                .OrderBy(e => e.Likes.Count)
                .ToList()),

            Sort.LikeDesc => Task.FromResult(_context.Post
                .Include(e => e.Tags)
                .Include(e => e.Likes)
                .Include(e => e.User)
                .OrderBy(e => e.Likes.Count)
                .ToList()),

            _ => throw new ArgumentException(message: "not implemented request", paramName: nameof(sort))
        };

        private Task<List<Post>> ReadTimeSort(List<Post> postList, int min, int max)
        {

            max = max == 0 ? int.MaxValue : max;

            var orderedList = postList.OrderBy(e => e.ReadTime);

            var res = new List<Post>();

            foreach(var item in orderedList)
            {
                var readTime = item.ReadTime;
                if(readTime >= min && readTime <= max) res.Add(item);
            }
            return Task.FromResult(res);
        } 

        private Task<List<Post>> GetPagePost(List<Post> posts, int page, int postsInPage)
        {
            var postCount = posts.Count;

            var startIndex = (page - 1) * postsInPage;

            if(startIndex > postCount) return Task.FromResult(new List<Post>());

            var endIndex = startIndex + postsInPage;

            if(endIndex > postCount) endIndex = startIndex + (postCount % postsInPage);

            var res = new List<Post>();

            for (var i = startIndex; i < endIndex; i++)
            {
                res.Add(posts[i]);
            }

            return Task.FromResult(res);
        }
        
        private Task<List<Post>> SearchByAuthor(List<Post> posts, string author) => Task.FromResult(posts.Where(e => e.User.FullName == author).ToList());
    }
}