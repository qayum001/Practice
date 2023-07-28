using Microsoft.AspNetCore.Mvc;
using Practice.Data.Dto;
using Practice.Services.PostService;

namespace Practice.Controllers
{

    [Route("api/post")]
    [ApiController]

    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) 
        { 
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPostsList() 
        {
            var res = await _postService.GetPostDtoList();

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task GetConcretePost(Guid id) { }

        [HttpPost("{id}/like")]
        public async Task Like(Guid id) { }

        [HttpDelete("{id}/like")]
        public async Task Unlike(Guid id) { }
    }
}