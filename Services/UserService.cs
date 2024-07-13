using blog_list_net_backend.Contexts;
using blog_list_net_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_list_net_backend.Services;

public class UserService
{
    private readonly BlogContext _context;

    public UserService(BlogContext context)
    {
        _context = context;
    }

    public async Task<List<User>> FindAllAsync()
    {
        //include total number of blogs
        return await _context.Users
            .Include(u => u.Blogs)
            .ToListAsync();
    }

    public async Task<User?> FindOneAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Blogs)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task AssignBlogToUserAsync(Guid userId, Blog blog)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user is null || blog is null)
        {
            throw new InvalidOperationException("User or blog does not exist");
        }

        user.Blogs ??= new List<Blog>();
        user.Blogs.Add(blog);

        await _context.SaveChangesAsync();
    }
}