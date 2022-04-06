using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Models;

public class UserModel
{
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [InverseProperty("Borrower")]
    public ICollection<LoanModel>? BorrowedLoans { get; set; }

    [InverseProperty("Lender")]
    public ICollection<LoanModel>? LendedLoans { get; set; }
}
