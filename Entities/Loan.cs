using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Entities
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime RepaymentDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
