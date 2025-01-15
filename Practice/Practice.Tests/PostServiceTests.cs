using Microsoft.Extensions.DependencyInjection;
using Practice.Data;
using Practice.Data.Model;
using Practice.Services.PostService;
using Practice.Tests.Helpers;

namespace Practice.Tests
{
    [TestClass]
    public class PostServiceTests
    {
        #region GetPostDtoById()
        //Bad Date Test
        [TestMethod]
        public async Task GetPostDtoById_PostWithoutTagsAndComments_ReturnsPostWithEmptyCollections()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = new User { Id = userId, FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" },
                Text = "test",
            };

            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.GetPostDtoById(postId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(postId, result.PostId);
            Assert.AreEqual(0, post.Likes.Count);
            Assert.AreEqual(0, result.TagList.Count);
            Assert.AreEqual(0, result.CommentList.Count);
        }

        // Bad Data Test
        [TestMethod]
        public async Task GetPostDtoById_NonexistentPostId_ReturnsNull()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = new User { Id = userId, FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" },
                Text = "test",
            };

            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            //calling for not existing data
            var result = await postService.GetPostDtoById(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);
        }


        //Good Data Test
        [TestMethod]
        public async Task GetPostDtoById_ValidPostIdWithoutComments_ReturnsPostWithoutComments()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = new User { Id = userId, FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" },
                Text = "test",
            };

            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.GetPostDtoById(postId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(postId, result.PostId);
            Assert.AreEqual(0, result.CommentList.Count);
        }

        //Good Data Test
        [TestMethod]
        public async Task GetPostDtoById_ValidPostIdWithComments_ReturnsPostWithComments()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = new User { Id = userId, FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" },
                Text = "test",
            };

            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.GetPostDtoById(postId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(postId, result.PostId);
        }

        //structured basis testing
        [TestMethod]
        public async Task GetPostDtoById_PostExistsWithAllData_ReturnsPostWithComments()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var tagId = Guid.NewGuid();
            var commentId = Guid.NewGuid();

