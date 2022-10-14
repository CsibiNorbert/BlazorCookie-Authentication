using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCookie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // this ensures that this controller can be accessed only by authenticated users
    public class DummyController : ControllerBase
    {
        public DummyController()
        {

        }

        [HttpGet]
        public async Task<string[]> DummyGet()
        {
            return (new string[] { "dummy1", "dummy2"});
        }
    }
}
