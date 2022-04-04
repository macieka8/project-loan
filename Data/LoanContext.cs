using Microsoft.EntityFrameworkCore;
using Base.Models;

namespace Base.Data;

public class LoanContext : DbContext
{
    public LoanContext (DbContextOptions<LoanContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Loan> Loans => Set<Loan>();
}
