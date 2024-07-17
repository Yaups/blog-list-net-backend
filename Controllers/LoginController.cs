using blog_list_net_backend.DTOs;
using blog_list_net_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController(LoginService service) : ControllerBase
{
    private readonly LoginService _service = service;

    [HttpPost]
    public async Task<ActionResult<LoginResponseDto>> Login(string username, string password)
    {
        if (username is null) return BadRequest("Username not given");
        if (password is null) return BadRequest("Password not given");

        var userInfo = await _service.Login(username, password);
        if (userInfo is null) return BadRequest("Invalid username or password");

        return userInfo;
    }
}