namespace blog_list_net_backend.DTOs
{
    public class LoginResponseDto(string token, string username, string name)
    {
        public string Token { get; set; } = token;

        public string Username { get; set; } = username;

        public string Name { get; set; } = name;
    }
}
