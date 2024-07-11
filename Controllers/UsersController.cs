using Microsoft.AspNetCore.Mvc;
using blog_list_net_backend.Models;


namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    public UsersController()
    {
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        throw new NotImplementedException();
    }
}
