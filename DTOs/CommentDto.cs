namespace blog_list_net_backend.DTOs;

public class CommentDto(Guid id, string text)
{
    public Guid Id { get; init; } = id;

    public string Text { get; init; } = text;
}
