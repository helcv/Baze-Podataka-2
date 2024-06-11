namespace BankingSystem.DTOs
{
    public class SavingsDto
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public  double InterestRate{ get; set; }
    }
}
