using blog_list_net_backend.Contexts;
using blog_list_net_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_list_net_backend.Services;

public class BlogService
{
    private readonly BlogContext _context;

    public BlogService(BlogContext context)
    {
        _context = context;
    }

    public async Task<List<Blog>> FindAllAsync()
    {
        return await _context.Blogs
            .Include(b => b.User)
            .ToListAsync();
    }

    public async Task<Blog?> FindOneAsync(Guid id)
    {
        return await _context.Blogs
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);
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

    public async Task Update(Guid blogId, Blog updatedBlog)
    {
        var oldBlog = await _context.Blogs.FindAsync(blogId);

        if (oldBlog is null) throw new InvalidOperationException("Blog to update not found");

        oldBlog.Likes = updatedBlog.Likes;

        await _context.SaveChangesAsync();
    }
}
