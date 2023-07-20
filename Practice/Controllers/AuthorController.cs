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
        public async Task AuthorsList()
        {

        }

    }
}
