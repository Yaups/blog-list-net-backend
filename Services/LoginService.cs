using blog_list_net_backend.Contexts;
using blog_list_net_backend.DTOs;
using blog_list_net_backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace blog_list_net_backend.Services;

public class LoginService(BlogContext context)
{
    private readonly BlogContext _context = context;

    public async Task<LoginResponseDto?> Login(string username, string password)
    {
        if (username is null) throw new ArgumentNullException(nameof(username));
        if (password is null) throw new ArgumentNullException(nameof(password));

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user is null) return null;

        bool passwordCorrect = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordCorrect) return null;

        if (user.Name is null) throw new ArgumentNullException(nameof(user.Name));

        var userForToken = new UserWithoutBlogsDto(user.Id, username, user.Name);

        string token = AuthHelpers.GenerateJWTToken(userForToken);

        return new LoginResponseDto(token, username, user.Name);
    }
}
