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
        var user = await _service.FindByIdAsync(id);

        if (user is null) return NotFound();

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<UserWithoutBlogsDto>> Post(NewUserDto user)
    {
        bool usernameIsUnique = await _service.IsUniqueUsername(user.Username);
        if (!usernameIsUnique) return BadRequest("Username already exists");

        var postedUser = await _service.CreateAsync(user);

        if (postedUser is null) return BadRequest("Error creating user: problem with formatting");

        return CreatedAtAction(nameof(GetById), new { id = postedUser.Id }, postedUser);

    }
}
