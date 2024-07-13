using blog_list_net_backend.Models;
using blog_list_net_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("blogs")]
public class BlogsController(BlogService blogService, UserService userService) : ControllerBase
{
    private readonly BlogService _blogService = blogService;
    private readonly UserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blog>>> GetAll()
    {
        var blogs = await _blogService.FindAllAsync();

        var serializedBlogs = JsonSerializer.Serialize(blogs, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        });

        return Ok(serializedBlogs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Blog>> GetById(Guid id)
    {
        var blog = await _userService.FindOneAsync(id);

        if (blog is null) return NotFound();

        var serializedBlog = JsonSerializer.Serialize(blog, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        });

        return Ok(serializedBlog);
    }

    [HttpPost]
    public async Task<ActionResult<Blog>> PostBlog(Guid userId, Blog blog)
    {
        var user = await _userService.FindOneAsync(userId);

        if (user is null) return Unauthorized("You must be logged in to post a blog!");

        var postedBlog = await _blogService.CreateAsync(blog, userId);

        if (postedBlog is null) return BadRequest("Malformatted blog");

        await _userService.AssignBlogToUserAsync(userId, postedBlog);

        return CreatedAtAction(nameof(GetById), new { id = postedBlog.Id }, postedBlog);
    }

    [HttpPost("{id}/comments")]
    public async Task<ActionResult<Comment>> PostComment(Guid userId, Guid blogId, Comment comment)
    {
        var user = await _userService.FindOneAsync(userId);

        if (user is null) return Unauthorized();

        var blog = await _blogService.FindOneAsync(blogId);

        if (blog is null) return NotFound();

        var postedComment = await _blogService.PostCommentAsync();

        if (postedComment is null) return BadRequest();

        return Ok(postedComment);
    }

    [HttpPut("{id}")]
    public IActionResult Modify(int id, Blog updatedBlog)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }
}
