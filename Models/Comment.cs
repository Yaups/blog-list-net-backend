using System.ComponentModel.DataAnnotations;

namespace blog_list_net_backend.Models;

public class Comment
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public int Text { get; set; }
}
