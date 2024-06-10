using BankingSystem.Entities;

namespace BankingSystem.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime RepaymentDate { get; set; }
        public ICollection<PaymentDto> Payments { get; set; }
    }
}
