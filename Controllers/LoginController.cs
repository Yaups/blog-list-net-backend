using blog_list_net_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("login")]
public class LoginController() : ControllerBase
{
    [HttpPost]
    public User Login()
    {
        throw new NotImplementedException();
    }
}
