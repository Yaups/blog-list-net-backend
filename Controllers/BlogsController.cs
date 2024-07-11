using Microsoft.AspNetCore.Mvc;
using blog_list_net_backend.Models;
using blog_list_net_backend.Services;

namespace blog_list_net_backend.Controllers
{
    [ApiController]
    [Route("blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogService _service;
        public BlogsController(BlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> Get()
        {
            return _service.GetAll();
        }
    }
}
