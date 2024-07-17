namespace blog_list_net_backend.DTOs;

public class BlogWithoutUserDto(Guid id, string title, string? author, string url, int likes)
{
    public Guid Id { get; init; } = id;

    public string Title { get; init; } = title;
    public string? Author { get; init; } = author;

    public string Url { get; init; } = url;

    public int Likes { get; init; } = likes;
}

public class BlogWithUserDto(
    Guid id,
    string title,
    string? author,
    string url,
    int likes,
    UserWithoutBlogsDto user,
    List<CommentDto>? comments)
    : BlogWithoutUserDto(id, title, author, url, likes)
{
    public UserWithoutBlogsDto User { get; init; } = user;

    public List<CommentDto> Comments { get; init; } = comments ?? [];
}