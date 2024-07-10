using System.ComponentModel.DataAnnotations;

namespace blog_list_net_backend.Models;

public class Blog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string? Title { get; set; }
    public string? Author { get; set; }

    public int UserId { get; set; }

    [Required]
    public string? Url { get; set; }

    public int Likes { get; set; } = 0;

    public List<Comment>? Comments { get; set; }
}

public class Comment
{
    public int Text { get; set; }
    public int CommentId { get; set; }
}
