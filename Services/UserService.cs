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
