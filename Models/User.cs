using System.ComponentModel.DataAnnotations;

namespace blog_list_net_backend.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    //[Unique]??
    [Required]
    [MinLength(3)]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [Required]
    public string? Name { get; set; }

    public List<Blog>? Blogs { get; set; }
}
