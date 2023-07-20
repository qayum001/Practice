using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{

    [Route("api/post")]
    [ApiController]

    public class PostController : ControllerBase
    {
        public PostController() { }

        [HttpGet]
        public async Task GetPostsList() { }

        [HttpGet("{id:guid}")]
        public async Task GetConcretePost(Guid id) { }

        [HttpPost("{id:guid}/like")]
        public async Task Like(Guid id) { }

        [HttpDelete("{id:guid}/like")]
        public async Task Unlike(Guid id) { }
    }
}
