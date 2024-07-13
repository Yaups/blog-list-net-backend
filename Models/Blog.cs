using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace blog_list_net_backend.Models;

public class Blog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string? Title { get; set; }
    public string? Author { get; set; }

    [Required]
    public string? Url { get; set; }

    public int Likes { get; set; } = 0;

    public List<Comment>? Comments { get; set; }

    public User? User { get; set; }
}
