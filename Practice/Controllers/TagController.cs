using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    [Route("api/tag")]
    [ApiController]

    public class TagController : ControllerBase
    {
        public TagController() { }

        [HttpGet]
        public async Task GetTag() { }
    }
}
