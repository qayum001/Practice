using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public CommentController() { }

        [HttpGet("{id:guid}/tree")]
        public async Task GetComment(Guid id) {}

        [HttpPost("{id:guid}/comment")]
        public async Task Comment(Guid id) { }

        [HttpPut("{id:guid}")]
        public async Task EditComment(Guid id) { }

        [HttpDelete("{id:guid}")]
        public async Task DeleteComment(Guid id) { }
    }
}
