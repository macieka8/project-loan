using Base.Models;
using Base.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Base.Services;

public class UserService
{
    readonly LoanContext _context;
    readonly IConfiguration _configuration;

    public UserService(LoanContext userContext, IConfiguration configuration)
    {
        _context = userContext;
        _configuration = configuration;
    }

    public User? GetById(long id)
    {
        return _context.Users
            .Include(u => u.BorrowedLoans)
            .Include(u => u.LendedLoans)
            .AsNoTracking()
            .SingleOrDefault(u => u.Id == id);
    }

    public User? Register(string username, string password)
    {
        // Check if username is available
        if (_context.Users.Any(u => u.Username == username)) return null;

        var newUser = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            BorrowedLoans = new List<Loan>(),
            LendedLoans = new List<Loan>(),
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }

    public void DeleteById(long id)
    {
        var userToDelete = _context.Users.Find(id);
        if (userToDelete is not null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }

    public (bool success, string token) Login(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);
        if (user is null) return (false, "Invalid username");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return (false, "Invalid password");

        var jwt = CreateJwt(user);

        return (true, jwt);
    }

    string CreateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Settings:Jwt").Value
        ));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
