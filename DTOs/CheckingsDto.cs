namespace BankingSystem.DTOs
{
    public class CheckingsDto
    {
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public double OverdraftLimit { get; set; }
    }
}
