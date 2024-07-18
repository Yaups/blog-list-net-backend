namespace blog_list_net_backend.DTOs;

public class NewUserDto(string username, string password, string name)
{
    public string Username { get; init; } = username;

    public string Password { get; init; } = password;

    public string Name { get; init; } = name;
}

public class BaseUserDto(Guid id, string username)
{
    public Guid Id { get; init; } = id;

    public string Username { get; init; } = username;
}

public class UserForLoginDto(Guid id, string username, string passwordHash) : BaseUserDto(id, username)
{
    public string PasswordHash { get; init; } = passwordHash;
}

public class UserWithoutBlogsDto(Guid id, string username, string name) : BaseUserDto(id, username)
{
    public string Name { get; init; } = name;
}

public class UserWithBlogsDto
    (Guid id,
    string username,
    string name,
    List<BlogWithoutUserDto>? blogs)
    : BaseUserDto(id, username)
{
    public string Name { get; init; } = name;

    public List<BlogWithoutUserDto> Blogs { get; init; } = blogs ?? [];
}
