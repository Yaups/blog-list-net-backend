using Microsoft.EntityFrameworkCore;
using blog_list_net_backend.Models;

namespace blog_list_net_backend.Contexts;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options) 
    { }

    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<User> Users => Set<User>();
}
