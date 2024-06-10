using System.ComponentModel.DataAnnotations;

namespace BankingSystem.DTOs
{
    public class CreateLoanDto
    {
        [Required]
        public double Amount { get; set; }
    }
}
