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

        [HttpGet("{id}")]
        public async Task GetConcretePost(Guid id) { }

        [HttpPost("{id}/like")]
        public async Task Like(Guid id) { }

        [HttpDelete("{id}/like")]
        public async Task Unlike(Guid id) { }
    }
}