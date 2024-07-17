using blog_list_net_backend.DTOs;
using blog_list_net_backend.Models;
using blog_list_net_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers;

[ApiController]
[Route("api/blogs")]
public class BlogsController(BlogService blogService, UserService userService) : ControllerBase
{
    private readonly BlogService _blogService = blogService;
    private readonly UserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogWithUserDto>>> GetAll()
    {
        var blogs = await _blogService.FindAllAsync();

        return blogs;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogWithUserDto>> GetById(Guid id)
    {
        var blog = await _blogService.FindOneAsync(id);

        if (blog is null) return NotFound();

        return blog;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BlogWithoutUserDto>> PostBlog(Blog blog)
    {
        var currentUser = HttpContext.User;

        string? userIdString = currentUser.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        bool isValidGuid = Guid.TryParse(userIdString, out Guid userId);
        if (!isValidGuid) return Unauthorized("You are not logged in.");

        var user = await _userService.FindOneAsync(userId);

        if (user is null) return Unauthorized("You must be logged in to post a blog!");

        var postedBlog = await _blogService.CreateAsync(blog, userId);

        if (postedBlog is null) return BadRequest("Malformatted blog");

        await _userService.AssignBlogToUserAsync(userId, postedBlog);

        return CreatedAtAction(nameof(GetById), new { id = postedBlog.Id }, postedBlog);
    }

    [Authorize]
    [HttpPost("{blogId}/comments")]
    public async Task<ActionResult<Comment>> PostComment(Guid blogId, Comment comment)
    {
        var currentUser = HttpContext.User;

        string? userIdString = currentUser.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        bool isValidGuid = Guid.TryParse(userIdString, out Guid userId);
        if (!isValidGuid) return Unauthorized("You are not logged in.");

        var user = await _userService.FindOneAsync(userId);
        if (user is null) return Unauthorized();

        var blog = await _blogService.FindOneAsync(blogId);
        if (blog is null) return NotFound();

        var postedComment = await _blogService.PostCommentAsync(blogId, comment);
        if (postedComment is null) return BadRequest();

        return postedComment;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyLikes(Guid id)
    {
        var currentUser = HttpContext.User;

        string? userIdString = currentUser.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        bool isValidGuid = Guid.TryParse(userIdString, out Guid userId);
        if (!isValidGuid) return Unauthorized("You are not logged in.");

        var user = await _userService.FindOneAsync(userId);
        if (user is null) return Unauthorized();

        //CAN ONLY LIKE IF YOU HAVE NOT LIKED THE BLOG ALREADY?

        var successfulBlog = await _blogService.IncreaseLikesByOneAsync(id);

        if (successfulBlog is null) return NotFound("Blog to update does not exist");

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var currentUser = HttpContext.User;

        string? userIdString = currentUser.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        bool isValidGuid = Guid.TryParse(userIdString, out Guid userId);
        if (!isValidGuid) return Unauthorized("You are not logged in.");

        var user = await _userService.FindOneAsync(userId);
        if (user is null) return Unauthorized();

        var blogToDelete = await _blogService.FindOneAsync(id);
        if (blogToDelete is null) return NotFound();

        if (blogToDelete.User.Id != user.Id) return Unauthorized("Only the blog poster can delete their blog.");

        await _blogService.DeleteBlogByIdAsync(id);

        return NoContent();
    }
}
