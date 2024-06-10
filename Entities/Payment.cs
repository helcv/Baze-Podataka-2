using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Entities
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ExecutionDate { get; set; }
        public double Amount { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
