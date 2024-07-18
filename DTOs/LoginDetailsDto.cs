namespace blog_list_net_backend.DTOs;

public class LoginRequestDto(string username, string password)
{
    public string Username { get; init; } = username;

    public string Password { get; init; } = password;
}
public class LoginResponseDto(string token, string username, string name)
{
    public string Token { get; init; } = token;

    public string Username { get; init; } = username;

    public string Name { get; init; } = name;
}
