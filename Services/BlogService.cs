using blog_list_net_backend.Contexts;
using blog_list_net_backend.DTOs;
using blog_list_net_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace blog_list_net_backend.Services;

public class BlogService(BlogContext context)
{
    private readonly BlogContext _context = context;

    public async Task<List<BlogWithUserDto>> FindAllAsync()
    {
        return await _context.Blogs
            .Include(b => b.User)
            .Select(b => new BlogWithUserDto
            (
                b.Id,
                b.Title!,
                b.Author,
                b.Url!,
                b.Likes,
                new UserWithoutBlogsDto
                (
                    b.User!.Id,
                    b.User.Username!,
                    b.User.Name!
                ),
                new List<CommentDto>()
            ))
            .ToListAsync();
    }

    public async Task<BlogWithUserDto?> FindOneAsync(Guid id)
    {
        var blog = await _context.Blogs
            .Include(b => b.User)
            .Include(b => b.Comments)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (blog is null) return null;

        var blogDto = new BlogWithUserDto
            (
                blog.Id,
                blog.Title!,
                blog.Author,
                blog.Url!,
                blog.Likes,
                new UserWithoutBlogsDto
                (
                    blog.User!.Id,
                    blog.User.Username!,
                    blog.User.Name!
                ),
                blog.Comments?.Select(c => new CommentDto
                (
                    c.Id!,
                    c.Text!
                )).ToList()
            );

        return blogDto;
    }

    public async Task<Blog?> CreateAsync(Blog blog, Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user is null)
        {
            throw new InvalidOperationException("Error creating blog: User cannot be found");
        }

        if (blog is null)
        {
            throw new InvalidOperationException("Error creating blog: Blog is corrupted");
        }

        blog.User = user;

        try
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Blog?> IncreaseLikesByOneAsync(Guid blogId)
    {
        var blog = await _context.Blogs.FindAsync(blogId);

        if (blog is null) return null;

        blog.Likes += 1;
        await _context.SaveChangesAsync();

        return blog;
    }

    public async Task<Comment?> PostCommentAsync(Guid blogId, Comment comment)
    {
        var blog = await _context.Blogs.FindAsync(blogId);

        if (blog is null) throw new InvalidOperationException("Error posting comment: blog not found");

        try
        {
            await _context.Comments.AddAsync(comment);
            blog.Comments ??= [];
            blog.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return null;
        }

        return comment;
    }

    public async Task DeleteBlogByIdAsync(Guid blogId)
    {
        var blogToDelete = await _context.Blogs.FindAsync(blogId);

        if (blogToDelete is null) throw new InvalidOperationException("Error deleting blog: Blog not found");

        _context.Blogs.Remove(blogToDelete);

        await _context.SaveChangesAsync();
    }
}
