using BCrypt.Net;
using blog_list_net_backend.Contexts;
using blog_list_net_backend.DTOs;
using blog_list_net_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_list_net_backend.Services;

public class UserService(BlogContext context)
{
    private readonly BlogContext _context = context;

    public async Task<List<UserWithBlogsDto>> FindAllAsync()
    {
        //include total number of blogs instead of all blogs
        return await _context.Users
            .Include(u => u.Blogs)
            .Select(u => new UserWithBlogsDto
            (
                u.Id,
                u.Username!,
                u.Name!,
                u.Blogs!.Select(b => new BlogWithoutUserDto
                (
                    b.Id,
                    b.Title!,
                    b.Author,
                    b.Url!,
                    b.Likes
                )
            ).ToList()
            ))
            .ToListAsync();
    }

    public async Task<UserWithBlogsDto?> FindOneAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Blogs)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) return null;

        return new UserWithBlogsDto
            (
                user.Id,
                user.Username!,
                user.Name!,
                user.Blogs!.Select(b => new BlogWithoutUserDto
                (
                    b.Id,
                    b.Title!,
                    b.Author,
                    b.Url!,
                    b.Likes
                )).ToList()
            );
    }

    public async Task<UserWithoutBlogsDto> CreateAsync(NewUserDto newUser)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        var userWithHashedPassword = new User
        {
            Username = newUser.Username,
            PasswordHash = passwordHash,
            Name = newUser.Name,
        };

        await _context.Users.AddAsync(userWithHashedPassword);
        await _context.SaveChangesAsync();

        return new UserWithoutBlogsDto(userWithHashedPassword.Id, newUser.Username, newUser.Name);
    }

    public async Task AssignBlogToUserAsync(Guid userId, Blog blog)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user is null || blog is null)
        {
            throw new InvalidOperationException("User or blog does not exist");
        }

        user.Blogs ??= [];
        user.Blogs.Add(blog);

        await _context.SaveChangesAsync();
    }
}