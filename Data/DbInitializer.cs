using Base.Models;

namespace Base.Data;

public static class DbInitializer
{
    public static void Initialize(LoanContext context)
    {
        if (context.Users.Any() && context.Loans.Any())
        {
            return;   // DB has been seeded
        }

        var firstUser = new UserModel
        {
            Username = "FirstUser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"),
            BorrowedLoans = new List<LoanModel>(),
            LendedLoans = new List<LoanModel>()
        };

        var secondUser = new UserModel
        {
            Username = "SecondUser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("asdf"),
            BorrowedLoans = new List<LoanModel>(),
            LendedLoans = new List<LoanModel>()
        };

        var thirdUser = new UserModel
        {
            Username = "ThirdUser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
            BorrowedLoans = new List<LoanModel>(),
            LendedLoans = new List<LoanModel>()
        };

        var fourthUser = new UserModel
        {
            Username = "FourthUser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("qwer"),
            BorrowedLoans = new List<LoanModel>(),
            LendedLoans = new List<LoanModel>()
        };


        var loan1 = new LoanModel{ Amount = 100, Currency = "PLN", Borrower = firstUser, Lender = secondUser };
        var loan2 = new LoanModel{ Amount = 2000, Currency = "PLN", Borrower = thirdUser, Lender = secondUser };
        var loan3 = new LoanModel{ Amount = 50, Currency = "PLN", Borrower = firstUser, Lender = thirdUser };

        var users = new UserModel[] { firstUser, secondUser, thirdUser, fourthUser };
        var loans = new LoanModel[] { loan1, loan2, loan3 };

        context.Users.AddRange(users);
        context.Loans.AddRange(loans);
        context.SaveChanges();
    }
}