            // Setup data
            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                Tags = new List<Tag> { new Tag { Id = tagId, Name = "Test Tag" } },
                User = new User { Id = userId, FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" },
                Text = "test",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = commentId,
                        Text = "Test Comment",
                        ParentCommentId = null,
                        ChildComments = new List<ChildCommentId>
                        {
                            new ChildCommentId { Id = Guid.NewGuid(), ParentId = commentId, CommentId = Guid.NewGuid() },
                            new ChildCommentId { Id = Guid.NewGuid(), ParentId = commentId, CommentId = Guid.NewGuid() }
                        },
                        AuthorName = "name"
                    }
                },
            };

            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.GetPostDtoById(postId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(postId, result.PostId);
            Assert.AreEqual(1, result.TagList.Count);
            Assert.AreEqual(1, result.CommentList.Count);
            Assert.AreEqual(2, result.CommentList.First().SubCommentsCount);
        }

        //structured basis testing
        [TestMethod]
        public async Task GetPostDtoById_PostExistsWithoutTagsOrComments_ReturnsPostWithEmptyLists()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = new User { Id = userId, FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" },
                Text = "test",
            };

            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.GetPostDtoById(postId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(postId, result.PostId);
            Assert.AreEqual(0, result.TagList.Count);
            Assert.AreEqual(0, result.CommentList.Count);
        }

        //structured basis testing
        [TestMethod]
        public async Task GetPostDtoById_PostDoesNotExist_ReturnsNull()
        {
            // Arrange
            var postId = Guid.NewGuid();

            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            // Act
            var result = await postService.GetPostDtoById(postId);

            // Assert
            Assert.IsNull(result);
        }
        #endregion

        #region GetPostDtoList()
        //structured basis testing
        [TestMethod]
        public async Task GetPostDtoList_GetPostsWithGivenTags_PostsWithTagsReturned()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = "Fullname",
                Password = "password",
                Email = "Email",
                PhoneNumber = "1234567890",
            };

            context.User.Add(user);
            await context.SaveChangesAsync();

            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = "test",
                CreatedTime = DateTime.Now,
            };

            var posts = new List<Post>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Tags = [tag],
                    Text = "d",
                    Title = "test",
                    User = user
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "f",
                    Title = "test",
                    User = user
                }
            };

            context.Post.AddRange(posts);
            await context.SaveChangesAsync();

            var pgn = new Pagination()
            {
                TagGuidList = [tag.Id]
            };

            // Act
            var res = await postService.GetPostDtoList(pgn);

            // Assert
            Assert.IsTrue(res?.TrueForAll(e => e.TagList.Any(e => e.Id == tag.Id)));
        }

        [TestMethod]
        public async Task GetPostDtoList_GetPostViaAuthorName_ReturnsListOfAuthorPost()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = "Fullname",
                Password = "password",
                Email = "Email",
                PhoneNumber = "1234567890",
            };

            var user2 = new User()
            {
                Id = Guid.NewGuid(),
                FullName = "asd",
                Password = "password",
                Email = "Email",
                PhoneNumber = "1234567890",
            };

            context.User.Add(user);
            await context.SaveChangesAsync();

            var posts = new List<Post>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "d",
                    Title = "test",
                    User = user
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "f",
                    Title = "test",
                    User = user2
                }
            };

            context.Post.AddRange(posts);
            await context.SaveChangesAsync();

            var pgn = new Pagination()
            {
                AuthorName = user.FullName,
            };

            // Act
            var res = await postService.GetPostDtoList(pgn);

            // Assert
            Assert.AreEqual(1, res?.Count);
        }

        [TestMethod]
        public async Task GetPostDtoList_GetPostsWithValidReadtime_ReturnsListOfPostWithValidReadTime()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = "Fullname",
                Password = "password",
                Email = "Email",
                PhoneNumber = "1234567890"
            };

            var posts = new List<Post>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "d",
                    Title = "test",
                    User = user,
                    ReadTime = 10
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "f",
                    Title = "test",
                    User = user,
                    ReadTime = 4
                }
            };

            context.Post.AddRange(posts);
            await context.SaveChangesAsync();

            var pgn = new Pagination()
            {
                MaxReadTime = 6,
            };

            //Act
            var res = await postService.GetPostDtoList(pgn);

            //Assert
            Assert.AreEqual(1, res?.Count);
        }

        [TestMethod]
        public async Task GetPostDtoList_DescSortByLikesCount_ReturnsDescSortedPostList()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var postService = serviceProvider.GetService<IPostService>()
                ?? throw new NullReferenceException();

            var context = serviceProvider.GetService<AppDbContext>()
                ?? throw new NullReferenceException();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = "Fullname",
                Password = "password",
                Email = "Email",
                PhoneNumber = "1234567890"
            };

            var posts = new List<Post>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "d",
                    Title = "test",
                    User = user,
                    Likes = [
                        new Like() { Id = Guid.NewGuid() },
                            ]
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "f",
                    Title = "test",
                    User = user,
                    Likes = [
                        new Like() { Id = Guid.NewGuid() },
                        new Like() { Id = Guid.NewGuid() }
                            ]
                }
            };

            context.Post.AddRange(posts);
            await context.SaveChangesAsync();

            var pgn = new Pagination()
            {
                Sort = Sort.LikeDesc,
            };

            //Act
            var res = await postService.GetPostDtoList(pgn);

            //Assert
            var isDesc = true;
            var lastNum = int.MaxValue;

            foreach(var post in res)
            {
                if (post.LikesCount <= lastNum)
                {
                    lastNum = post.LikesCount;
                    continue;
                }

                isDesc = false;
                break;
            }
            
            Assert.IsTrue(isDesc);
        }
        #endregion

        #region LikePost()
        //structured basis testing
        [TestMethod]
        public async Task LikePost_PostNotFound_ReturnsFailResponse()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();
            var postService = serviceProvider.GetService<IPostService>() ?? throw new NullReferenceException();
            var nonExistentPostId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var result = await postService.LikePost(userId, nonExistentPostId);

            // Assert
            Assert.AreEqual("Fail", result.Stasus);
            Assert.AreEqual("Post not found", result.Message);
        }

        //structured basis testing
        [TestMethod]
        public async Task LikePost_AlreadyLikedPost_ReturnsFailResponse()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();
            var postService = serviceProvider.GetService<IPostService>() ?? throw new NullReferenceException();
            var context = serviceProvider.GetService<AppDbContext>() ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid(), FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" };
            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = user,
                Text = "test",
            };
            var like = new Like { UserId = user.Id, PostId = post.Id, User = user, Post = post };

            context.Post.Add(post);
            context.Like.Add(like);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.LikePost(user.Id, post.Id);

            // Assert
            Assert.AreEqual("Fail", result.Stasus);
            Assert.AreEqual("Already liked", result.Message);
        }

        //structured basis testing
        [TestMethod]
        public async Task LikePost_ValidLike_ReturnsSuccessResponse()
        {
            // Arrange
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();
            var postService = serviceProvider.GetService<IPostService>() ?? throw new NullReferenceException();
            var context = serviceProvider.GetService<AppDbContext>() ?? throw new NullReferenceException();

            var postId = Guid.NewGuid();
            var user = new User { Id = Guid.NewGuid(), FullName = "Test User", Password = "appa", Email = "Ad", PhoneNumber = "dd" };
            var post = new Post
            {
                Id = postId,
                Title = "Test Post",
                User = user,
                Text = "test",
            };

            context.User.Add(user);
            context.Post.Add(post);
            await context.SaveChangesAsync();

            // Act
            var result = await postService.LikePost(user.Id, post.Id);

            // Assert
            Assert.AreEqual("Success", result.Stasus);
            Assert.AreEqual("Post has been liked", result.Message);
        }
        #endregion
    }
}