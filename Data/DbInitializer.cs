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

        var firstUser = new User
        {
            Username = "FirstUser",
            PasswordHash = "123",
            Salt = "456",
            BorrowedLoans = new List<Loan>(),
            LendedLoans = new List<Loan>()
        };

        var secondUser = new User
        {
            Username = "SecondUser",
            PasswordHash = "asdf",
            Salt = "ghjk",
            BorrowedLoans = new List<Loan>(),
            LendedLoans = new List<Loan>()
        };

        var thirdUser = new User
        {
            Username = "ThirdUser",
            PasswordHash = "qqq",
            Salt = "www",
            BorrowedLoans = new List<Loan>(),
            LendedLoans = new List<Loan>()
        };

        var fourthUser = new User
        {
            Username = "FourthUser",
            PasswordHash = "qqq",
            Salt = "www",
            BorrowedLoans = new List<Loan>(),
            LendedLoans = new List<Loan>()
        };


        var loan1 = new Loan{ Amount = 100, Currency = "PLN", Borrower = firstUser, Lender = secondUser };
        var loan2 = new Loan{ Amount = 2000, Currency = "PLN", Borrower = thirdUser, Lender = secondUser };
        var loan3 = new Loan{ Amount = 50, Currency = "PLN", Borrower = firstUser, Lender = thirdUser };

        var users = new User[] { firstUser, secondUser, thirdUser, fourthUser };
        var loans = new Loan[] { loan1, loan2, loan3 };

        context.Users.AddRange(users);
        context.Loans.AddRange(loans);
        context.SaveChanges();
    }
}
