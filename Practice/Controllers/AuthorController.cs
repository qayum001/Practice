using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.AuthorService;
using Practice.Services.TokenService;
using System.Runtime.CompilerServices;

namespace Practice.Controllers
{
    [Route("api/author")]
    [ApiController]

    public class AuthorController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthorService _authorService;

        public AuthorController(ITokenService tokenService,
            IAuthorService authorService)
        {
            _tokenService = tokenService;
            _authorService = authorService;
        }

        /// <summary>
        /// Get Author List
        /// </summary>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet("list")]
        public async Task<ActionResult<List<AuthorDto>>> AuthorsList() 
        {
            var autorDtoList = await _authorService.GetAuthorList();

            return autorDtoList;
        }

        /// <summary>
        /// Get author by Id
        /// </summary>
        /// <returns>AuthorDto</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetConcreteAuthor(Guid id) 
        {
            var author = await _authorService.GetAuthorDto(id);

            if (author == null) { return NotFound(); }

            return Ok(author);
        }

        /// <summary>
        /// Crete New Post
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPost("post")]
        [Authorize]
        public async Task<ActionResult<Response>> CreatePost([FromBody] PostCreateDto postDto)  
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var isTokenValid = await _tokenService.IsTokenValid(token);
            if (!isTokenValid) return StatusCode(401);

            if (!ModelState.IsValid) return StatusCode(400);

            var userId = await _tokenService.GetGuid(token);

            var response = await _authorService.CreatePost(userId, postDto);

            return Ok(response);
        }

        /// <summary>
        /// Edite post
        /// </summary>
        /// <param name="postEditDto"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPut("post")]
        [Authorize]
        public async Task<ActionResult<Response>> EditePost([FromBody] PostEditDto postEditDto) 
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var isValid = await _tokenService.IsTokenValid(token);

            if (!isValid) return StatusCode(401);

            var userId = await _tokenService.GetGuid(token);

            var response = await _authorService.EditPost(userId, postEditDto);

            return Ok(response);
        }

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpDelete("post")]
        [Authorize]
        public async Task<ActionResult<Response>> DeletePost(Guid postId) 
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var isValid = await _tokenService.IsTokenValid(token);

            if (!isValid) return StatusCode(401);
            if (!ModelState.IsValid) return BadRequest();

            var userId = await _tokenService.GetGuid(token);

            var responce = await _authorService.DeletePost(userId, postId);

            return Ok(responce);
        }
    }
}