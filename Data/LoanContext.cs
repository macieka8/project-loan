using Microsoft.EntityFrameworkCore;
using Base.Models;

namespace Base.Data;

public class LoanContext : DbContext
{
    public LoanContext (DbContextOptions<LoanContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<LoanModel> Loans => Set<LoanModel>();
}
