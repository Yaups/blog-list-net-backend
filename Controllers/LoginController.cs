using blog_list_net_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    public LoginController()
    {
    }

    [HttpPost]
    public User Post()
    {
        throw new NotImplementedException();
    }
}
