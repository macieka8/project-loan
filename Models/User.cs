using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Models;

public class User
{
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [InverseProperty("Borrower")]
    public ICollection<Loan>? BorrowedLoans { get; set; }

    [InverseProperty("Lender")]
    public ICollection<Loan>? LendedLoans { get; set; }
}
