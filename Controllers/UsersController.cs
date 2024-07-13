using blog_list_net_backend.Models;
using blog_list_net_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("users")]
public class UsersController(UserService service) : ControllerBase
{
    private readonly UserService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _service.FindAllAsync();

        var serializedUsers = JsonSerializer.Serialize(users, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        });

        return Ok(serializedUsers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var user = await _service.FindOneAsync(id);

        if (user is null) return NotFound();

        var serializedUser = JsonSerializer.Serialize(user, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        });

        return Ok(serializedUser);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
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
