using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    [Route("api/author")]
    [ApiController]

    public class AuthorController : ControllerBase
    {
        public AuthorController() { }

        [HttpGet("list")]
        public async Task AuthorsList() { }

        [HttpGet("{id}")]
        public async Task GetConcreteAuthor() { }

        [HttpPost("post")]
        public async Task CreatePost() { }

        [HttpPut("post")]
        public async Task EditePost() { }

        [HttpDelete("post")]
        public async Task DeletePost() { }
    }
}