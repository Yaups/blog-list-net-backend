using System.ComponentModel.DataAnnotations;

namespace blog_list_net_backend.Models;

public class Blog(string title, string url)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string? Title { get; set; } = title;
    public string? Author { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    public string? Url { get; set; } = url;

    public int Likes { get; set; } = 0;

    public List<Comment>? Comments { get; set; }
}
