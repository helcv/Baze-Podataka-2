using BankingSystem.Data;
using BankingSystem.Entities;
using BankingSystem.Interfaces;
using BankingSystem.Repository;

namespace BankingSystem.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankContext _context;
    private Repository<User> _users;
    private Repository<Account> _accounts;
    private Repository<Loan> _loans;
    private Repository<Payment> _payments;
    private Repository<Card> _cards;
    private Repository<Transaction> _transactions;
    private Repository<Bank> _banks;
    private Repository<Branch> _branches;
    private Repository<Employee> _employees;

    public UnitOfWork(BankContext context)
    {
        _context = context;
    }

    public IRepository<User> Users => _users ??= new Repository<User>(_context);
    public IRepository<Account> Accounts => _accounts ??= new Repository<Account>(_context);
    public IRepository<Loan> Loans => _loans ??= new Repository<Loan>(_context);
    public IRepository<Payment> Payments => _payments ??= new Repository<Payment>(_context);
    public IRepository<Card> Cards => _cards ??= new Repository<Card>(_context);
    public IRepository<Transaction> Transactions => _transactions ??= new Repository<Transaction>(_context);
    public IRepository<Bank> Banks => _banks ??= new Repository<Bank>(_context);
    public IRepository<Branch> Branches => _branches ??= new Repository<Branch>(_context);
    public IRepository<Employee> Employees => _employees ??= new Repository<Employee>(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}


