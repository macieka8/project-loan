using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Base.Models;

public class Loan
{
    public long Id { get; set; }

    public decimal Amount { get; set; }

    [Required]
    [MaxLength(3)]
    public string? Currency { get; set; }

    [Required]
    public long BorrowerUserId { get; set; }

    [JsonIgnore]
    [ForeignKey("BorrowerUserId")]
    public User? Borrower { get; set; }

    [Required]
    public long LenderUserId { get; set; }

    [JsonIgnore]
    [ForeignKey("LenderUserId")]
    public User? Lender { get; set; }
}
