using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BankingSystem.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
