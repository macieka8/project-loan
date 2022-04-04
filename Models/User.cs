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
    [MaxLength(256)]
    public string? PasswordHash { get; set; }
    public string? Salt { get; set; }

    [InverseProperty("Borrower")]
    public List<Loan>? BorrowedLoans { get; set; }

    [InverseProperty("Lender")]
    public List<Loan>? LendedLoans { get; set; }
}