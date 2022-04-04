using Base.Models;
using Base.Data;
using Microsoft.EntityFrameworkCore;

namespace Base.Services;

public class UserService
{
    readonly LoanContext _context;

    public UserService(LoanContext userContext)
    {
        _context = userContext;
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
}
