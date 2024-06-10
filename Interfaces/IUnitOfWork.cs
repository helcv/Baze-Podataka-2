using BankingSystem.Entities;

namespace BankingSystem.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Account> Accounts { get; }
    IRepository<Loan> Loans { get; }
    IRepository<Payment> Payments { get; }
    IRepository<Card> Cards { get; }
    IRepository<Transaction> Transactions { get; }
    IRepository<Bank> Banks { get; }
    IRepository<Branch> Branches { get; }
    IRepository<Employee> Employees { get; }
    Task SaveAsync();
}

