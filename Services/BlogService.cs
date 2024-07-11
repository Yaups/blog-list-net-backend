using blog_list_net_backend.Contexts;
using blog_list_net_backend.Models;

namespace blog_list_net_backend.Services;

public class BlogService
{
    /*
    private readonly BlogContext _context;

    public BlogService(BlogContext context)
    { 
        _context = context;
    }
    */

    private readonly List<Blog> TempBlogs = new List<Blog> { new Blog("Yo", "Tom A"), new Blog("Oi", "Tom B") };

    public List<Blog> GetAll()
    {
        return TempBlogs;
    }
}
