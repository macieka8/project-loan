using Base.Models;
using Base.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Base.Services;

public class AuthenticationService : IAuthenticationService
{
    readonly LoanContext _context;
    readonly IConfiguration _configuration;

    public AuthenticationService(LoanContext loanContext, IConfiguration configuration)
    {
        _context = loanContext;
        _configuration = configuration;
    }

    public (bool success, string error, UserModel? user) Register(string username, string password)
    {
        // Check if username is available
        if (_context.Users.Any(u => u.Username == username)) return (false, "Username already used", null);

        var newUser = new UserModel
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            BorrowedLoans = new List<LoanModel>(),
            LendedLoans = new List<LoanModel>(),
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return (true, "", newUser);
    }

    public (bool success, string token) Login(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);
        if (user is null) return (false, "Invalid username");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return (false, "Invalid password");

        var jwt = CreateJwt(user);

        return (true, jwt);
    }

    string CreateJwt(UserModel user)
    {
        var claims = new Claim[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("name", user.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("Settings:Jwt").Value
        ));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
