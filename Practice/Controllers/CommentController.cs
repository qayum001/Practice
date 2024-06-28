using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.CommentService;
using Practice.Services.TokenService;
using System.Runtime.CompilerServices;

namespace Practice.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ITokenService _tokenService;
        public CommentController(ICommentService commentService, 
            ITokenService tokenService) 
        { 
            _commentService = commentService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Get all nested comments
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="404">Not Found</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpGet("{id}/tree")]
        public async Task<ActionResult<List<CommentDto>>> GetComment(Guid id) 
        {   
            var comments = await _commentService.GetComments(id);

            if(comments == null) { return NotFound(); }

            return comments;
        }

        /// <summary>
        /// Add a comment to a concrete post
        /// </summary>
        /// <param name="commentCreateDto"></param>
        /// <param name="id"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPost("{id}/comment")]
        [Authorize]
        public async Task<ActionResult<Response>> Comment([FromBody] CommentCreateDto commentCreateDto, Guid id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (!await _tokenService.IsTokenValid(token)) return Unauthorized();

            var userId = await _tokenService.GetGuid(token);

            var response = await _commentService.CommentPost(id, userId, commentCreateDto);

            return Ok(response);
        }

        /// <summary>
        /// Edit concrete post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editText"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Response>> EditComment(Guid id, string editText) 
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (!await _tokenService.IsTokenValid(token)) return Unauthorized();

            var userId = await _tokenService.GetGuid(token);

            var response = await _commentService.EditComment(id, userId, editText);

            return Ok(response);
        }

        /// <summary>
        /// Delete concrete comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Responce</returns>
        /// <response  code="200">Success</response>
        /// <response  code="400">Bad Request</response>
        /// <response  code="401">Unauthorized</response>
        /// <response  code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Response>> DeleteComment(Guid id) 
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (!await _tokenService.IsTokenValid(token)) return Unauthorized();

            var userId = await _tokenService.GetGuid(token);

            var response = await _commentService.DeleteComment(id, userId);

            return Ok(response);
        }
    }
}