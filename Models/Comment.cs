using System.ComponentModel.DataAnnotations;

namespace blog_list_net_backend.Models;

public class Comment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string? Text { get; set; }
}
