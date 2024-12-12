using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.PostService;
using Practice.Services.TokenService;
using System.Net;

namespace Practice.Controllers
{

    [Route("api/post")]
    [ApiController]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ITokenService _tokenService;
        public PostController(IPostService postService,
            ITokenService tokenService) 
        { 
            _postService = postService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Get post list
        /// </summary>
        /// <returns>PostDto List</returns>
        /// <param name="tagIdlist">Search by TagList</param>
        /// <param name="authorName">Search by author name</param>
        /// <param name="minReadTime">Set min read time in minutes</param>
        /// <param name="maxReadTime">Set max read time in minutes</param>
        /// <param name="sort">Set sort type</param>
        /// <param name="page">Set page</param>
        /// <param name="postCount">Set posts count in page</param>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="404">Not Found</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPostsList(
            [FromQuery] List<Guid>? tagIdlist = null,
            string? authorName = null,
            int minReadTime = 0,
            int maxReadTime = 0,
            Sort sort = Sort.CreateAsc,
            int page = 1,
            int postCount = 5) 
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var pagination = new Pagination
            {
                TagGuidList = tagIdlist,//todo: add distinct
                AuthorName = authorName,
                MinReadTime = minReadTime,
                MaxReadTime = maxReadTime,
                Sort = sort,
                Page = page,
                PostCount = postCount
            };

            var res = await _postService.GetPostDtoList(pagination);

            if(res == null) return NotFound();

            return Ok(res);
        }

        /// <summary>
        /// Get post
        /// </summary>
        /// <returns>PostDto</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="404">Not Found</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetConcretePost(Guid id) 
        {
            var postDto = await _postService.GetPostDtoById(id);

            if (postDto == null) return NotFound(); 

            return Ok(postDto);
        }

        /// <summary>
        /// Like post
        /// </summary>
        /// <returns>PostDto</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="404">Not Found</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPost("{id}/like")]
        [Authorize]
        public async Task<ActionResult<Response>> Like(Guid id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (!await _tokenService.IsTokenValid(token)) return Unauthorized();

            var userId = await _tokenService.GetGuid(token);

            var response = await _postService.LikePost(userId, id);

            if(response == null) return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// DisLike post
        /// </summary>
        /// <returns>PostDto</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="404">Not Found</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpDelete("{id}/like")]
        [Authorize]
        public async Task<ActionResult<Response>> Unlike(Guid id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (!await _tokenService.IsTokenValid(token)) return Unauthorized();

            var userId = await _tokenService.GetGuid(token);

            var response = await _postService.DisLikePost(userId, id);

            if (response == null) return NotFound();

            return Ok(response);
        }
    }
}