using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTOs
{
    public class CreatePaymentDto
    {
        [Required]
        public double Amount { get; set; }
    }
}
