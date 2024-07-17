using blog_list_net_backend.DTOs;
using blog_list_net_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserService service) : ControllerBase
{
    private readonly UserService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserWithBlogsDto>>> GetAll()
    {
        var users = await _service.FindAllAsync();

        return users;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithBlogsDto>> GetById(Guid id)
    {
        var user = await _service.FindOneAsync(id);

        if (user is null) return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<UserWithoutBlogsDto>> Post(NewUserDto user)
    {
        try
        {
            var postedUser = await _service.CreateAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = postedUser.Id }, postedUser);
        }
        catch
        {
            return BadRequest();
        }
    }
}
