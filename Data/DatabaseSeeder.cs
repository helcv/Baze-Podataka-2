using BankingSystem.Common;
using BankingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace BankingSystem.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedData(BankContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var banks = new Bank[]
        {
            new Bank { Name = "Bank of America", SwiftCode = "BOFAUS3N" },
            new Bank { Name = "Chase Bank", SwiftCode = "CHASUS33" },
        };
            foreach (var bank in banks)
            {
                context.Banks.Add(bank);
            }
            context.SaveChanges();

            var branches = new Branch[]
            {
            new Branch { Name = "Main Branch", Location = "New York", BankId = 1 }, 
            new Branch { Name = "Downtown Branch", Location = "Los Angeles", BankId = 2 }, 
            };
            foreach (var branch in branches)
            {
                context.Branches.Add(branch);
            }
            context.SaveChanges();

            var employees = new Employee[]
            {
            new Employee { FirstName = "John", LastName = "Doe", BranchId = 1 },
            new Employee { FirstName = "Jane", LastName = "Smith", BranchId = 2 }
            };
            foreach (var employee in employees)
            {
                context.Employees.Add(employee);
            }
            context.SaveChanges();

            var users = new User[]
            {
            new User { FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", PhoneNumber = "1234567890", Address = "123 Main St", EmployeeId = 1 }, 
            new User { FirstName = "Bob", LastName = "Smith", Email = "bob@example.com", PhoneNumber = "9876543210", Address = "456 Elm St", EmployeeId = 2 }
            };
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }
            context.SaveChanges();

            var accounts = new List<Account>();

            accounts.Add(new SavingsAccount { Balance = 1000, CreatedDate = DateTime.Now, UserId = 1, InterestRate = 0.03 });
            accounts.Add(new SavingsAccount { Balance = 1500, CreatedDate = DateTime.Now, UserId = 2, InterestRate = 0.03 });

            accounts.Add(new CheckingsAccount { Balance = 500, CreatedDate = DateTime.Now, UserId = 1, OverdraftLimit = 200 });
            accounts.Add(new CheckingsAccount { Balance = 800, CreatedDate = DateTime.Now, UserId = 2, OverdraftLimit = 300 });

            foreach (var account in accounts)
            {
                context.Accounts.Add(account);
            }

            context.SaveChanges();

            var cards = new Card[]
            {
            new Card { CardNumber = 1234567890123456, Balance = 1000, ExpirationDate = DateTime.Now.AddYears(3), CVV = 123, Type = CardType.CREDIT, UserId = 1, AccountId = 1 },
            new Card { CardNumber = 2345678901234567, Balance = 500, ExpirationDate = DateTime.Now.AddYears(3), CVV = 456, Type = CardType.DEBIT, UserId = 2, AccountId = 2 },
            };

            foreach (var card in cards)
            {
                context.Cards.Add(card);
            }
            context.SaveChanges();

            var transactions = new Transaction[]
            {
            new Transaction { Timestamp = DateTime.Now, Status = "Success", Type = "Purchase", CardNumber = 1234567890123456 },
            new Transaction { Timestamp = DateTime.Now, Status = "Success", Type = "Withdrawal", CardNumber = 2345678901234567 },
            };

            foreach (var transaction in transactions)
            {
                context.Transactions.Add(transaction);
            }
            context.SaveChanges();

            var loans = new Loan[]
            {
            new Loan { Amount = 10000, RepaymentDate = DateTime.Now.AddYears(1), UserId = 1, BranchId = 1 },
            new Loan { Amount = 5000, RepaymentDate = DateTime.Now.AddYears(2), UserId = 2, BranchId = 2 },
            };

            foreach (var loan in loans)
            {
                context.Loans.Add(loan);
            }
            context.SaveChanges();
        }
    }
}
